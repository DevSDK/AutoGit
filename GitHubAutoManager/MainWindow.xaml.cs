using System;
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
using System.Net.Http;
using LibGit2Sharp;
using System.Data.SQLite;
using System.IO;

namespace GitHubAutoManager
{

    public partial class MainWindow : Window
    {

        private object sync = new object();
        private Octokit.User CurrentUser = null;
        IReadOnlyList<Octokit.Repository> UserRepos = null;
        SQLiteConnection SQLConnection;
        private Octokit.GitHubClient Client = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("AutoGit"));
        private bool isLogined = false;

        private bool MessageShowYesNo(string msg)
        {
            return true;
        }


        private async Task<bool> Login(string id, string pw)
        {
            try
            {

                Octokit.Credentials basicAuth = new Octokit.Credentials(id, pw);
                Client.Credentials = basicAuth;
                CurrentUser = await Client.User.Current();
            }
            catch (Octokit.AuthorizationException exp)
            {
                LoginInfo.Text = "login failed wrong id/pw";
                LoginInfo.Visibility = Visibility.Visible;
                Storyboard LoginSlide = (Storyboard)FindResource("LoginFall_Wrong_ID_PW");
                UI_Password.Password = "";
                LoginSlide.Begin(this);
                return false;
            }
            catch (ArgumentException exp)
            {
                LoginInfo.Text = "Pleas write id/pw";
                LoginInfo.Visibility = Visibility.Visible;
                Storyboard LoginSlide = (Storyboard)FindResource("LoginFall_Wrong_ID_PW");
                LoginSlide.Begin(this);
                UI_Password.Password = "";
                return false;
            }
            catch (HttpRequestException exp)
            {
                MsgBox.Show("No Connection Internet");
                return false;
            }

            UI_Password.Password = "";
            return true;
        }
        private void LoadRepositorys()
        {
            foreach (Octokit.Repository rp in UserRepos)
            {
                TestRepo.Items.Add(rp.FullName);
            }
        }

        private async void LoginSecces()
        {
            LoginGrid.Visibility = Visibility.Hidden;
            ProfileGrid.Visibility = Visibility.Visible;
            UI_UserName.Text = CurrentUser.Name + "'s ProFile";
            UI_USERID.Text = CurrentUser.Login; ;
            UI_About.Text = CurrentUser.Bio;
            Button_Login.Content = "Profile";
            ProfileResourceManager.setUser(CurrentUser);
            Avator.Source = new BitmapImage(new Uri(CurrentUser.AvatarUrl));

            UserRepos = await Client.Repository.GetAllForUser(CurrentUser.Login);
            TestRepo.Items.Clear();
            LoadRepositorys();
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

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (isLogined == false && isShowedLogin == false)
            {
                LoginGrid.Visibility = Visibility.Visible;
                Storyboard LoginSlide = (Storyboard)FindResource("ShowLeftGrid");
                LoginSlide.Begin(this);
            }
            if (isLogined == true && isShowedLogin == false)
            {
                Storyboard LoginSlide = (Storyboard)FindResource("ShowLeftGrid");
                LoginSlide.Begin(this);
            }
        }
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            if (isShowedLogin == false)
                return;
            RepositoryMenuGrid.Visibility = Visibility.Hidden;
            if (MenuList.SelectedItem != null)
                ((ListBoxItem)MenuList.SelectedItem).IsSelected = false;

            Storyboard LoginSlide = (Storyboard)FindResource("HideLeftGrid");
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
            MsgBox.Initalize(this);
        }
        private void Logout()
        {
            CurrentUser = null;
            // Client.Credentials = null;//TODO: WHY EXCEPTION?
            isLogined = false;
            Button_Login.Content = "Login";
            ProfileGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;

        }
        LibGit2Sharp.Repository localrepo; //TEST 
        private void button_Click(object sender, RoutedEventArgs e)
        {

            /*var dialog = new System.Windows.Forms.FolderBrowserDialog() { ShowNewFolderButton = false };
    
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            localrepo = new LibGit2Sharp.Repository(dialog.SelectedPath);
            TreeChanges changs = localrepo.Diff.Compare<TreeChanges>(localrepo.Head.Tip.Tree, DiffTargets.Index | DiffTargets.WorkingDirectory);
            foreach (var item in localrepo.RetrieveStatus())
            {
                if (item.State == FileStatus.ModifiedInWorkdir)
                {
                     var patch = localrepo.Diff.Compare<Patch>(new List<string>() { item.FilePath });
                    Console.WriteLine("~~~~ {0} ~~~~",item.FilePath);
                    Console.WriteLine(patch.Content);
                }
            }
                foreach (LibGit2Sharp.Commit item in localrepo.Commits)
            {
                Console.WriteLine(item.Id + "mes: " + item.Message);
            }
            */

            AddLocalRepository("Test", @"data.sdf");
        }

        private void LogoutAskSlsect(bool selected)
        {
            if (selected)
            {
                Logout();
            }
            ((ListBoxItem)MenuList.SelectedItem).IsSelected = false;
        }
        private void UI_MenuList_Logout_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (MenuList.SelectedItem == null)
                return;
            string selectionTag = (string)((ListBoxItem)MenuList.SelectedItem).Tag;
            switch (selectionTag)
            {
                case "LogOut":
                    MsgBox.ShowYesNo("Are You Sure?", LogoutAskSlsect);
                    break;
                case "Repositories":
                    RepositoryMenuGrid.Visibility = Visibility.Visible;

                    break;
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }
        private void CreateDBFile()
        {

            SQLiteConnection.CreateFile("data.db");


        }
        private void AddLocalRepository(string name, string path)
        {



 
            else
            {
                SQLConnection = new SQLiteConnection("Data Source=data.db;Version=3;");
                SQLConnection.Open();
            }

            using (SQLiteCommand command = SQLConnection.CreateCommand())
            {
                command.CommandText = "select count(*) from sqlite_master Where Name = 'Local_RepoTable';";
                command.CommandType = System.Data.CommandType.Text;
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(Convert.ToString(rdr["data.db"]));
                }
            }


        }


    }
}
