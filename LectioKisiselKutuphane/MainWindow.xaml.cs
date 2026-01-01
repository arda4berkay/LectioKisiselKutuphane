using LectioKisiselKutuphane.Classes;
using SQLite;
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

//filtre türleri ekle ok, kitap detay penceresi ekle, ilk giriş ekranı ekle, ayarlar ekle, hakkında ekle 
namespace LectioKisiselKutuphane
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kitap> Kitaplar;
        public MainWindow()
        {
            InitializeComponent();
            InitalizeDatabase();
            KitaplarSirala("Alfabetik");
            
        }
        
        //başlangıç için
        //TODO daha az iğrenç bir yol bul
        private void InitalizeDatabase()
        {
            var db = new SQLiteConnection(App.databasePath);
            db.CreateTable<Kitap>();
            Kitaplar = db.Table<Kitap>().ToList();
        }
        private void ReadDatabase()
        { 
            booksListView.ItemsSource = Kitaplar;
        }

        //kitapları sırala data base updatele filtre butonunu yazısını değiştir
        private void KitaplarSirala(String filtre)
        {
            if(filtre == "Alfabetik")
            {
                //kitaparı enumberable liste yap linq ile sırala
                Kitaplar = Kitaplar.ToList().OrderBy(k => k.Isim).ToList();
                filterTextBlock.Text = " Alfabetik";
            }
            if (filtre == "Sayfa")
            {
                Kitaplar = Kitaplar.ToList().OrderBy(k => k.Sayfa).ToList();
                filterTextBlock.Text = " Sayfa";
            }
            if (filtre == "Okunma")
            {
                Kitaplar = Kitaplar.ToList().OrderBy(k => k.IsRead).ToList();
                filterTextBlock.Text = " Okunma";
            }
            ReadDatabase();
        }

        private void UserDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void booksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void kitapekleButton_Click(object sender, RoutedEventArgs e)
        {
            YeniKitapWindow yeniKitapWindow = new YeniKitapWindow();
            yeniKitapWindow.ShowDialog();
            ReadDatabase();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            //Butonda yazanı değiştir ve yeni yazan filtreyi uygula
            if (filterTextBlock.Text.Trim() == "Alfabetik")
                KitaplarSirala("Sayfa");
            else if (filterTextBlock.Text.Trim() == "Sayfa")
                KitaplarSirala("Okunma");
            else if (filterTextBlock.Text.Trim() == "Okunma")
                KitaplarSirala("Alfabetik");
        }
    }
}