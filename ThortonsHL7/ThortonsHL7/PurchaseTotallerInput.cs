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

            if (Validate())
            {
                float purchaseSubtotal = float.Parse(textBoxPurchaseSubtotal.Text);
                string province = (string)comboBoxProvince.SelectedValue;

                // Async task or something
                MessageBox.Show("Sending purchase subtotal to be totalled by purchase totaller");
            }
            
        }

        private bool Validate()
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

    }
}
