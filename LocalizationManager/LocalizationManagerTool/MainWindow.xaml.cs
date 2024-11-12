using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Columns = new List<string>();
        ObservableCollection<Row> row = new ObservableCollection<Row>();
        
        class Row
        {
            public Row(int size)
            {
                items = new string[size];
            }
            public string[] items;
        }
        public MainWindow()
        {
            InitializeComponent();
            Columns.Add("id");
            Columns.Add("en");
            Columns.Add("fr");
            Columns.Add("es");
            Columns.Add("ja");

            dataGrid.ItemsSource = row;
            foreach (string column in Columns)
            {
                //Pour ajouter une colonne à notre datagrid
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = column;
                textColumn.Binding = new Binding(column);
                dataGrid.Columns.Add(textColumn);
            }

            

            Row test = new Row(Columns.Count);
            for (int i = 0; i < Columns.Count; i++)
            {
                test.items[i] = i.ToString();
            }
            row.Add(test);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Clicked");
            var dialog = new OpenFileDialog();
            dialog.FileName = "Import file";
            dialog.Filter = "CSV|*.csv|XML|*.xml|JSON|*.json";
            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                string filename = dialog.FileName;
                string extension = System.IO.Path.GetExtension(filename);
                switch(extension)
                {
                    case "xml":
                        ImportXML(filename);
                        MessageBox.Show("XML imported");
                        break;
                }
            }
        }

        private void ImportXML(string filename)
        {
            List<ColumnStruct> xmlFile = ImportFromXML(filename);
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string folderName = dialog.FolderName + "/test.xml";
                ExportToXML(dataGrid, folderName);
            }
        }
    }
}