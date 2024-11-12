using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
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
                List<FieldInfo> languages = GetLanguages();

                for (int i = 0; i < languages.Count; i++)
                {
                    toAdd += "\t\t{ \"" + languages[i].Name + "\",new Dictionary<string, string>()";
                    foreach (var item in rows)
                    {
                        toAdd += "\n\t\t\t{ \"" + item.Id + "\", \"" + languages[i].GetValue(item) + "\" }";
                        if(rows.IndexOf(item) != rows.Count -1)
                        {
                            toAdd += ",";
                        }
                    }

                    toAdd += "\n\t\t}";

                    if (i != languages.Count -1)
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
                List<FieldInfo> languages = GetLanguages();

                for (int i = 0; i < languages.Count; i++)
                {
                    toAdd += "\t\t{ \"" + languages[i].Name + "\", {";
                    foreach (var item in rows)
                    {
                        toAdd += "\n\t\t\t{ \"" + item.Id + "\", \"" + languages[i].GetValue(item) + "\" }";
                        if (rows.IndexOf(item) != rows.Count - 1)
                        {
                            toAdd += ",";
                        }
                    }

                    toAdd += "}\n\t\t}";

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

        public List<FieldInfo> GetLanguages()
        {
            Type rowType = typeof(Row);
            List<FieldInfo> list = new List<FieldInfo>();
            foreach (var field in rowType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                //MessageBox.Show(field.FieldType.ToString());
                if (field.FieldType == typeof(string) && field.Name != "id")
                {
                    list.Add(field);
                }
            }
            return list;
        }

        //AHAHA DUNKAN
    }
}
