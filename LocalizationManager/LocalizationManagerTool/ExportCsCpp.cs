using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LocalizationManagerTool
{
    public partial class MainWindow
    {
        struct LocalizationData
        {
            LocalizationData(string _ID, string _language, string _localizedValue)
            {
                ID = _ID;
                language = _language;
                localizedValue = _localizedValue;
            }

            public string ID { get; set; }
            public string language { get; set; }

            public string localizedValue { get; set; }
        }

        private static Dictionary<string, Dictionary<string, string>> localizerDictionaries = new Dictionary<string, Dictionary<string, string>>()
        {
            { "en", new Dictionary<string, string>
                {
                    { "hello", "Hello" },
                    { "goodbye", "Goodbye" },
                    { "welcome", "Welcome" }
                }
            },
        };

        public void ExportCS(DataGrid dataGrid, string filePath)
        {
            using (var reader = new StreamReader(@"D:\SVN\Eleves\Tools\CSModel.cs"))
            {
                string? result = reader.ReadToEnd();
                int id = result.IndexOfAny("/**/".ToCharArray());
                //result = result.Insert(id,
                //    "\t\t{ \"fr\",new Dictionary<string, string> " +
                //        "\n\t\t { \"hello\", \"Bonjour\" }, " +
                //        "\n\t\t { \"play\", \"Jouer\" }, " +
                //        "\n\t\t { \"settings\", \"Options\" } " +
                //        "\n\t\t } " +
                //    "\n\t },");
                //string toModify = result.Substring(id, 1);
                //toModify = 

                string toAdd = string.Empty;

                List<string> languages = GetLanguages();
                MessageBox.Show(languages.Count.ToString());

                for (int i = 0; i < languages.Count; i++)
                {
                    toAdd += "\t\t{ \"" + languages[i] + "\",new Dictionary<string, string> ";
                    foreach (var item in rows)
                    {
                        toAdd += "\n\t\t { \"" + item.Id + "\", \"" + item.Fr + "\" },";
                    }
                    toAdd += "\n\t\t }\n\t },";
                }

                result = result.Insert(id, toAdd);

                using (var writer = new StreamWriter(filePath))
                {
                    writer.Write(result);
                }

                //do
                //{
                //    result = reader.ReadLine();
                //    Debug.WriteLine(result);
                //}
                //while (result != "/**/");
                //fs.Seek(reader.Peek() -1, SeekOrigin.Begin);
                //writer.Write("HAHAHA");
            }
        }

        public void ExportCPP(string filePath)
        {

        }

        public List<string> GetLanguages()
        {
            Type rowType = typeof(Row);
            List<string> list = new List<string>();
            foreach (var field in rowType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                //MessageBox.Show(field.FieldType.ToString());
                if (field.FieldType == typeof(string) && field.Name != "id")
                {
                    list.Add(field.Name);
                }
            }
            return list;
        }

        //AHAHA DUNKAN
    }
}
