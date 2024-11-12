using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        public static List<Row> ImportFromJSON(string filePath)
        {
            using var reader = new StreamReader(filePath) ?? throw new ArgumentNullException("tu troll mon frere");

            return JsonSerializer.Deserialize<List<Row>>(reader.ReadToEnd()) ?? throw new ArgumentNullException("Non franchement tu troll de zinzin");
        }

        public static void ExportToJSON(DataGrid dataGrid, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(dataGrid.ItemsSource.Cast<Row>().ToList());

            using StreamWriter writetext = new StreamWriter(filePath);
            writetext.Write(jsonString);
        }
    }
}
