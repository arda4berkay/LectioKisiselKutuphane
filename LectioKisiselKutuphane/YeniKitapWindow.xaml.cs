using LectioKisiselKutuphane.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
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
    /// Interaction logic for YeniKitapWindow.xaml
    /// </summary>
    public partial class YeniKitapWindow : Window
    {
        internal bool MakeLabelVisibleIfEmpty(TextBox textBox)
        {
            if(string.IsNullOrWhiteSpace(textBox.Text))
            {
                return false;
            }
            return true;
        }
        public YeniKitapWindow()
        {
            InitializeComponent();
        }

        private void kaydetButton_Click(object sender, RoutedEventArgs e)
        {
            int sayfaSayisi;
            bool IsOkunduMu;
            //TODO: daha iyi yapılabilir
            //kutular boş mu kontrol eder
            if (!(MakeLabelVisibleIfEmpty(isimTextBox) &&
                MakeLabelVisibleIfEmpty(yazarTextBox) &&
                MakeLabelVisibleIfEmpty(turTextBox) &&
                MakeLabelVisibleIfEmpty(sayfaTextBox) &&
                MakeLabelVisibleIfEmpty(notlarTextBox)))
            {
                warningLabel.Visibility = Visibility.Visible;
                SystemSounds.Exclamation.Play();
                return;
            }

            //sayfa sayısı pozitif integer mi kontrol eder
            if(!(Int32.TryParse(sayfaTextBox.Text, out sayfaSayisi) && sayfaSayisi > 0))
            {
                numberWarningLabel.Visibility = Visibility.Visible;
                SystemSounds.Exclamation.Play();
                return;
            }

            //TODO: daha iyi yapılabilir
            if (isReadBox.IsChecked == true)
                IsOkunduMu = true;
            else
                IsOkunduMu = false;

                var db = new SQLiteConnection(App.databasePath);
            var kitap = new Kitap
                (
                    isimTextBox.Text, yazarTextBox.Text, turTextBox.Text, notlarTextBox.Text, sayfaSayisi, IsOkunduMu
                );
            db.Insert(kitap);
            db.Close();

            //TODO: eski duruyor belki kullanılabilir
            //SystemSounds.Beep.Play();
            //MessageBox.Show($"{isimTextBox.Text} kütüphaneye eklendi!", "Yeni kitap!", MessageBoxButton.OK, MessageBoxImage.Information);
            
            this.Close();
        }
    }
}
