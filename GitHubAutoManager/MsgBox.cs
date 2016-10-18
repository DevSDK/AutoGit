using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace GitHubAutoManager
{
    static class MsgBox
    {
        private static MainWindow TargetWindow = null;
        private static bool isInialize = false;
        public static void Initalize(MainWindow window)
        {
            if (isInialize)
                return;
            TargetWindow = window;

            TargetWindow.MessageBox_Button_Ok.Click += MeesageBox_Ok_Click;
            TargetWindow.MessageBox_Button_Yes.Click += MeesageBox_Yes_Click;
            TargetWindow.MessageBox_Button_No.Click += MeesageBox_No_Click;

            isInialize = true;
        }
        private static void MeesageBox_Ok_Click(object sender, RoutedEventArgs e)
        {

        }
        private static void MeesageBox_Yes_Click(object sender, RoutedEventArgs e)
        {

        }
        private static void MeesageBox_No_Click(object sender, RoutedEventArgs e)
        {

        }
        public static void Show(string msg)
        {

        }
        public static void Show(string msg, string title)
        {

        }
        public static bool ShowYesNo(string msg)
        {
            return false;

        }
        public static bool ShowYesNo(string msg, string title)
        {
            return false;

        }
    }
}
