namespace ConsoleApplication1
{
    partial class EllipticForm
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
            this.labelIsCorrect = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.textBox2Hash = new System.Windows.Forms.TextBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelIsCorrect
            // 
            this.labelIsCorrect.AutoSize = true;
            this.labelIsCorrect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIsCorrect.Location = new System.Drawing.Point(20, 67);
            this.labelIsCorrect.Name = "labelIsCorrect";
            this.labelIsCorrect.Size = new System.Drawing.Size(0, 16);
            this.labelIsCorrect.TabIndex = 11;
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(606, 292);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(128, 23);
            this.btnVerify.TabIndex = 10;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(606, 257);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(128, 24);
            this.btnSign.TabIndex = 9;
            this.btnSign.Text = "Sing";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // textBox2Hash
            // 
            this.textBox2Hash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2Hash.Location = new System.Drawing.Point(23, 31);
            this.textBox2Hash.Name = "textBox2Hash";
            this.textBox2Hash.Size = new System.Drawing.Size(713, 20);
            this.textBox2Hash.TabIndex = 8;
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(23, 5);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(713, 20);
            this.textBoxText.TabIndex = 7;
            this.textBoxText.Text = "Message";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(608, 339);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(128, 22);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // EllipticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 367);
            this.Controls.Add(this.labelIsCorrect);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.textBox2Hash);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.btnTest);
            this.Name = "EllipticForm";
            this.Text = "EllipticForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIsCorrect;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.TextBox textBox2Hash;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button btnTest;
    }
}