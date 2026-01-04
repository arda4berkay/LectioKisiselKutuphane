using LectioKisiselKutuphane.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LectioKisiselKutuphane
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        SQLiteConnection Database;
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void evetButton_Click(object sender, RoutedEventArgs e)
        {
            Database = new SQLiteConnection(App.databasePath);
            Database.CreateTable<Kitap>();

            var loremIpsum = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to " +
                "make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. " +
                "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software " +
                "like Aldus PageMaker including versions of Lorem Ipsum.";
            var ornekKitaplar = new List<Kitap>
            {
                new Kitap { Isim = "Bülbülü Öldürmek", Yazar = "Harper Lee", Tur = "Roman", Sayfa = 355, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon0.png", IsRead = false },
                new Kitap { Isim = "Fahrenheit 451", Yazar = "Ray Bradbury", Tur = "Roman", Sayfa = 200, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon1.png", IsRead = false },
                new Kitap { Isim = "Sefiller", Yazar = "Victor Hugo", Tur = "Roman", Sayfa = 200, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon2.png", IsRead = false },
                new Kitap { Isim = "Denemeler", Yazar = "Michel de Montaigne", Tur = "Roman", Sayfa = 67, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon3.png", IsRead = false },
                new Kitap { Isim = "Cesur Yeni Dünya", Yazar = "Aldous Huxley", Tur = "Roman", Sayfa = 325, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon4.png", IsRead = false },
                new Kitap { Isim = "İnsan ne ile Yaşar", Yazar = "Tolstoy", Tur = "Roman", Sayfa = 40, Notlar = loremIpsum, IkonPath = "pack://application:,,,/Images/BookIcons/Icon5.png", IsRead = false },
            };
            Database.InsertAll(ornekKitaplar);

            this.Close();
        }
    }
}
