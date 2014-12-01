using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;
using Shared.Messages;

namespace ThortonsHL7
{
    public partial class ServiceChooser : Form
    {
        public ServiceChooser()
        {
            InitializeComponent();

            //Dictionary<string, string> teamInfo = Comms.GetServices();

        }

        private void buttonPurchaseTotaller_Click(object sender, EventArgs e)
        {
            new PurchaseTotallerInput().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Attempt to register team (needs testing)
            string registerTeamStr = RegisterTeam.GenerateMessage(textBoxTeamName.Text);
            string response = SocketClient.RegisterTeam(registerTeamStr);
            bool registerResponse = false;
            try {
                registerResponse = RegisterTeam.ParseMessage(response);
            } catch { }

            // Check response for OK/FAIL
            if (!registerResponse)
            {
                MessageBox.Show("Error registering team: [" + RegisterTeam.GetErrorCode() + "] " + RegisterTeam.GetErrorMessage());
            }
            else
            {
                buttonPurchaseTotaller.Enabled = true;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
