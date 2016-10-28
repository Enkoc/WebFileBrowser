using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebFileBrowser.Models;

namespace WebFileBrowser.Controllers
{
    public class FileBrowserController : ApiController
    {
        
        public List<string> GetDisks()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<string> diskNames = new List<string>();

            foreach (var d in allDrives)
            {
                diskNames.Add(d.Name);
            }

            return diskNames;
        }


        public Folder GetNextFolder(string path)
        {                        
            Folder fol = new Folder(path);
            return fol;
        }       
    }
}
