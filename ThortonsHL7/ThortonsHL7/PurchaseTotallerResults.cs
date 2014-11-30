﻿using System;
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
        }

        public PurchaseTotallerResults(float subTotal, float PST, float GST, float HST, float total)
        {
            InitializeComponent();
            
            // Wow
            // Display information
            subtotalTextbox.Text = subTotal.ToString();
            pstTextbox.Text = PST.ToString();
            gstTextbox.Text = GST.ToString();
            hstTextbox.Text = HST.ToString();
            totalTextbox.Text = total.ToString();
            // Such code
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Very productivity
            this.Close();
        }
    }
}
