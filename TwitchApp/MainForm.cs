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
using System.Diagnostics;
using System.Data.SQLite;
using RestSharp;

namespace TwitchTV_App
{
    public partial class MainForm : Form
    {

        Variables variables = Program.Variables;

        WebClient webClient;
        SQLiteConnection m_dbConnection;
        bool webClientRunning;

        public MainForm()
        {
            InitializeComponent();
            webClient = new WebClient();
            variables.twitchLinked = false;
            m_dbConnection = new SQLiteConnection("Data Source=Games.sqlite;Version=3");
            //Download Games database
            webClient.DownloadFile("http://projects.kieranwest.me/TwitchTV-App/Games.sqlite", "Games.sqlite");
            Console.WriteLine("Database Downloaded!");
        }

        public void fetchTwitchData()
        {
            while(variables.twitchLinked)
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

                //Update Game Title
                updateGameTitle();
                labelCurrentGame.Invoke((MethodInvoker)(() => labelCurrentGame.Text = "Current Game: " + variables.gameName));
                Thread.Sleep(30000);

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

        private void fetchProcesses()
        {
            Process[] processList = Process.GetProcesses();

            m_dbConnection.Open();
            foreach (Process process in processList)
            {
                string processName = process.ProcessName;

                string sql = "select * from games";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (processName == reader["processName"].ToString())
                    {
                        variables.gameProcessName = processName;
                        variables.gameName = reader["gameName"].ToString();
                        break;
                    }
                }
            }

            m_dbConnection.Close();
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

        private void updateGameTitle()
        {

            if(variables.twitchLinked)
            {
                fetchProcesses();
                var client = new RestClient("https://api.twitch.tv/kraken/channels/" + variables.display_name);
                var request = new RestRequest(Method.PUT);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "OAuth " + variables.access_token);
                request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
                request.AddParameter("application/json", "{\"channel\":{\"game\":\"" + variables.gameName + "\"}}",
                    ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
            }
            else
            {
                MessageBox.Show("Link Twitch Account");
            }
            
        }
    }
}
