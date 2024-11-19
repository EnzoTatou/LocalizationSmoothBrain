
using System.Windows;
using System.Windows.Controls;
namespace LocalizationManagerTool
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AddColumnWindow : Window
    {
        public string language = string.Empty;
        public AddColumnWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox? textbox = (sender as TextBox);
            if (textbox == null) return;
            language = textbox.Text;
        }
    }
}
