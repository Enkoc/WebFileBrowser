using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebFileBrowser.Models
{
    public class Folder
    {
        List<FileInfo> fileInfoList = new List<FileInfo>();
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Files { get; set; }
        public int LessThan10MbFiles { get; set; }
        public int LessThan10MoreThan50MbFiles { get; set; }
        public int MoreThan100MbFiles { get; set; }

        public Folder(string path)
        {
            try 
            {
                Path = path;
                Files = Directory.GetFiles(path).ToList();
                Files.AddRange(Directory.GetDirectories(path).ToList());

                Name = new DirectoryInfo(path).Name;

                ApplyAllFiles(path, ProcessFile);

                LessThan10MbFiles = fileInfoList.Count(f => f.Length <= 10 * Math.Pow(2, 20));
                LessThan10MoreThan50MbFiles = fileInfoList.Count(f => f.Length > 10 * Math.Pow(2, 20) && f.Length <= 50 * Math.Pow(2, 20));
                MoreThan100MbFiles = fileInfoList.Count(f => f.Length >= 100 * Math.Pow(2, 20));
            }
            catch(Exception ex)
            {
                //When try to read file or access to file or folder denied
                throw ex;
            }          
        }

        void ProcessFile(string path)
        {
            fileInfoList.Add(new FileInfo(path));
        }

        //Find all files from dirs and subdirs. And doing some actoin with each file
        void ApplyAllFiles(string folder, Action<string> fileAction)
        {
            foreach (string file in Directory.GetFiles(folder))
            {
                fileAction(file);
            }
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    ApplyAllFiles(subDir, fileAction);
                }
                catch
                {

                }
            }
        }
    }
}
