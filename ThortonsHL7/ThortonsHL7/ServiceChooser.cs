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
        public string teamName;

        public ServiceChooser()
        {
            InitializeComponent();

            //Dictionary<string, string> teamInfo = Comms.GetServices();

        }

        private void buttonPurchaseTotaller_Click(object sender, EventArgs e)
        {
            teamName = textBoxTeamName.Text;
            string queryString = QueryService.GenerateMessage(teamName, RegisterTeam.GetTeamID(), "GIORP-TOTAL");
            Logger.Log("Sending query service message: " + queryString);
            // Still need to set up sockets properly
            // string response = SocketClient.QueryService();
            // Logger.Log("Recieved response: " + response);
            bool queryResponse = false;
            try
            {
                // queryResponse = QueryService.ParseMessage(response);
            }
            catch { }

            if (!queryResponse)
            {
                MessageBox.Show("Error querying service: [" + QueryService.GetErrorCode() + "] " + QueryService.GetErrorMessage());
                Logger.Log("Error querying service: [" + QueryService.GetErrorCode() + "] " + QueryService.GetErrorMessage());
            }
            else
            {
                new PurchaseTotallerInput().Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Attempt to register team (needs testing)
            teamName = textBoxTeamName.Text;
            string registerTeamStr = RegisterTeam.GenerateMessage(teamName);
            Logger.Log("Sending team register message: " + registerTeamStr);
            string response = SocketClient.RegisterTeam(registerTeamStr);
            Logger.Log("Recieved response: " + response);
            bool registerResponse = false;
            try {
                registerResponse = RegisterTeam.ParseMessage(response);
            } catch { }

            // Check response for OK/FAIL
            if (!registerResponse)
            {
                MessageBox.Show("Error registering team: [" + RegisterTeam.GetErrorCode() + "] " + RegisterTeam.GetErrorMessage());
                Logger.Log("Error registering team: [" + RegisterTeam.GetErrorCode() + "] " + RegisterTeam.GetErrorMessage());
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
