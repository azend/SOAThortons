using Shared;
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
    public partial class PurchaseTotallerInput : Form
    {
        public PurchaseTotallerInput()
        {
            InitializeComponent();
            comboBoxProvince.DataSource = Regions.GetNames();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {

            if (ValidateData())
            {
                float purchaseSubtotal = float.Parse(textBoxPurchaseSubtotal.Text);
                string province = (string)comboBoxProvince.SelectedValue;
                MessageBox.Show("Sending purchase subtotal to be totalled by purchase totaller");

                // Placeholder code
                // string executeString = ExecuteService.GenerateMessage("freelancer", RegisterTeam.GetTeamID(), "GIORP-TOTALLER", "2", "?", "?", "?", "?");
                //Logger.Log("Sending query service message: " + executeString);
                // Still need to set up sockets properly
                // string response = SocketClient.ExecuteService();
                // Logger.Log("Recieved response: " + response);
                bool executeResponse = false;
                try
                {
                    // executeResponse = ExecuteService.ParseMessage(response);
                }
                catch { }

                if (!executeResponse)
                {
                    MessageBox.Show("Error executing service: [" + ExecuteService.GetErrorCode() + "] " + ExecuteService.GetErrorMessage());
                    Logger.Log("Error executing service: [" + ExecuteService.GetErrorCode() + "] " + ExecuteService.GetErrorMessage());
                }
                else
                {
                    this.Close();
                    // parse data and send to result screen
                    // new PurchaseTotallerResults(float subTotal, float PST, float GST, float HST, float total).Show();
                    new PurchaseTotallerResults().Show();
                }
            }
        }

        private bool ValidateData()
        {
            bool valid = true;

            try
            {
                float.Parse(textBoxPurchaseSubtotal.Text);
            }
            catch (Exception e)
            {
                valid = false;
                MessageBox.Show("Invalid purchase subtotal");
            }

            if (comboBoxProvince.SelectedValue == null)
            {
                valid = false;
                MessageBox.Show("Invalid province / territory");
            }

            return valid;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPurchaseSubtotal_Enter(object sender, EventArgs e)
        {
            if (textBoxPurchaseSubtotal.Text == "Enter value")
            {
                textBoxPurchaseSubtotal.Clear();
            }
        }

    }
}
