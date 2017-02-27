using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {

        Variables variables = Program.Variables;

        WebClient webClient;
        bool webClientRunning;

        public MainForm()
        {
            InitializeComponent();
            webClient = new WebClient();
            Thread fetchTwitchUsernameThread = new Thread(new ThreadStart(fetchTwitchUsername));
            fetchTwitchUsernameThread.Start();
        }

        private void fetchTwitchUsername()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                if (variables.access_token == null)
                {
                    Console.WriteLine("Log In First");
                    Thread.Sleep(5000);
                }
                else
                {
                    string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/user?oauth_token=" + variables.access_token);
                    twitchUserAPI twitchUser = JsonConvert.DeserializeObject<twitchUserAPI>(jsonString);

                    variables.display_name = twitchUser.display_name;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebForm webForm = new WebForm();
            webForm.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(variables.display_name);
        }
    }
}
