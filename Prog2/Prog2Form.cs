// Spencer Kerber
// CIS 200-10
// Program 3
// Due 6/22/15

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Prog2
{
    public partial class Prog2Form : Form
    {
        private List<Address> addressList; // The list of addresses
        private List<Parcel> parcelList;   // The list of parcels

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            addressList = new List<Address>();
            parcelList = new List<Parcel>();

            // Test Data - Magic Numbers OK
           
        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("Program 2{0}By: Andrew L. Wright{0}" +
                "CIS 200{0}Summer 2015", Environment.NewLine), "About Program 2");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm(); // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    Address newAddress = new Address(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        int.Parse(addressForm.ZipText)); // Use form's properties to create address
                    addressList.Add(newAddress);
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String

            result.Append("Addresses:");
            result.Append(Environment.NewLine); // Remember, \n doesn't always work in GUIs
            result.Append(Environment.NewLine);

            foreach (Address a in addressList)
            {
                result.Append(a.ToString());
                result.Append(Environment.NewLine);
                result.Append(Environment.NewLine);
            }

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (addressList.Count < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return;
            }

            letterForm = new LetterForm(addressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as addressList
                    Letter newLetter = new Letter(addressList[letterForm.OriginAddressIndex],
                        addressList[letterForm.DestinationAddressIndex],
                        decimal.Parse(letterForm.FixedCostText)); // Letter to be inserted
                    parcelList.Add(newLetter);
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs

            result.Append("Parcels:");
            result.Append(Environment.NewLine); // Remember, \n doesn't always work in GUIs
            result.Append(Environment.NewLine);

            foreach (Parcel p in parcelList)
            {
                result.Append(p.ToString());
                result.Append(Environment.NewLine);
                result.Append(Environment.NewLine);
                totalCost += p.CalcCost();
            }

            result.Append("------------------------------");
            result.Append(Environment.NewLine);
            result.Append(String.Format("Total Cost: {0:C}", totalCost));

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  File, Open menu item activated
        // Postcondition: The address file specified by the user is opened
        private void openAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter reader = new BinaryFormatter();
            FileStream input = null;
            DialogResult result;
            string fileName;
            List<Address> tempAddresses;

             using (OpenFileDialog fileChooser = new OpenFileDialog()) 
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; 
            } 

            if (result == DialogResult.OK)
            {
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    try
                    {
                        input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    
                       tempAddresses=(List<Address>)reader.Deserialize(input);

                        addressList=tempAddresses;
                    }
                     catch (IOException)
                    {
                        MessageBox.Show("Error Reading From File", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    catch (SerializationException)
                    {
                        MessageBox.Show("Error Reading From File", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                    finally
                    {
                        if (input != null)
                            input.Close(); 
                    }
                }
            } 
        }

        // Precondition:  File, Save menu item activated
        // Postcondition: The address is saved to the file specified by the user
        private void saveAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter(); 
            FileStream output = null;                          
            DialogResult result;                               
            string fileName;                                   

            using (SaveFileDialog fileChooser = new SaveFileDialog()) 
            {
                fileChooser.CheckFileExists = false; 

                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; 
            } 

              if (result == DialogResult.OK)
            {
                if (fileName == string.Empty)
                    MessageBox.Show("Invalid File Name", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    try
                    {
                        output = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                         formatter.Serialize(output, addressList); 
                    } 

                      catch (IOException)
                    {
                        MessageBox.Show("Error Writing to File", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (SerializationException)
                    {
                        MessageBox.Show("Error Writing to File", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                    finally
                    {
                        if (output != null)
                            output.Close(); 
                    }
                } 
            } 
        }

        // Precondition:  Edit, Address menu item activated
        // Postcondition: The address selected  has been edited with the new information replacing 
        //                the existing object's properties
        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<Address> Addresses;

            Addresses = addressList;

            if ((Addresses.Count() == 0))
                MessageBox.Show("Must have address to edit", "Edit Error");
            else
            {
                SelectAddressForm saForm = new SelectAddressForm(Addresses);
                DialogResult result = saForm.ShowDialog();

                if (result == DialogResult.OK) 
                {
                    int editIndex; 

                    editIndex = saForm.addressIndex;

                    if (editIndex >= 0)
                    {
                        Address editAddress = Addresses[editIndex];

                        AddressForm addressForm = new AddressForm();

                        addressForm.AddressName = editAddress.Name;

                        DialogResult editResult = addressForm.ShowDialog();

                        if (editResult == DialogResult.OK)
                        {
                            editAddress.Name = addressForm.AddressName;
                        }
                        addressForm.Dispose();
                    }
                }
                saForm.Dispose();
            }


           

            
        }
    }
}