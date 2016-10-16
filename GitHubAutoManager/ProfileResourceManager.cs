using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace GitHubAutoManager
{
    static class ProfileResourceManager
    {
        private static User targetUser;
        public static readonly string RootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AutoGit" + "\\UserData" + "\\";
        public static void setUser(User user)
        {
            targetUser = user;
        }
        public static void DownloadProfile()
        {

            string path = RootPath+ targetUser.Id+"\\";

            ResourceUtil.CheckAndCreateDirectory(path);
            ResourceUtil.DownloadPng(targetUser.AvatarUrl, path + "UserAvatar.png");
           

        }

        


    }
}
