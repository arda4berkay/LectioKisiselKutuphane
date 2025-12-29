using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LectioKisiselKutuphane.Classes
{
    public class Kitap
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Yazarp { get; set; }
        public string Yayinci { get; set; }
        public string Tur { get; set; }
        public int IkonNo { get; set; }
        public int Sayfa { get; set; }
        public int Puan { get; set; }
        public bool isRead { get; set; }

        public Kitap()
        {
            Random random = new Random();
            var ikonNo = random.Next(0, 8);

            this.IkonNo =ikonNo;
        }
    }
}
