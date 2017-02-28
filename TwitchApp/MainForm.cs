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
            variables.twitchLinked = false;
        }

        public void fetchTwitchData()
        {
            Console.WriteLine("Fetching Twitch Data...");
            fetchTwitchUsername();
            fetchTwitchStatus();
        }

        private void fetchTwitchUsername()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                Console.WriteLine("Fetching Twitch Username...");
                string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/user?oauth_token=" + variables.access_token);
                twitchUserAPI twitchUser = JsonConvert.DeserializeObject<twitchUserAPI>(jsonString);

                variables.display_name = twitchUser.display_name;
                webClientRunning = false;
            }
        }

        private void fetchTwitchStatus()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                if(variables.display_name == null)
                {
                    fetchTwitchUsername();
                }
                else
                {
                    Console.WriteLine("Fetching Twitch Stream Status...");
                    string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                    twitchUsersAPI.Channel twitchUsers = JsonConvert.DeserializeObject<twitchUsersAPI.Channel>(jsonString);
                    Console.WriteLine(jsonString);
                    webClientRunning = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebForm webForm = new WebForm();
            webForm.Show();
            
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if(variables.access_token == null)
            {
                MessageBox.Show("Please Authenticate Your Twitch Account");
            }else
            {
                fetchTwitchData();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fetchTwitchStatus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (variables.access_token == null)
            {
                MessageBox.Show("Please Authenticate Your Twitch Account First");
            }
            else
            {
                if (variables.status == null)
                {
                    MessageBox.Show("Twitch Channel Offline");
                }
                else
                {
                    MessageBox.Show("Twitch Title: " + variables.status);
                }
            }
        }
    }
}
