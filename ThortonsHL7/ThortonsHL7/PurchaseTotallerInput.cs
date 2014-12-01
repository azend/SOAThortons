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
                Dictionary<string, string> serviceInfo = Comms.ExecuteService();
                this.Close();
                new PurchaseTotallerResults().Show();
                //Logger.Log("Sending query service message: " + executeString);
                // Logger.Log("Recieved response: " + response);
                //if (!executeResponse)
                //{
                //    MessageBox.Show("Error executing service: [" + ExecuteService.GetErrorCode() + "] " + ExecuteService.GetErrorMessage());
                //    Logger.Log("Error executing service: [" + ExecuteService.GetErrorCode() + "] " + ExecuteService.GetErrorMessage());
                //}
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
