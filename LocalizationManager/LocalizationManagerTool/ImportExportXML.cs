using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LocalizationManagerTool
{
    public struct ColumnStruct
    {
        public int ID { get; set; }
        public string Languages { get; set; }
    }

    public partial class MainWindow
    {
        public List<Row> ImportFromXML(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<Row>));
            using (var reader = new StreamReader(filePath))
            {
                if (serializer != null)
                {
                    object? deserializedContent = serializer.Deserialize(reader);

                    if (deserializedContent != null)
                    {
                        return (List<Row>)deserializedContent;
                    }
                }
            }

            var xDocument = XDocument.Load(filePath);

            foreach (var languageElement in xDocument.Root.Elements())
            {
                string language = languageElement.Name.LocalName;
                MessageBox.Show(language);
                //if (!localizerDictionaries.ContainsKey(language))
                //{
                //    localizerDictionaries[language] = new Dictionary<string, string>();
                //}

                //foreach (var entryElement in languageElement.Elements())
                //{
                //    string key = entryElement.Name.LocalName;
                //    string value = entryElement.Value;
                //    localizerDictionaries[language][key] = value;
                //}
            }

            throw new System.Exception("haha dunkan tu t'es fait avoir");
        }

        public void ExportToXML(DataGrid dataGrid, string filePath)
        {
            var items = dataGrid.ItemsSource.Cast<Row>().ToList();
            var serializer = new XmlSerializer(typeof(List<Row>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, items);
            }
        }
    }
}
