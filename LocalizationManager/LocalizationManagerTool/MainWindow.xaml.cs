using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LocalizationManagerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Columns = new List<string>();
        ObservableCollection<Row> row = new ObservableCollection<Row>();

        public class Row : INotifyPropertyChanged
        {
            //public Row(int size)
            //{
            //    items = new string[size];
            //}

            //private string[] items;
            private string id = string.Empty;
            private string en = string.Empty;
            private string fr = string.Empty;
            private string es = string.Empty;
            private string ja = string.Empty;
            public string Id
            {
                get { return id; }
                set
                {
                    id = value;
                    OnPropertyChanged(id);
                }
            }
            public string En
            {
                get { return en; }
                set
                {
                    en = value;
                    OnPropertyChanged(en);
                }
            }
            public string Fr
            {
                get { return fr; }
                set
                {
                    fr = value;
                    OnPropertyChanged(fr);
                }
            }
            public string Es
            {
                get { return es; }
                set
                {
                    es = value;
                    OnPropertyChanged(es);
                }
            }
            public string Ja
            {
                get { return ja; }
                set
                {
                    ja = value;
                    OnPropertyChanged(ja);
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            dataGrid.ItemsSource = row;

            Row test = new Row();
            test.Id = "test";
            test.En = "en";
            test.Fr = "fr";
            test.Es = "es";
            test.Ja = "ja";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Clicked");
            var dialog = new OpenFileDialog();
            dialog.FileName = "Import file";
            dialog.Filter = "CSV|*.csv|XML|*.xml|JSON|*.json";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filename = dialog.FileName;
                string extension = System.IO.Path.GetExtension(filename);
                MessageBox.Show(extension);
                switch (extension)
                {
                    case ".xml":
                        ImportXML(filename);
                        MessageBox.Show("XML imported");
                        break;
                    case ".json":
                        ImportJSON(filename);
                        MessageBox.Show("JSON imported");
                        break;
                    case ".csv":
                        ImportCSV(filename);
                        MessageBox.Show("CSV imported");
                        break;
                }
            }
        }

        private void ImportXML(string filename)
        {
            List<Row> xmlFile = ImportFromXML(filename);
            foreach (Row xmlRow in xmlFile)
            {
                row.Add(xmlRow);
            }
        }

        private void ImportJSON(string filename)
        {
            List<Row> xmlFile = ImportFromJSON(filename);
            foreach (Row xmlRow in xmlFile)
            {
                row.Add(xmlRow);
            }
        }

        private void ImportCSV(string filename)
        {
            List<Row> xmlFile = ImportFromCSV(filename);
            foreach (Row xmlRow in xmlFile)
            {
                row.Add(xmlRow);
            }
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string folderName = dialog.FolderName + "/test.csv";
                ExportToCSV(row.ToList(), folderName);
            }
        }
    }
}