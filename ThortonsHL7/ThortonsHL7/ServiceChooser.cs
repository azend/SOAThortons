/*
 * FILE        : ServiceChooser.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : Main form for the project, contains buttons that allow the user to register a team, unregister a team, 
 *               query/execute the GIORP Purchase Totaller service, and exit.
 */
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
        Dictionary<string, string> teamInfo;

        public ServiceChooser()
        {
            InitializeComponent();
        }

        private void buttonRegisterTeam_Click(object sender, EventArgs e)
        {
            teamName = textBoxTeamName.Text;

            this.teamInfo = Comms.RegisterTeam(teamName);

            if (teamInfo != null)
            {
               // MessageBox.Show(String.Format("Name: {0}\nID: {1}\nExpiry: {2}", teamInfo["Name"], teamInfo["ID"], teamInfo["Expiry"]));
                buttonPurchaseTotaller.Enabled = true;
            }
            else
            {
                MessageBox.Show("Your team name could not be registered.");
            }
        }

        private void buttonUnregisterTeam_Click(object sender, EventArgs e)
        {
            if (teamInfo != null)
            {
                int teamID = -1;
                Int32.TryParse(teamInfo["ID"], out teamID);
                bool result = Comms.UnregisterTeam(teamInfo["Name"], teamID);

                MessageBox.Show(String.Format("Result: {0}\n", result));
            }
        }

        private void buttonPurchaseTotaller_Click(object sender, EventArgs e)
        {
            if (teamInfo != null)
            {
                Dictionary<string, string> serviceInfo = Comms.QueryService(teamInfo);

               // MessageBox.Show(String.Format("Name: {0}\nIP Address: {1}\nPort: {2}\n", serviceInfo["Name"], serviceInfo["IPAddress"], serviceInfo["Port"]));
                this.Hide();
                new PurchaseTotallerInput().ShowDialog();
                if(QueryService.GetSuccess())
                {
                    new PurchaseTotallerResults().ShowDialog();
                }
                
                this.Show();
                this.TopMost = true;
                this.Focus();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
