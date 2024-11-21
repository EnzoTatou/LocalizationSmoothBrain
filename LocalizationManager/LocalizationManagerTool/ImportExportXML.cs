using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
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
            var xDocument = XDocument.Load(filePath);

            // Extract languages and values
            var languages = xDocument.Root.Elements()
                .Select(lang => new
                {
                    LangCode = lang.Name.NamespaceName, // Get the namespace as the language code
                    Entries = lang.Elements("Id")
                        .Zip(lang.Elements("Value"), (id, value) => new { Id = id.Value, Value = value.Value })
                }).ToList();

            foreach (var lang in languages) 
            {
                MessageBox.Show(lang.Entries.First().Value);
            }

            // Create a list of rows
            var rows = new List<Row>();

            // Create the header row
            var header = new Row();
            header.Languages["id"] = "id";
            foreach (var language in languages)
            {
                header.Languages[language.LangCode] = language.LangCode;
            }
            rows.Add(header);

            // Create the data rows
            var ids = languages.First().Entries.Select(e => e.Id).Distinct();

            foreach (var id in ids)
            {
                var row = new Row();
                row.Languages["id"] = id;
                foreach (var language in languages)
                {
                    var value = language.Entries.FirstOrDefault(e => e.Id == id)?.Value ?? "";
                    MessageBox.Show(value);
                    row.Languages[language.LangCode] = value;
                }
                rows.Add(row);
            }

            return rows;


            //List<XElement> ids = xDocument.Root.Descendants().Where(element => element.Name.LocalName == "Id").ToList();
            //List<string> languages = new List<string>();
            //List<XElement> values = xDocument.Root.Descendants().Where(element => element.Name.LocalName == "Value").ToList();
            //List<XElement> languageElements = xDocument.Root.Elements().ToList();

            //foreach (var languageElement in languageElements)
            //{
            //    languages.Add(languageElement.Name.NamespaceName);
            //}

            //for(int i = 0; i < ids.Count; i++)
            //{
            //    Row row = new Row();
            //    int k = 0;
            //    for(int j = 0; j < languages.Count; j++)
            //    {
            //        if()
            //        row.Languages.Add(languages[j], )
            //    }
            //}


            throw new System.Exception("haha dunkan tu t'es fait avoir");
        }

        public void ExportToXML(DataGrid dataGrid, string filePath)
        {
            Dictionary<string, Dictionary<string, string>> localizerDictionaries = new();

            foreach (string language in GetLanguages())
            {
                localizerDictionaries.Add(language, new Dictionary<string, string>());
            }

            foreach (var row in rows)
            {
                string id = row.Languages["id"];
                foreach (var key in row.Languages.Keys)
                {
                    if (key == "id") continue;
                    localizerDictionaries[key][id] = row.Languages[key];
                }
            }

            // Create an XML file manually
            using (var writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Languages"); // Root element

                foreach (string language in localizerDictionaries.Keys)
                {
                    writer.WriteStartElement("Language", language);

                    foreach (var idValuePair in localizerDictionaries[language])
                    {
                        writer.WriteElementString("Id", idValuePair.Key);
                        writer.WriteElementString("Value", idValuePair.Value);
                    }

                    writer.WriteEndElement(); // </Language>
                }

                writer.WriteEndElement(); // </Languages>
                writer.WriteEndDocument();
            }
        }
    }
}
