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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Json;
using Microsoft.Win32;

namespace lewd_dot_se_Uploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            DiscordRPC.StopDiscordRPC();
            Process.GetCurrentProcess().Kill();
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog FileSelect = new OpenFileDialog();
            if (FileSelect.ShowDialog() == true)
            {
                string Path = System.IO.Path.GetDirectoryName(FileSelect.FileName);

                WebClient wc = new WebClient();
                wc.Headers.Set("token", token.Text);
                byte[] resultBytes = wc.UploadFile("https://lewd.se/upload", System.IO.Path.Combine(Path, FileSelect.FileName));
                string response = System.Text.Encoding.ASCII.GetString(resultBytes);

                JsonValue json = JsonValue.Parse(response);
                JsonObject result = json as JsonObject;

                error errorWindow = new error();
                errorWindow.Owner = this;

                uploadSuccess successWindow = new uploadSuccess();
                successWindow.Owner = this;

                if (resultBytes.ToString() == "You need to be signed in to upload")
                {
                    errorWindow.Show();
                }
                else
                {
                    successWindow.Show();
                    Properties.Settings.Default.token = token.Text;
                    Properties.Settings.Default.Save();

                    string link = result["data"]["link"];
                    Clipboard.SetText(link);
                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DiscordRPC.StartDiscordRPC();
            if (Properties.Settings.Default.token == "Token")
            {

            }
            else
            {
                token.Text = Properties.Settings.Default.token;
            }
        }

        private void discord_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discordapp.com/invite/AKGeqgN");
        }

        private void github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("github.com");
        }
    }
}
