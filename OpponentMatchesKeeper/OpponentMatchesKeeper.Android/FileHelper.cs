using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(OpponentMatchesKeeper.Droid.FileHelper))]
namespace OpponentMatchesKeeper.Droid
{

    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }

}