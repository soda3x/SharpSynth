namespace SharpSynth
{
    partial class Main
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
            this.oscillator1 = new SharpSynth.Oscillator();
            this.oscillator2 = new SharpSynth.Oscillator();
            this.oscillator4 = new SharpSynth.Oscillator();
            this.oscillator3 = new SharpSynth.Oscillator();
            this.SuspendLayout();
            // 
            // oscillator1
            // 
            this.oscillator1.BackColor = System.Drawing.Color.White;
            this.oscillator1.Location = new System.Drawing.Point(13, 13);
            this.oscillator1.Name = "oscillator1";
            this.oscillator1.OscillatorNumber = 0;
            this.oscillator1.Size = new System.Drawing.Size(249, 219);
            this.oscillator1.TabIndex = 0;
            this.oscillator1.TabStop = false;
            this.oscillator1.Text = "Osc 1";
            // 
            // oscillator2
            // 
            this.oscillator2.BackColor = System.Drawing.Color.White;
            this.oscillator2.Location = new System.Drawing.Point(268, 13);
            this.oscillator2.Name = "oscillator2";
            this.oscillator2.OscillatorNumber = 0;
            this.oscillator2.Size = new System.Drawing.Size(249, 219);
            this.oscillator2.TabIndex = 11;
            this.oscillator2.TabStop = false;
            this.oscillator2.Text = "Osc 2";
            // 
            // oscillator4
            // 
            this.oscillator4.BackColor = System.Drawing.Color.White;
            this.oscillator4.Location = new System.Drawing.Point(268, 238);
            this.oscillator4.Name = "oscillator4";
            this.oscillator4.OscillatorNumber = 0;
            this.oscillator4.Size = new System.Drawing.Size(249, 219);
            this.oscillator4.TabIndex = 13;
            this.oscillator4.TabStop = false;
            this.oscillator4.Text = "Osc 4";
            // 
            // oscillator3
            // 
            this.oscillator3.BackColor = System.Drawing.Color.White;
            this.oscillator3.Location = new System.Drawing.Point(13, 238);
            this.oscillator3.Name = "oscillator3";
            this.oscillator3.OscillatorNumber = 0;
            this.oscillator3.Size = new System.Drawing.Size(249, 219);
            this.oscillator3.TabIndex = 12;
            this.oscillator3.TabStop = false;
            this.oscillator3.Text = "Osc 3";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(525, 467);
            this.Controls.Add(this.oscillator4);
            this.Controls.Add(this.oscillator3);
            this.Controls.Add(this.oscillator2);
            this.Controls.Add(this.oscillator1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "SharpSynth";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.ResumeLayout(false);

        }


        #endregion

        private Oscillator oscillator1;
        private Oscillator oscillator2;
        private Oscillator oscillator4;
        private Oscillator oscillator3;
    }
}

