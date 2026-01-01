using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LectioKisiselKutuphane.Classes
{
    public class Kitap
    {
        private static readonly Random random = new();

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Yazar { get; set; }
        public string Tur { get; set; }
        public string IkonPath { get; set; }
        public int Sayfa { get; set; }
        public string Notlar { get; set; }
        public bool IsRead { get; set; }

        //sql bağlantısı için
        public Kitap() { }

        public Kitap(string isim, string yazar, string tur, string notlar, int sayfa, bool isRead)
        {
            
            //Random random = new Random();

            var ikonNo = random.Next(0, 8);
            this.IkonPath = IkonPath = $"pack://application:,,,/Images/BookIcons/Icon{ikonNo}.png";

            this.Isim = isim;
            this.Notlar = notlar;
            this.Yazar = yazar;
            this.Tur = tur;
            this.Sayfa = sayfa;
            this.IsRead = isRead;
        }
    }
}
