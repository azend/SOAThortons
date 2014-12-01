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
    public partial class TestForm : Form
    {

        private Dictionary<string, string> teamInfo = null;
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            RegisterTeam.ParseMessage("SOA|OK|1180|18:00:05||");
            MessageBox.Show("Testing Response: SOA|OK|1180|18:00:05|| \nReturn: " + RegisterTeam.ParseMessage("SOA|OK|1180|18:00:05||") + "\nTeamID: " + RegisterTeam.GetTeamID() + "\nExpiry " + RegisterTeam.GetExpiry());
            MessageBox.Show("Testing Response: SOA|NOT-OK|-1|Message doesn't contain EOM marker|| \nReturn: " + RegisterTeam.ParseMessage("SOA|NOT-OK|-1|Message doesn't contain EOM marker||")); 
            MessageBox.Show("Testing Response(Should throw exception): SOA|OK||18:00:05|| \nTest Passed: " + RegisterTeam.ParseMessage("SOA|OK||18:00:05||") + "\nTeamID: " + RegisterTeam.GetTeamID() + "\nExpiry " + RegisterTeam.GetExpiry());
            */

            Dictionary<string, string> teamInfo = Comms.RegisterTeam("Freelancer");
            MessageBox.Show(String.Format("Name: {0}\nID: {1}\nExpiry: {2}", teamInfo["Name"], teamInfo["ID"], teamInfo["Expiry"]));

            this.teamInfo = teamInfo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (teamInfo != null)
            {
                int teamID = -1;
                Int32.TryParse(teamInfo["ID"], out teamID);
                bool result = Comms.UnregisterTeam(teamInfo["Name"], teamID);

                MessageBox.Show(String.Format("Result: {0}\n", result));
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (teamInfo != null)
            {
                Dictionary<string, string> serviceInfo = Comms.QueryService(teamInfo);

                MessageBox.Show(String.Format("Name: {0}\nIP Address: {1}\nPort: {2}\n", serviceInfo["Name"], serviceInfo["IPAddress"], serviceInfo["Port"]));
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            MessageBox.Show("Testing Response: PUB|OK|||5|RSP|1|PST|string|0>|RSP|2|HST|string|13|RSP|3|GST|string|0|RSP|4|SubTotalAmount|string|100|RSP|5|TotalPurchaseAmount|string|113| \nReturn: " + ExecuteService.ParseMessage("PUB|OK|||5|RSP|1|PST|string|0>|RSP|2|HST|string|13|RSP|3|GST|string|0|RSP|4|SubTotalAmount|string|100|RSP|5|TotalPurchaseAmount|string|113|"));
            MessageBox.Show("Testing Response: PUB|NOT-OK|-4|No team '<teamName>' (ID : <teamID>) found registered in Dbase|| \nReturn: " + ExecuteService.ParseMessage("PUB|NOT-OK|-4|No team '<teamName>' (ID : <teamID>) found registered in Dbase||"));  
         * */
        }
    }
}
