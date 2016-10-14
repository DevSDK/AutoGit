﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Octokit;
namespace GitHubAutoManager
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {

        private object sync = new object();
        private User CurrentUser = null;
        private GitHubClient Client =  new GitHubClient(new ProductHeaderValue("AutoGit")) ;
        private bool isLogined = false;



        private async Task<bool> Login(string id , string pw)
        {
            bool logned = false;
            try
            {
                Credentials basicAuth = new Credentials(id, pw);
                Client.Credentials = basicAuth;
              
                CurrentUser = await Client.User.Current();
            }
            catch (AuthorizationException exp)
            {
                LoginInfo.Text = "login failed wrong id/pw";
                LoginInfo.Visibility = Visibility.Visible;
                Storyboard LoginSlide = (Storyboard)FindResource("LoginFall_Wrong_ID_PW");
                LoginSlide.Begin(this);
                return true;
            }
            catch(ArgumentException exp)
            {
                LoginInfo.Text = "Pleas write id/pw";
                LoginInfo.Visibility = Visibility.Visible;
                Storyboard LoginSlide = (Storyboard)FindResource("LoginFall_Wrong_ID_PW");
                LoginSlide.Begin(this);
                return false;
            }
                return true;
          }
        private void LoginSecces()
        {
            LoginGrid.Visibility = Visibility.Hidden;
            ProfileGrid.Visibility = Visibility.Visible;
            UI_UserName.Text = CurrentUser.Name;

        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            isLogined = await Login(UI_ID.Text, UI_Password.Password);
            if (isLogined == true)
                LoginSecces();
        }
  
        private async void UI_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;
            isLogined = await Login(UI_ID.Text, UI_Password.Password);
            if (isLogined == true)
                LoginSecces();

        }



        //////////////////// UX/UI ////////////////////


        private bool isShowedLogin = false;
       



        private void BUTTON_TEST_Click(object sender, RoutedEventArgs e)
        {
            if(isLogined == false && isShowedLogin == false)
            {
                Storyboard LoginSlide = (Storyboard)FindResource("ShowLogin");
                LoginGrid.Visibility = Visibility.Visible;
                LoginSlide.Begin(this);
            }

        }


        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            if (isShowedLogin == false)
                return;
            Storyboard LoginSlide = (Storyboard)FindResource("HideLogin");
           

            LoginSlide.Begin(this);
        }

        private void ShowLoginCompleted(object sender, EventArgs e)
        {
            isShowedLogin = true;
        }
        private void HideLoginCompleted(object sender, EventArgs e)
        {
            isShowedLogin = false;
        }


        //////////////////// TitleBar ////////////////////
        private void Button_ApplicationExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_ApplicationHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }






        public MainWindow()
        {
            InitializeComponent();
        }

   
    }
}
