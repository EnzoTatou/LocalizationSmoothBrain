using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        public static List<ColumnStruct> ImportFromJSON(string filePath)
        {
            using var reader = new StreamReader(filePath) ?? throw new ArgumentNullException("tu troll mon frere");

            return JsonSerializer.Deserialize<List<ColumnStruct>>(reader.ReadToEnd()) ?? throw new ArgumentNullException("Non franchement tu troll de zinzin");
        }

        public static void ExportToJSON(DataGrid dataGrid, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(dataGrid.Items.Cast<ColumnStruct>().ToList());

            using StreamWriter writetext = new StreamWriter(filePath);
            writetext.Write(jsonString);
        }
    }
}
