/*
 * FILE        : PurchaseTotallerResults.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : A windows form to display results from the service.
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

namespace ThortonsHL7
{
    public partial class PurchaseTotallerResults : Form
    {
        public PurchaseTotallerResults()
        {
            InitializeComponent();

            try
            {
                string[] buffer = new string[Convert.ToInt32(Shared.Messages.QueryService.getNumResponses)];

                for (int x = 0; x < Convert.ToInt32(Shared.Messages.QueryService.getNumResponses); x++)
                {
                    buffer[x] = Shared.Messages.ExecuteService.GetRSPValue()[x];
                }
                DisplayResults(buffer);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error displaying results: " + e.ToString());
                Logger.LogMessage("(PurchaseTotallerResults) Unable to parse results: ", e.ToString());
                this.Close();
            }
        }

        public void DisplayResults(string[] results)
        {         
            // Display information
            subtotalTextbox.Text = results[0];
            pstTextbox.Text = results[1];
            gstTextbox.Text = results[2];
            hstTextbox.Text = results[3];
            totalTextbox.Text = results[4];
            Logger.LogMessage("(PurchaseTotallerResults:DisplayResults) Successfully displayed results. Total:", results[4]);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
