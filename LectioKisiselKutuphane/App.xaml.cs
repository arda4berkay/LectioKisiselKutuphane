using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace LectioKisiselKutuphane
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //databasei pathı
        static string databaseName = "Lectio.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);

        
    }

}
