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

namespace LectioKisiselKutuphane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            kitapduzenleButton.IsEnabled = false;
            kitapsilButton.IsEnabled = false;
        }

        private void UserDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}