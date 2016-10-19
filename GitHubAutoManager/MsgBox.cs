using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace GitHubAutoManager
{
    static class MsgBox
    {
        private static MainWindow TargetWindow = null;
        private static bool isInialize = false;
        private static bool isShowed = false;
        public static bool isYes { get; private set; }
        public  delegate void MessageYesNo (bool sender);
        private static event MessageYesNo MessageEvent;

        private static void PuaseingThread()
        {
            
        }
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
        private static void ShowMessageBox(string msg,MessageYesNo YesNo)
        {
            TargetWindow.MessageGrid.Visibility = Visibility.Visible;
            TargetWindow.MessageBox_Text.Text = msg;
            ((BlurEffect)TargetWindow.MainGrid.Effect).Radius = 5;
            isShowed = true;
            if(YesNo != null)
              MessageEvent = YesNo;
        }
        private static void HideMessageBox()
        {
            TargetWindow.MessageGrid.Visibility = Visibility.Hidden;

            ((BlurEffect)TargetWindow.MainGrid.Effect).Radius = 0;
            isShowed = false;
        }
        private static void MeesageBox_Ok_Click(object sender, RoutedEventArgs e)
        {
            HideMessageBox();
        }
        private static void MeesageBox_Yes_Click(object sender, RoutedEventArgs e)
        {
            HideMessageBox();
            MessageEvent(true);   
        }
        private static void MeesageBox_No_Click(object sender, RoutedEventArgs e)
        {
            HideMessageBox();
            MessageEvent(false);
        }
        public static void Show(string msg)
        {
            ShowMessageBox(msg,null);
            TargetWindow.Message_Button_Style.Visibility = Visibility.Visible;
           
        }
        public static void Show(string msg, string title)
        {
            TargetWindow.Message_Button_Style.Visibility = Visibility.Visible;
            ShowMessageBox(msg,null);
        }
        public static void ShowYesNo(string msg, MessageYesNo yesno)
        {
            TargetWindow.MessageBox_YesNoStyle.Visibility = Visibility.Visible;
            TargetWindow.Message_Button_Style.Visibility = Visibility.Hidden;
            ShowMessageBox(msg, yesno);
        }
        public static void ShowYesNo(string msg, string title,MessageYesNo yesno)
        {
            TargetWindow.MessageBox_YesNoStyle.Visibility = Visibility.Visible;
            TargetWindow.Message_Button_Style.Visibility = Visibility.Hidden;
            ShowMessageBox(msg, yesno);
        }
    }
}
