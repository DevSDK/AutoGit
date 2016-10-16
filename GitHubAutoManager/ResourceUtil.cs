using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAutoManager
{
    static class ResourceUtil
    {
        public static void CheckAndCreateDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                di.Create();
            }
        }
        public static void DownloadPng(string uri, string filepath)
        {


            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(new Uri(uri));

                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (var image = Image.FromStream(ms))
                    {
                        image.Save(filepath, ImageFormat.Png);
                    }
                }
            }
        }

         


    }
}
