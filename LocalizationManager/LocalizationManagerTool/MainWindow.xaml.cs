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
        ObservableCollection<Row> rows = new ObservableCollection<Row>();

        public class Row : INotifyPropertyChanged
        {

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
            dataGrid.ItemsSource = rows;

            GenerateColumns();
        }



        private void ImportXML(string filename)
        {
            List<Row> xmlFile = ImportFromXML(filename);
            foreach (Row xmlRow in xmlFile)
            {
                rows.Add(xmlRow);
            }
        }

        private void ImportJSON(string filename)
        {
            List<Row> xmlFile = ImportFromJSON(filename);
            foreach (Row xmlRow in xmlFile)
            {
                rows.Add(xmlRow);
            }
        }

        private void ImportCSV(string filename)
        {
            List<Row> xmlFile = ImportFromCSV(filename);
            foreach (Row xmlRow in xmlFile)
            {
                rows.Add(xmlRow);
            }
        }

        private void Button_Export(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Export file";
            dialog.Filter = "CSV|*.csv|XML|*.xml|JSON|*.json";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string folderName = dialog.FileName;

                string filename = dialog.FileName;
                string extension = System.IO.Path.GetExtension(filename);
                MessageBox.Show(extension);
                switch (extension)
                {
                    case ".xml":
                        ExportToJSON(dataGrid, filename);
                        MessageBox.Show("XML exported");
                        break;
                    case ".json":
                        ExportToJSON(dataGrid, filename);
                        MessageBox.Show("JSON exported to " + filename);
                        break;
                    case ".csv":
                        ExportToCSV(dataGrid, filename);
                        MessageBox.Show("CSV exported");
                        break;
                }
            }
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
        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            Button? button = sender as Button;
            Row? row = button?.Tag as Row;

            if (row == null) return;
            rows.Remove(row);
        }

        private void GenerateColumns()
        {
            foreach (var field in typeof(Row).GetFields(System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance))
            {
                if(field.FieldType != typeof(PropertyChangedEventHandler))
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = field.Name,
                    Binding = new System.Windows.Data.Binding(field.Name)
                });
            }
        }
    }
}