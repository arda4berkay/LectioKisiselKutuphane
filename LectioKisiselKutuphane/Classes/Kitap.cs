using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LectioKisiselKutuphane.Classes
{
    class Kitap
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

    }
}
