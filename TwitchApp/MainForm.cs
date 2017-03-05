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

namespace TwitchTV_App
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
            while(true)
            {
                Console.WriteLine("Fetching Twitch Data...");

                //Fetch Twitch Username
                variables.display_name = fetchTwitchUsername();
                Console.WriteLine("Twitch Username: " + variables.display_name);

                //Fetch Twitch Stream Status
                variables.status = fetchTwitchStatus();
                Console.WriteLine("Twitch Stream Status: " + variables.status);

                //Fetch Twitch Followers
                variables.followers = fetchTwitchFollowers();
                Console.WriteLine("Followers: ");

                labelFollowers.Invoke((MethodInvoker)(() => labelFollowers.Text = "Followers: " + variables.followers.ToString()));

                Thread.Sleep(5000);

            }
        }

        private string fetchTwitchUsername()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/user?oauth_token=" + variables.access_token);
                dynamic twitchUserAPI = JsonConvert.DeserializeObject(jsonString);
                webClientRunning = false;
                return twitchUserAPI.display_name;
            }
            return null;
        }

        private string fetchTwitchStatus()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                    string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                    dynamic twitchUsersAPI = JsonConvert.DeserializeObject(jsonString);

                    if(twitchUsersAPI.stream == null)
                    {
                    webClientRunning = false;
                    return "Offline";
                    }
                    else
                    {
                    webClientRunning = false;
                    return "Online";
                    }
                }
            return null;
        }

        private int fetchTwitchFollowers()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                dynamic twitchUsersAPI = JsonConvert.DeserializeObject(jsonString);
                
                if(variables.status == "Offline")
                {
                    return 0;
                }
                else
                {
                    return twitchUsersAPI.stream.channel.followers;
                }
            }
            return 0;
        }

        private void twitchLogin_Click(object sender, EventArgs e)
        {
            WebForm webForm = new WebForm(this);
            webForm.ShowDialog();
            if (variables.twitchLinked)
            {
                twitchLogin.Enabled = false;
            }
        }

        private void updateLabel_Click(object sender, EventArgs e)
        {
            labelFollowers.Text = "Followers: " + variables.followers.ToString();
        }

        public void changeFollowersLabel()
        {
            
        }

    }
}
