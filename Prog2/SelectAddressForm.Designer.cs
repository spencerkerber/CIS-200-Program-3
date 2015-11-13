namespace Prog2
{
    partial class SelectAddressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.editAddressComboBox = new System.Windows.Forms.ComboBox();
            this.editAddressOKButton = new System.Windows.Forms.Button();
            this.editAddressCancelButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Address";
            // 
            // editAddressComboBox
            // 
            this.editAddressComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editAddressComboBox.FormattingEnabled = true;
            this.editAddressComboBox.Location = new System.Drawing.Point(52, 34);
            this.editAddressComboBox.Name = "editAddressComboBox";
            this.editAddressComboBox.Size = new System.Drawing.Size(121, 21);
            this.editAddressComboBox.TabIndex = 2;
            // 
            // editAddressOKButton
            // 
            this.editAddressOKButton.Location = new System.Drawing.Point(36, 74);
            this.editAddressOKButton.Name = "editAddressOKButton";
            this.editAddressOKButton.Size = new System.Drawing.Size(75, 23);
            this.editAddressOKButton.TabIndex = 2;
            this.editAddressOKButton.Text = "OK";
            this.editAddressOKButton.UseVisualStyleBackColor = true;
            this.editAddressOKButton.Click += new System.EventHandler(this.editAddressOKButton_Click);
            // 
            // editAddressCancelButton
            // 
            this.editAddressCancelButton.Location = new System.Drawing.Point(117, 74);
            this.editAddressCancelButton.Name = "editAddressCancelButton";
            this.editAddressCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editAddressCancelButton.TabIndex = 3;
            this.editAddressCancelButton.Text = "Cancel";
            this.editAddressCancelButton.UseVisualStyleBackColor = true;
            this.editAddressCancelButton.Click += new System.EventHandler(this.editAddressCancelButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SelectAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 137);
            this.Controls.Add(this.editAddressCancelButton);
            this.Controls.Add(this.editAddressOKButton);
            this.Controls.Add(this.editAddressComboBox);
            this.Controls.Add(this.label1);
            this.Name = "SelectAddressForm";
            this.Text = "SelectAddressForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox editAddressComboBox;
        private System.Windows.Forms.Button editAddressOKButton;
        private System.Windows.Forms.Button editAddressCancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}