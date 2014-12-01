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

            //Dictionary<string, string> teamInfo = Comms.RegisterTeam(teamName);
            Dictionary<string, string> teamInfo = null;

            if (teamInfo != null)
            {
                buttonPurchaseTotaller.Enabled = true;
            }
            else
            {
                MessageBox.Show("Your team name could not be registered.");
            }
            

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
