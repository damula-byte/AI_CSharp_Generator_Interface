namespace GeminiC__App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            rtbOutput = new RichTextBox();
            rtbRun = new RichTextBox();
            btnGenerate = new Button();
            btnExecute = new Button();
            txtPrompt = new TextBox();
            lblPerformance = new Label();
            btnOpen = new Button();
            SuspendLayout();
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = Color.Azure;
            rtbOutput.Font = new Font("Montserrat Medium", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = SystemColors.InfoText;
            rtbOutput.Location = new Point(28, 21);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(762, 354);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // rtbRun
            // 
            rtbRun.BackColor = Color.Azure;
            rtbRun.Font = new Font("Montserrat Medium", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbRun.ForeColor = SystemColors.InfoText;
            rtbRun.Location = new Point(813, 21);
            rtbRun.Name = "rtbRun";
            rtbRun.Size = new Size(292, 354);
            rtbRun.TabIndex = 1;
            rtbRun.Text = "";
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.MintCream;
            btnGenerate.Cursor = Cursors.Hand;
            btnGenerate.FlatStyle = FlatStyle.Popup;
            btnGenerate.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerate.Location = new Point(897, 396);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(134, 42);
            btnGenerate.TabIndex = 2;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnExecute
            // 
            btnExecute.BackColor = Color.MintCream;
            btnExecute.Cursor = Cursors.Hand;
            btnExecute.FlatStyle = FlatStyle.Popup;
            btnExecute.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExecute.Location = new Point(897, 444);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(134, 42);
            btnExecute.TabIndex = 3;
            btnExecute.Text = "Execute";
            btnExecute.UseVisualStyleBackColor = false;
            btnExecute.Click += btnExecute_Click_1;
            // 
            // txtPrompt
            // 
            txtPrompt.Anchor = AnchorStyles.Left;
            txtPrompt.BackColor = Color.MintCream;
            txtPrompt.Cursor = Cursors.IBeam;
            txtPrompt.Font = new Font("Montserrat Medium", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPrompt.ForeColor = SystemColors.ControlText;
            txtPrompt.Location = new Point(28, 396);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(762, 61);
            txtPrompt.TabIndex = 4;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // lblPerformance
            // 
            lblPerformance.AutoSize = true;
            lblPerformance.Font = new Font("Montserrat SemiBold", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPerformance.ForeColor = SystemColors.Desktop;
            lblPerformance.Location = new Point(28, 477);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(61, 24);
            lblPerformance.TabIndex = 5;
            lblPerformance.Text = "label1";
            // 
            // btnOpen
            // 
            btnOpen.BackColor = Color.MintCream;
            btnOpen.Cursor = Cursors.Hand;
            btnOpen.FlatStyle = FlatStyle.Popup;
            btnOpen.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpen.Location = new Point(897, 492);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(134, 42);
            btnOpen.TabIndex = 6;
            btnOpen.Text = "Show";
            btnOpen.UseVisualStyleBackColor = false;
            btnOpen.Click += btnOpen_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1134, 549);
            Controls.Add(btnOpen);
            Controls.Add(lblPerformance);
            Controls.Add(txtPrompt);
            Controls.Add(btnExecute);
            Controls.Add(btnGenerate);
            Controls.Add(rtbRun);
            Controls.Add(rtbOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "C# Generator";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbOutput;
        private RichTextBox rtbRun;
        private Button btnGenerate;
        private Button btnExecute;
        private TextBox txtPrompt;
        private Label lblPerformance;
        private Button btnOpen;
    }
}
