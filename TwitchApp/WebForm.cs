using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WebForm : Form
    {
        Variables variables = Program.Variables;
        MainForm mainForm = new MainForm();

        public WebForm()
        {
            InitializeComponent();
            webBrowser2.Navigate("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=n34bthzktntu43c8fskvfl3hdt4adp&redirect_uri=http://localhost&scope=channel_editor+user_read&state=abc123");
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            variables.twitchURL = webBrowser2.Url.Fragment;
            variables.access_token = variables.twitchURL.Split('=', '&')[1];
            Console.WriteLine(variables.access_token);
            this.Close();
        }
    }
}
