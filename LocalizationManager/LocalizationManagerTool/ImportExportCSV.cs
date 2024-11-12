using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
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
                        var values = line.Split(',');
                        Row row = new Row();
                        Type rowType = typeof(Row);
                        for (int i = 0; i < values.Length; i++)
                        {
                            rowType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)[i].SetValue(row, values[i]);
                        }
                        content.Add(row);
                    }
                }
            }

            return content;
        }

        public void ExportToCSV(List<Row> content, string _path)
        {
            using (var writer = new StreamWriter(_path))
            {
                foreach (var item in content)
                {
                    writer.WriteLine(item);
                }
            }
        }
    }
}
