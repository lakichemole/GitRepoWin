using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace GitRepo.UI.Common
{
    public class StorageManager
    {

        private StorageManager()
        {
        }

        #region SingleTon

        private static volatile StorageManager instance;
        private static object syncRoot = new Object();

        public static StorageManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new StorageManager();
                    }
                }

                return instance;
            }

        }
        #endregion

        public async Task SaveStringToLocalFile(string filename, string content)
        {
            // saves the string 'content' to a file 'filename' in the app's local storage folder
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(content.ToCharArray());

            // create a file with the given filename in the local folder; replace any existing file with the same name
            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            // write the char array created from the content string into the file
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                await stream.WriteAsync(fileBytes, 0, fileBytes.Length);
            }
        }

        public async Task<string> ReadStringFromLocalFile(string filename)
        {
            // reads the contents of file 'filename' in the app's local storage folder and returns it as a string

            string text = string.Empty;
            // access the local folder
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            // open the file 'filename' for reading
            if (await FileExist(local, filename))
            {

                Stream stream = await local.OpenStreamForReadAsync(filename);

                // copy the file contents into the string 'text'
                using (StreamReader reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }

            return text;
        }

        public async Task<bool> FileExist(StorageFolder folder, string fileName)
        {
            bool exist = true;
            try
            {
                await folder.GetFileAsync(fileName);
            }
            catch (Exception)
            {
                exist = false;
            }
            return exist;
        }


    }
}
