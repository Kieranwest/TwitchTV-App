using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchTV_App
{
    public partial class WebForm : Form
    {
        Variables variables = Program.Variables;
        MainForm mainForm;

        public WebForm(MainForm m)
        {
            mainForm = m;
            InitializeComponent();
            webBrowser2.ScriptErrorsSuppressed = true;
            Uri twitchAuthURL = new Uri("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=n34bthzktntu43c8fskvfl3hdt4adp&redirect_uri=http://localhost&scope=channel_editor+user_read+chat_login&state=abc123");
            webBrowser2.Navigate(twitchAuthURL);
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = webBrowser2.DocumentTitle;
            if(webBrowser2.Url.Host != "localhost")
            {
                Console.WriteLine("WebForm: Not there yet.");
            }
            else
            {
                variables.twitchURL = webBrowser2.Url.Fragment;
                variables.access_token = variables.twitchURL.Split('=', '&')[1];
                Console.WriteLine("Access Token: " + variables.access_token);
                variables.twitchLinked = true;
                Thread fetchTwitchData = new Thread(new ThreadStart(mainForm.fetchTwitchData));
                fetchTwitchData.IsBackground = true;
                fetchTwitchData.Start();
                Close();
            }
        }

        private void WebForm_Load(object sender, EventArgs e)
        {

        }
    }
}
