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
using System.Net.Sockets;
using System.IO;

namespace TwitchTV_App
{
    public partial class MainForm : Form
    {

        Variables variables = Program.Variables;

        //IRC
        #region
        IrcClient irc = new IrcClient("irc.chat.twitch.tv", 6667, "justinfan13234452562626");
        NetworkStream serverStream = default(NetworkStream);
        string readData = "";
        Thread chatThread;
        #endregion

        //Web Client
        WebClient webClient;
        SQLiteConnection m_dbConnection;
        bool webClientRunning;

        public MainForm()
        {
            InitializeComponent();
            webClient = new WebClient();
            m_dbConnection = new SQLiteConnection("Data Source=Games.sqlite;Version=3");

            //Download Games database
            webClient.DownloadFile("https://github.com/Kieranwest/TwitchTV-App/raw/master/Games.sqlite", "Games.sqlite");
            Console.WriteLine("Database Downloaded!");

            twitchChat.Text = "Twitch Chat" + Environment.NewLine;

            //Check for previous login. 
            if(File.Exists("credentials.txt"))
            {
                Console.WriteLine("Found previous credentials.");
                //Set required variables. 
                try
                {
                    StreamReader reader = new StreamReader("credentials.txt");
                    string access_token = reader.ReadToEnd();
                    variables.access_token = access_token;
                    variables.twitchLinked = true;
                    twitchLogin.Enabled = false;
                    Thread fetchTwitchData = new Thread(new ThreadStart(this.fetchTwitchData));
                    fetchTwitchData.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("No previous credentials found.");
            }

        }

        public void fetchTwitchData()
        {
          //Testing previous dates
          //and this one
            while(variables.twitchLinked)
            {
                Console.WriteLine("Fetching Twitch Data...");

                //Fetch Twitch Username
                variables.display_name = fetchTwitchUsername();
                Console.WriteLine("Twitch Username: " + variables.display_name);

                //Fetch Twitch Stream Status
                variables.status = fetchTwitchStatus();
                Console.WriteLine("Twitch Stream Status: " + variables.status);

				//Update Game Title
				updateGameTitle();
				labelCurrentGame.Invoke((MethodInvoker)(() => labelCurrentGame.Text = "Current Game: " + variables.gameName));
				Console.WriteLine("Current Game: " + variables.gameName);

				//Fetch Twitch Followers
				variables.followers = fetchTwitchFollowers();
				labelFollowers.Invoke((MethodInvoker)(() => labelFollowers.Text = "Followers: " + variables.followers));
				Console.WriteLine("Followers: " + variables.followers);

                //Fetch Stream Viewers
                variables.viewers = fetchTwitchViewers();
                labelViewers.Invoke((MethodInvoker)(() => labelViewers.Text = "Viewers: " + variables.viewers));
                Console.WriteLine("Viewers: " + variables.viewers);

                //Initiate Twitch Chat
                if(variables.chatStarted == false)
                {
                    joinTwitchChat();
                }

                //string jsonString = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                //dynamic twitchUsersAPI = JsonConvert.DeserializeObject(jsonString);
                //Console.WriteLine(jsonString);

                //Refresh Every 30 Seconds
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

        private string fetchTwitchAPIData()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                string jsonstring = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                dynamic twitchUsersAPI = JsonConvert.DeserializeObject(jsonstring);

                if (variables.status == "Offline")
                {
                    return "null";
                }
                else
                {
                    return jsonstring;
                }

            }
            return "null";
        }

        private int fetchTwitchViewers()
        {
            webClientRunning = true;
            while (webClientRunning)
            {
                string jsonstring = webClient.DownloadString("https://api.twitch.tv/kraken/streams/" + variables.display_name + "?oauth_token=" + variables.access_token);
                dynamic twitchUsersAPI = JsonConvert.DeserializeObject(jsonstring);

                if (variables.status == "Offline")
                {
                    return 0;
                }
                else
                {
                    return twitchUsersAPI.stream.viewers;
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
                Console.WriteLine(processName.ToLower());
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (processName == reader["processName"].ToString().ToLower())
                    {
                        variables.gameProcessName = processName;
                        variables.gameName = reader["gameName"].ToString().ToLower();
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

        private void joinTwitchChat()
        {
            irc.joinRoom(variables.display_name.ToLower());
            chatThread = new Thread(getMessage);
            chatThread.IsBackground = true;
            chatThread.Start();
            variables.chatStarted = true;
        }

        private void getMessage()
        {
            serverStream =  irc.tcpClient.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = irc.tcpClient.ReceiveBufferSize;
            while(variables.twitchLinked)
            {
                try
                {
                    readData = irc.readMessage();
                    msg();
                }
                catch(Exception e)
                {

                }
            }
        }

        //Chat Processing
        private void msg()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                string[] seperator = new string[] { "#" + variables.display_name.ToLower() + " :" };
                string[] singlesep = new string[] { ":" , "!"};

                if(readData.Contains("PRIVMSG"))
                {
                    string username = readData.Split(singlesep, StringSplitOptions.None)[1];
                    string message = readData.Split(seperator, StringSplitOptions.None)[1];

                    twitchChat.AppendText(username + ": " + message + Environment.NewLine);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
		{

		}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(variables.chatStarted)
            {
                irc.leaveRoom();
                serverStream.Dispose();
                Environment.Exit(0);
            }
            else
            {
                irc.leaveRoom();
            }
        }
    }

    class IrcClient
    {
        Variables variables = Program.Variables;

        private string username;
        private string channel;

        public TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        public IrcClient(string ip, int port, string username)
        {
            tcpClient = new TcpClient(ip, port);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());

            //outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + username);
            outputStream.WriteLine("USER " + username + " 8 * :" + username);
            outputStream.WriteLine("CAP REQ :twitch.tv/membership");
            outputStream.WriteLine("CAP REQ :twitch.tv/commands");
            outputStream.Flush();
        }

        public void joinRoom(string channel)
        {
            this.channel = channel;
            outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void leaveRoom()
        {
            outputStream.Close();
            inputStream.Close();
        }

        public void sendIrcMessage(string message)
        {
            outputStream.Write(message);
            outputStream.Flush();
        }

        public void sendChatMessage(string message)
        {
            sendIrcMessage(":" + variables.display_name.ToLower() + "!" + variables.display_name.ToLower() + "@" + variables.display_name.ToLower() + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
        }

        public void pingResponse()
        {
            sendIrcMessage("PONG tmi.twitch.tv\r\n");
        }

        public string readMessage()
        {
            string message = "";
            message = inputStream.ReadLine();
            return message;
        }
    }
}
