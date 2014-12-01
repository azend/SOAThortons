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
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterTeam.ParseMessage("SOA|OK|1180|18:00:05||");
            MessageBox.Show("Testing Response: SOA|OK|1180|18:00:05|| \nReturn: " + RegisterTeam.ParseMessage("SOA|OK|1180|18:00:05||") + "\nTeamID: " + RegisterTeam.GetTeamID() + "\nExpiry " + RegisterTeam.GetExpiry());
            MessageBox.Show("Testing Response: SOA|NOT-OK|-1|Message doesn't contain EOM marker|| \nReturn: " + RegisterTeam.ParseMessage("SOA|NOT-OK|-1|Message doesn't contain EOM marker||"));
            MessageBox.Show("Testing Response(Missing a perameter): SOA|OK||18:00:05|| \nReturn: " + RegisterTeam.ParseMessage("SOA|OK||18:00:05||") + "\nTeamID: " + RegisterTeam.GetTeamID() + "\nExpiry " + RegisterTeam.GetExpiry());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testing Response: SOA|OK|1180|18:00:05|| \nReturn: " + UnregisterTeam.ParseMessage("SOA|OK|1180|18:00:05||"));
            MessageBox.Show("Testing Response: SOA|NOT-OK|-2|DRC/UNREG-TEAM segment not according to Spec. || \nReturn: " + UnregisterTeam.ParseMessage("SOA|NOT-OK|-2|DRC/UNREG-TEAM segment not according to Spec. ||"));
            MessageBox.Show("Testing Response(Missing a perameter): SOA|OK|1180||| \nReturn: " + UnregisterTeam.ParseMessage("SOA|OK|1180|||"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testing Response: SOA|OK|||9|SRV|TeamFreelancer|totalPurchase||2|5|Returns a calculated amount of money| ARG|1|Province|string|mandatory|| ARG|2|totalMoney|float|mandatory|| RSP|1|PST|string||RSP|2|HST|string||RSP|3|GST|string||RSP|4|SubTotalAmount|string||RSP|5|TotalPurchaseAmount|string||MCH|142.112.50.103|50002|   \nReturn: " + QueryService.ParseMessage("SOA|OK|||9|SRV|TeamFreelancer|totalPurchase||2|5|Returns a calculated amount of money| ARG|1|Province|string|mandatory|| ARG|2|totalMoney|float|mandatory|| RSP|1|PST|string||RSP|2|HST|string||RSP|3|GST|string||RSP|4|SubTotalAmount|string||RSP|5|TotalPurchaseAmount|string||MCH|142.112.50.103|50002| Server" ));
            MessageBox.Show("Testing Response: SOA|NOT-OK|-1|SOA command <whatever you sent> - UNKNOWN|| \nReturn: " + UnregisterTeam.ParseMessage("SOA|NOT-OK|-1|SOA command <whatever you sent> - UNKNOWN||"));           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Testing Response: PUB|OK|||5|RSP|1|PST|string|0>|RSP|2|HST|string|13|RSP|3|GST|string|0|RSP|4|SubTotalAmount|string|100|RSP|5|TotalPurchaseAmount|string|113| \nReturn: " + ExecuteService.ParseMessage("PUB|OK|||5|RSP|1|PST|string|0>|RSP|2|HST|string|13|RSP|3|GST|string|0|RSP|4|SubTotalAmount|string|100|RSP|5|TotalPurchaseAmount|string|113|"));
            MessageBox.Show("Testing Response: SOA|NOT-OK|-4|No team '<teamName>' (ID : <teamID>) found registered in Dbase|| \nReturn: " + ExecuteService.ParseMessage("SOA|NOT-OK|-4|No team '<teamName>' (ID : <teamID>) found registered in Dbase ||"));  
        }
    }
}
