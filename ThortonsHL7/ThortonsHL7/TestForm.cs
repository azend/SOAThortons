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
            MessageBox.Show("Testing Response(Should throw exception): SOA|OK||18:00:05|| \nTest Passed: " + RegisterTeam.ParseMessage("SOA|OK||18:00:05||") + "\nTeamID: " + RegisterTeam.GetTeamID() + "\nExpiry " + RegisterTeam.GetExpiry());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test Passed: " + UnregisterTeam.ParseMessage(""));
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
