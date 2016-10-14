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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Octokit;
using System.Runtime.CompilerServices;

namespace GitHubAutoManager
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        private object sync =  new object();
        private GitHubClient client;
        private User user;

        public Window1()
        {
            InitializeComponent();
            client = new GitHubClient(new ProductHeaderValue("Test"));
        }

       
        private async void button_Click(object sender, RoutedEventArgs e)
        {

            var basicAuth = new Credentials(UI_ID.Text, UI_Password.Password); 
            client.Credentials = basicAuth;

            User _user = await client.User.Current();

            lock (sync)
                user = _user;
            
        


        }
    }
}
