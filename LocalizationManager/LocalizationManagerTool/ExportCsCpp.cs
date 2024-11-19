using System.IO;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        public void ExportCS(DataGrid dataGrid, string filePath)
        {
            using (var reader = new StreamReader(@"..\..\..\..\CSModel.cs"))
            {
                string? result = reader.ReadToEnd();
                int id = result.IndexOfAny("/**/".ToCharArray());
                string toAdd = string.Empty;
                List<string> languages = GetLanguages();

                for (int i = 0; i < languages.Count; i++)
                {
                    toAdd += "\t\t{ \"" + languages[i] + "\",new Dictionary<string, string>()";
                    foreach (var item in rows)
                    {
                        toAdd += "\n\t\t\t{ \"" + item["id"] + "\", \"" + item[languages[i]] + "\" }";
                        if (rows.IndexOf(item) != rows.Count - 1)
                        {
                            toAdd += ",";
                        }
                    }

                    toAdd += "\n\t\t}";

                    if (i != languages.Count - 1)
                    {
                        toAdd += ",";
                    }
                    toAdd += "\n\n";
                }

                toAdd += "\n\t }";

                result = result.Insert(id, toAdd);

                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(result);
                }
            }
        }

        public void ExportCPP(DataGrid dataGrid, string filePath)
        {
            using (var reader = new StreamReader(@"..\..\..\..\CPPModel.h"))
            {
                string? result = reader.ReadToEnd();
                int id = result.IndexOfAny("/**/".ToCharArray());
                string toAdd = string.Empty;
                List<string> languages = GetLanguages();

                for (int i = 0; i < languages.Count; i++)
                {
                    toAdd += "\t\t{ \"" + languages[i] + "\", {";
                    foreach (var item in rows)
                    {
                        toAdd += "\n\t\t\t{ \"" + item["id"] + "\", \"" + item[languages[i]] + "\" }";
                        if (rows.IndexOf(item) != rows.Count - 1)
                        {
                            toAdd += ",";
                        }
                    }

                    toAdd += "}\n\t\t}";

                    if (i != languages.Count - 1)
                    {
                        toAdd += ",";
                        toAdd += "\n\n";
                    }
                }

                toAdd += "\n }";

                result = result.Insert(id, toAdd);

                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(result);
                }
            }

            using (var reader = new StreamReader(@"..\..\..\..\CPPModel.cpp"))
            {
                string? result = reader.ReadToEnd();
                string? folderPath = System.IO.Path.GetDirectoryName(filePath);
                string cppFilePath = (folderPath is not null) ? folderPath + "/test.cpp" : throw new Exception("no folder");

                using (var writer = new StreamWriter(cppFilePath))
                {
                    writer.Write(result);
                }
            }
        }

        public List<string> GetLanguages()
        {
            List<string> list = new List<string>();
            foreach (var column in dataGrid.Columns)
            {
                if (column.Header == null || column.Header.ToString() == "id") continue;
                string? header = column.Header.ToString();
                if (header != null)
                {
                    list.Add(header);
                }
            }
           
            return list;
        }

        //AHAHA DUNKAN
    }
}
