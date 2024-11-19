using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
            private Dictionary<string, string> languages = new Dictionary<string, string>();
            public Dictionary<string, string> Languages
            {
                get { return languages; }
                set { languages = value; OnPropertyChanged(nameof(Languages)); }
            }
            public Row()
            {
                languages.Add("id", string.Empty);
                languages.Add("en", string.Empty);
                languages.Add("fr", string.Empty);
                languages.Add("es", string.Empty);
                languages.Add("ja", string.Empty);
            }

            public string this[string key]
            {
                get
                {
                    if (!languages.TryGetValue(key, out var value)) throw new Exception($"can't find {key}");
                    return value;
                }
                set
                {
                    if(languages.ContainsKey(key)) languages[key] = value;
                    else languages.Add(key, value);
                    OnPropertyChanged(nameof(Languages));
                }
            }
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
            Row tempRow = new Row();
            var langages = typeof(Row).GetProperty("Languages")?.GetValue(tempRow) as Dictionary<string, string>;
            if (langages == null) return;

            foreach(var test in langages)
            {
                MessageBox.Show(test.Key);
                dataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = test.Key,
                    Binding = new Binding($"[{test.Key}]") // Utilise la propriété publique comme chemin
                });
            }
            foreach (var property in typeof(Row).GetProperties(System.Reflection.BindingFlags.Public
                                                                | System.Reflection.BindingFlags.Instance))
            {
               
            }

            var actionColumn = new DataGridTemplateColumn
            {
                Header = "Actions",
                CellTemplate = new DataTemplate
                {
                    VisualTree = CreateButton()
                }
            };



            // Ajouter la colonne à la fin
            dataGrid.Columns.Add(actionColumn);
        }

        private FrameworkElementFactory CreateButton()
        {
            var button = new FrameworkElementFactory(typeof(Button));
            button.SetValue(Button.ContentProperty, "Supprimer");
            button.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Red));
            button.SetValue(Button.ForegroundProperty, new SolidColorBrush(Colors.White));
            button.AddHandler(Button.ClickEvent, new RoutedEventHandler(DeleteRow_Click));

            // Liez le Tag du bouton à la ligne correspondante
            button.SetBinding(Button.TagProperty, new Binding());

            return button;
        }

    }

}