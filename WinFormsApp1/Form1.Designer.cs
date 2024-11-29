namespace WinFormsApp1
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
            buttonloadimage = new Button();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            labelSize = new Label();
            trackBar1 = new TrackBar();
            labelTrackBarValue = new Label();
            aprowdbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // buttonloadimage
            // 
            buttonloadimage.Location = new Point(211, 308);
            buttonloadimage.Name = "buttonloadimage";
            buttonloadimage.Size = new Size(172, 29);
            buttonloadimage.TabIndex = 0;
            buttonloadimage.Text = "Загрузка";
            buttonloadimage.UseVisualStyleBackColor = true;
            buttonloadimage.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(211, 29);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(344, 260);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(389, 308);
            button2.Name = "button2";
            button2.Size = new Size(166, 29);
            button2.TabIndex = 2;
            button2.Text = "Сохранение";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // labelSize
            // 
            labelSize.AutoSize = true;
            labelSize.Location = new Point(211, 353);
            labelSize.Name = "labelSize";
            labelSize.Size = new Size(0, 20);
            labelSize.TabIndex = 3;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(572, 233);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 4;
            trackBar1.Value = 100;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // labelTrackBarValue
            // 
            labelTrackBarValue.AutoSize = true;
            labelTrackBarValue.Location = new Point(621, 269);
            labelTrackBarValue.Name = "labelTrackBarValue";
            labelTrackBarValue.Size = new Size(33, 20);
            labelTrackBarValue.TabIndex = 5;
            labelTrackBarValue.Text = "100";
            // 
            // aprowdbutton
            // 
            aprowdbutton.Location = new Point(572, 292);
            aprowdbutton.Name = "aprowdbutton";
            aprowdbutton.Size = new Size(130, 29);
            aprowdbutton.TabIndex = 6;
            aprowdbutton.Text = "Применить";
            aprowdbutton.UseVisualStyleBackColor = true;
            aprowdbutton.Click += aprowdbutton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(aprowdbutton);
            Controls.Add(labelTrackBarValue);
            Controls.Add(trackBar1);
            Controls.Add(labelSize);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(buttonloadimage);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonloadimage;
        private PictureBox pictureBox1;
        private Button button2;
        private Label labelSize;
        private TrackBar trackBar1;
        private Label labelTrackBarValue;
        private Button aprowdbutton;
    }
}
