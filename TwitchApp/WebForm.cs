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
        Variables variables = new Variables();
        MainForm mainForm = new MainForm();

        public WebForm()
        {
            InitializeComponent();
            webBrowser2.Navigate("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=r7n8hh5hxsostgcl6ikzuetxrqyprd&redirect_uri=http://westie1010.xyz&scope=channel_editor+user_read&state=abc123");
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            variables.twitchURL = webBrowser2.Url.Fragment;
            Console.WriteLine(variables.twitchURL);
        }
    }
}
