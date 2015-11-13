// Spencer Kerber
// CIS 200-10
// Program 3
// Due 6/22/15

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class SelectAddressForm : Form
    {
        private List<Address> addressList;

        // Preconditions: List addressList is populated with the available
        //                addresses to choose from
        // Postconditions: The fors GUI is prepared for display
        public SelectAddressForm(List<Address>Addresses)
        {
            InitializeComponent();

            addressList = Addresses;
        }

        // Preconditions: None
        // Postconditions: The list of addresses is used to populate the
        //                 address combo box
        private void SelectAddressForm_Load(object sender, EventArgs e)
        {
            foreach (Address address in addressList)
                editAddressComboBox.Items.Add(address.Name);
        }

        public int addressIndex
        {
            // Preconditions: None
            // Postconditions: The index of form's selected address combo box has been returned
            get
            {
                return editAddressComboBox.SelectedIndex;
            }
        }

        // Preconditions: Focus is shifted from the address combo box
        // Postconditions: If selection is invalid, focus remains and error provider
        //                 highlights the field
        private void editAddressComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (editAddressComboBox.SelectedIndex == -1) // Nothing selected
            {
                e.Cancel = true;
                errorProvider.SetError(editAddressComboBox, "Must select Address");
            }
        }

        // Preconditions: Validating of address combobox not cancelled, so data OK
        // Postconditions: Error provider cleared and focus allowed to change
        private void editAddressComboBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(editAddressComboBox, "");
        }

        // Preconditions: Cancel is clicked 
        // Postconditions: Form is closed
        private void editAddressCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // Preconditions: OK is clicked 
        // Postconditions: Address is selected 
        private void editAddressOKButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }
    }
}
