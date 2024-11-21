using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        public List<Row> ImportFromCSV(string _path)
        {
            List<Row> content = new();

            int it = 0;
            List<string> headers = new();
            int nbOfFields = 0;

            using (var reader = new StreamReader(_path))
            {
                while (!reader.EndOfStream)
                {
                    it++;
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    // headers
                    if (it <= 1)
                    {
                        headers = line.Split(';').ToList();
                        nbOfFields = headers.Count;

                        continue;
                    }

                    // rows
                    Row row = new Row();

                    List<string> values = line.Split(';').ToList();

                    for (int i = 0; i < values.Count - 1; i++)
                    {
                        row[headers[i]] = values[i];
                    }

                    content.Add(row);
                }
            }

            return content;
        }

        public void ExportToCSV(DataGrid dataGrid, string filePath)
        {
            var items = dataGrid.ItemsSource?.Cast<Row>().ToList();

            List<string> headers = new();
            foreach (var column in dataGrid.Columns)
            {
                if (column.Header == null) continue;
                string? header = column.Header.ToString();
                if (header != null) headers.Add(header);
            }

            using (var writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(string.Join(";", headers));

                foreach (Row item in items)
                {
                    var values = dataGrid.Columns.Select(col =>
                    {
                        var cellContent = col.GetCellContent(item);
                        if (cellContent is TextBlock textBlock)
                            return textBlock.Text;
                        return "";
                    }).ToArray();

                    writer.WriteLine(string.Join(";", values));
                }
            }
        }
    }
}
