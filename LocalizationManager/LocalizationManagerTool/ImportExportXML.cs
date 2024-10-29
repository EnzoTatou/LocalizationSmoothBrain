using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
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
        public List<ColumnStruct> ImportFromXML(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<ColumnStruct>));
            using (var reader = new StreamReader(filePath))
            {
                if(serializer != null)
                {
                    object? deserializedContent = serializer.Deserialize(reader);

                    if (deserializedContent != null)
                    {
                        return (List<ColumnStruct>)deserializedContent;
                    }
                }
            }

            throw new System.Exception("haha dunkan tu t'es fait avoir");
        }

        public void ExportToXML(DataGrid dataGrid, string filePath)
        {
            var items = dataGrid.Items.Cast<ColumnStruct>().ToList();
            var serializer = new XmlSerializer(typeof(List<ColumnStruct>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, items);
            }
        }
    }
}
