/*
 * FILE        : PurchaseTotallerInput.cs
 * PROJECT     : Service Oriented Architecture - Assignment #1 (Thorton's SOA)
 * AUTHORS     : Jim Raithby, Verdi R-D, Richard Meijer, Mathew Cain 
 * SUBMIT DATE : 11/30/2014
 * DESCRIPTION : A windows form that allows a user to enter the subtotal amount, and province to send to the GIORP purchase totaller service.
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

                try
                {
                    Dictionary<string, string> serviceInfo = Comms.ExecuteService(purchaseSubtotal, province);
                }
                catch
                {
                    MessageBox.Show("Error executing service: [" + ExecuteService.GetErrorCode() + "] " + ExecuteService.GetErrorMessage());
                    Logger.LogMessage("(PurchaseTotallerInput:Execute) Error executing service: {" + ExecuteService.GetErrorCode() + "} ", ExecuteService.GetErrorMessage());
                }

                this.Close();
                new PurchaseTotallerResults().Show();
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
                Logger.LogMessage("(PurchaseTotallerInput:ValidateData) Error parsing subtotal: ", e.ToString());
            }

            if (comboBoxProvince.SelectedValue == null)
            {
                valid = false;
                MessageBox.Show("Invalid province / territory");
                Logger.LogMessage("(PurchaseTotallerInput:ValidateData) Invalid province/territory entered: ", comboBoxProvince.SelectedValue.ToString());
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
