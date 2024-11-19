using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {

        public List<Row> ImportFromCSV(string _path)
        {
            List<Row> content = new();

            using (var reader = new StreamReader(_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line != null)
                    {
                        var values = line.Split(';');
                        Row row = new Row();
                        Type rowType = typeof(Row);
                        for (int i = 0; i < values.Length - 1; i++)
                        {
                            rowType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)[i].SetValue(row, values[i]);
                        }
                        content.Add(row);
                    }
                }
            }

            return content;
        }

        public void ExportToCSV(DataGrid dataGrid, string filePath)
        {
            var items = dataGrid.ItemsSource?.Cast<Row>().ToList();

            List<string> headers = new();
            foreach (var property in typeof(Row).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                headers.Add(property.Name);
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
