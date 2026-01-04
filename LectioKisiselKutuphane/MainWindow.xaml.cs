using LectioKisiselKutuphane.Classes;
using Microsoft.Win32;
using SQLite;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Numerics;
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
        SQLiteConnection Database;
        Kitap seciliKitap;
        Kitap tempKitap;

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
            Database = new SQLiteConnection(App.databasePath);
            Database.CreateTable<Kitap>();
            Kitaplar = Database.Table<Kitap>().ToList();
        }
        private void ReadDatabase()
        {
            Kitaplar = Database.Table<Kitap>().ToList();
            booksListView.ItemsSource = Kitaplar;
        }

        //string pathı wpf için imagesource e çevirir
        //kaynak: https://stackoverflow.com/questions/1904956/cannot-convert-string-to-imagesource-how-can-i-do-this
        private static ImageSource PathToImageSource(String path, bool DosyaMi)
        {
            //dosya yoksa veya path nulllsa varsayılan kitap ikonunu döndür
            if(DosyaMi == true && !File.Exists(path))
                return new BitmapImage(new Uri("pack://application:,,,/Images/EmptyBook.png"));
            if (string.IsNullOrWhiteSpace(path))
                return new BitmapImage(new Uri("pack://application:,,,/Images/EmptyBook.png"));
            BitmapImage kitapIkon = new BitmapImage();
            kitapIkon.BeginInit();
            kitapIkon.UriSource = new Uri(path);
            kitapIkon.EndInit();

            return kitapIkon;
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
                Kitaplar = Kitaplar.ToList().OrderByDescending(k => k.Sayfa).ToList();
                filterTextBlock.Text = " Sayfa";
            }
            if (filtre == "Okunma")
            {
                Kitaplar = Kitaplar.ToList().OrderByDescending(k => k.IsRead).ToList();
                filterTextBlock.Text = " Okunma";
            }
            booksListView.ItemsSource = Kitaplar;
        }

        private void booksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sayfaTextBox.BorderBrush = Brushes.Gray;
            kitapsilButton.IsEnabled = true;
            seciliKitap = (Kitap)booksListView.SelectedItem;
            if (seciliKitap == null)
                return;

            //yan paneli doldur
            isimTextBox.Text = seciliKitap.Isim;
            yazarTextBox.Text = seciliKitap.Yazar;
            turTextBox.Text = seciliKitap.Tur;
            sayfaTextBox.Text = seciliKitap.Sayfa.ToString();
            notlarTextBlock.Text = seciliKitap.Notlar;
            if (seciliKitap.IsRead)
                okudummuCheckBox.IsChecked = true;
            else
                okudummuCheckBox.IsChecked = false;

                resimImage.Source = PathToImageSource(seciliKitap.IkonPath, false);

            //yan paneldeki değişiklikler burdan gelecek
            tempKitap = new Kitap
            {
                Id = seciliKitap.Id,
                Isim = seciliKitap.Isim,
                Yazar = seciliKitap.Yazar,
                Tur = seciliKitap.Tur,
                Sayfa = seciliKitap.Sayfa,
                Notlar = seciliKitap.Notlar,
                IkonPath = seciliKitap.IkonPath,
                IsRead = seciliKitap.IsRead
            };
        }

        private void kitapekleButton_Click(object sender, RoutedEventArgs e)
        {
            YeniKitapWindow yeniKitapWindow = new YeniKitapWindow();
            yeniKitapWindow.ShowDialog();

            ReadDatabase();
            KitaplarSirala(filterTextBlock.Text.Trim());
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

        private void notlarTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
        }

        private void okudummuCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (seciliKitap == null)
                return;
            kaydetButton.IsEnabled = true;
            if(okudummuCheckBox.IsChecked == true)
            {
                SoundPlayer yeey = new SoundPlayer(LectioKisiselKutuphane.Properties.Resources.BookRead);
                yeey.Play();
            }

        }

        private void kaydetButton_Click(object sender, RoutedEventArgs e)
        {
            int sayfaSayisi;
            var IsOkunduMu = false;

            //kontroller
            if (seciliKitap == null)
                return;
            if (okudummuCheckBox.IsChecked == true)
                IsOkunduMu = true;
            if (!(Int32.TryParse(sayfaTextBox.Text, out sayfaSayisi) && sayfaSayisi > 0))
            {
                sayfaTextBox.BorderBrush = Brushes.Red;
                SystemSounds.Exclamation.Play();
                return;
            }

            seciliKitap.Isim = isimTextBox.Text;
            seciliKitap.Yazar = yazarTextBox.Text;
            seciliKitap.Tur = turTextBox.Text;
            seciliKitap.Sayfa = sayfaSayisi;
            seciliKitap.Notlar = notlarTextBlock.Text;
            seciliKitap.IkonPath = tempKitap.IkonPath;
            seciliKitap.IsRead = IsOkunduMu;
            Database.Update(seciliKitap);

            ReadDatabase();
            sayfaTextBox.BorderBrush = Brushes.Gray;
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (seciliKitap == null)
                return;

            //kaynak: https://stackoverflow.com/questions/19660775/how-to-use-open-file-dialog
            var dlg = new OpenFileDialog();
            dlg.Filter = "PNG files (*.png)|*.png";
            dlg.Multiselect = false;

            //eğer pencere kapanması gerektiği gibi kapanmadıysa return;
            if (dlg.ShowDialog() != true)
                return;
            tempKitap.IkonPath = dlg.FileName;
            resimImage.Source = PathToImageSource(tempKitap.IkonPath, true);
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
            Random random = new();
            var ikonNo = random.Next(0, 8);
            if (seciliKitap == null)
                return;

            tempKitap.IkonPath = $"pack://application:,,,/Images/BookIcons/Icon{ikonNo}.png";
            resimImage.Source = PathToImageSource(tempKitap.IkonPath, false);
        }

        private void sayfaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
        }

        private void turTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
        }

        private void yazarTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
        }

        private void isimTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            kaydetButton.IsEnabled = true;
        }

        private void kitapsilButton_Click(object sender, RoutedEventArgs e)
        {
            //kaynak github.com/praeclarum/sqlite-net
            Kitap seciliKitap = (Kitap)booksListView.SelectedItem;
            if(seciliKitap != null)
                Database.Delete(seciliKitap);

            ReadDatabase();

        }

        private void cikisButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}