using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThortonsHL7
{
    public partial class PurchaseTotallerResults : Form
    {
        public PurchaseTotallerResults()
        {
            InitializeComponent();

            string[] buffer = new string[Convert.ToInt32(Shared.Messages.QueryService.GetNumArgs())];

            for (int x = 0; x < Convert.ToInt32(Shared.Messages.QueryService.GetNumArgs()); x++)
            {
                buffer[x] = Shared.Messages.ExecuteService.GetRSPValue()[x];
            }
            DisplayResults(buffer);
        }

        public void DisplayResults(string[] results)
        {         
            // Display information
            subtotalTextbox.Text = results[0];
            pstTextbox.Text = results[1];
            gstTextbox.Text = results[2];
            hstTextbox.Text = results[3];
            totalTextbox.Text = results[4];
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
