namespace EPUBTest
{
    partial class Form1
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
            this.testButton = new System.Windows.Forms.Button();
            this.textboxAuthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxTitle = new System.Windows.Forms.TextBox();
            this.richtextSampleContent = new System.Windows.Forms.RichTextBox();
            this.checkBoxJpeg = new System.Windows.Forms.CheckBox();
            this.checkBoxPng = new System.Windows.Forms.CheckBox();
            this.checkBoxGif = new System.Windows.Forms.CheckBox();
            this.checkBoxFonts = new System.Windows.Forms.CheckBox();
            this.checkBoxSvg = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(102, 207);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(174, 23);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Generate ePUB";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // textboxAuthor
            // 
            this.textboxAuthor.Location = new System.Drawing.Point(59, 17);
            this.textboxAuthor.Name = "textboxAuthor";
            this.textboxAuthor.Size = new System.Drawing.Size(100, 20);
            this.textboxAuthor.TabIndex = 1;
            this.textboxAuthor.Text = "John Doe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Author";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Title";
            // 
            // textboxTitle
            // 
            this.textboxTitle.Location = new System.Drawing.Point(59, 43);
            this.textboxTitle.Name = "textboxTitle";
            this.textboxTitle.Size = new System.Drawing.Size(100, 20);
            this.textboxTitle.TabIndex = 3;
            this.textboxTitle.Text = "Vampires... whatever";
            // 
            // richtextSampleContent
            // 
            this.richtextSampleContent.Location = new System.Drawing.Point(165, 12);
            this.richtextSampleContent.Name = "richtextSampleContent";
            this.richtextSampleContent.Size = new System.Drawing.Size(218, 175);
            this.richtextSampleContent.TabIndex = 5;
            this.richtextSampleContent.Text = "Add some text here in  any language";
            // 
            // checkBoxJpeg
            // 
            this.checkBoxJpeg.AutoSize = true;
            this.checkBoxJpeg.Location = new System.Drawing.Point(18, 78);
            this.checkBoxJpeg.Name = "checkBoxJpeg";
            this.checkBoxJpeg.Size = new System.Drawing.Size(122, 17);
            this.checkBoxJpeg.TabIndex = 6;
            this.checkBoxJpeg.Text = "Add JPEG test page";
            this.checkBoxJpeg.UseVisualStyleBackColor = true;
            // 
            // checkBoxPng
            // 
            this.checkBoxPng.AutoSize = true;
            this.checkBoxPng.Location = new System.Drawing.Point(18, 101);
            this.checkBoxPng.Name = "checkBoxPng";
            this.checkBoxPng.Size = new System.Drawing.Size(118, 17);
            this.checkBoxPng.TabIndex = 7;
            this.checkBoxPng.Text = "Add PNG test page";
            this.checkBoxPng.UseVisualStyleBackColor = true;
            // 
            // checkBoxGif
            // 
            this.checkBoxGif.AutoSize = true;
            this.checkBoxGif.Location = new System.Drawing.Point(18, 124);
            this.checkBoxGif.Name = "checkBoxGif";
            this.checkBoxGif.Size = new System.Drawing.Size(112, 17);
            this.checkBoxGif.TabIndex = 8;
            this.checkBoxGif.Text = "Add GIF test page";
            this.checkBoxGif.UseVisualStyleBackColor = true;
            // 
            // checkBoxFonts
            // 
            this.checkBoxFonts.AutoSize = true;
            this.checkBoxFonts.Location = new System.Drawing.Point(18, 170);
            this.checkBoxFonts.Name = "checkBoxFonts";
            this.checkBoxFonts.Size = new System.Drawing.Size(85, 17);
            this.checkBoxFonts.TabIndex = 9;
            this.checkBoxFonts.Text = "Embed fonts";
            this.checkBoxFonts.UseVisualStyleBackColor = true;
            // 
            // checkBoxSvg
            // 
            this.checkBoxSvg.AutoSize = true;
            this.checkBoxSvg.Location = new System.Drawing.Point(18, 147);
            this.checkBoxSvg.Name = "checkBoxSvg";
            this.checkBoxSvg.Size = new System.Drawing.Size(117, 17);
            this.checkBoxSvg.TabIndex = 10;
            this.checkBoxSvg.Text = "Add SVG test page";
            this.checkBoxSvg.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 243);
            this.Controls.Add(this.checkBoxSvg);
            this.Controls.Add(this.checkBoxFonts);
            this.Controls.Add(this.checkBoxGif);
            this.Controls.Add(this.checkBoxPng);
            this.Controls.Add(this.checkBoxJpeg);
            this.Controls.Add(this.richtextSampleContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textboxTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxAuthor);
            this.Controls.Add(this.testButton);
            this.Name = "Form1";
            this.Text = "EPUBTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TextBox textboxAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxTitle;
        private System.Windows.Forms.RichTextBox richtextSampleContent;
        private System.Windows.Forms.CheckBox checkBoxJpeg;
        private System.Windows.Forms.CheckBox checkBoxPng;
        private System.Windows.Forms.CheckBox checkBoxGif;
        private System.Windows.Forms.CheckBox checkBoxFonts;
        private System.Windows.Forms.CheckBox checkBoxSvg;
    }
}

