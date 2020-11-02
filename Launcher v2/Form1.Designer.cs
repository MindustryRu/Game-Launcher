namespace Launcher_v2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.downloadLbl = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.roundButton3 = new Launcher_v2.RoundButton();
            this.roundButton2 = new Launcher_v2.RoundButton();
            this.roundButton1 = new Launcher_v2.RoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // downloadLbl
            // 
            this.downloadLbl.AutoSize = true;
            this.downloadLbl.BackColor = System.Drawing.Color.Transparent;
            this.downloadLbl.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.downloadLbl.Location = new System.Drawing.Point(12, 263);
            this.downloadLbl.Name = "downloadLbl";
            this.downloadLbl.Size = new System.Drawing.Size(0, 15);
            this.downloadLbl.TabIndex = 21;
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(686, -33);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(34, 26);
            this.closeBtn.TabIndex = 31;
            this.closeBtn.Text = "х";
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Location = new System.Drawing.Point(646, -33);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(34, 26);
            this.minimizeBtn.TabIndex = 30;
            this.minimizeBtn.Text = "_";
            this.minimizeBtn.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 281);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(456, 23);
            this.progressBar1.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Launcher_v2.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(107, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(271, 29);
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(433, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(35, 22);
            this.button5.TabIndex = 26;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::Launcher_v2.Properties.Resources.folder;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Launcher_v2.Properties.Resources.folder;
            this.pictureBox2.Location = new System.Drawing.Point(12, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::Launcher_v2.Properties.Resources.maps;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::Launcher_v2.Properties.Resources.maps;
            this.pictureBox3.Location = new System.Drawing.Point(42, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 25);
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::Launcher_v2.Properties.Resources.minimize2;
            this.button1.Location = new System.Drawing.Point(398, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 22);
            this.button1.TabIndex = 39;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::Launcher_v2.Properties.Resources.setting;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Location = new System.Drawing.Point(72, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(25, 25);
            this.pictureBox4.TabIndex = 40;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(12, 34);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(456, 226);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.TabStop = false;
            this.webBrowser1.Url = new System.Uri("http://update.mindustry.ru/web/", System.UriKind.Absolute);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // roundButton3
            // 
            this.roundButton3.BackColor = System.Drawing.Color.Gainsboro;
            this.roundButton3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton3.ButtonBorderColor = System.Drawing.Color.Black;
            this.roundButton3.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton3.ButtonHighlightColor2 = System.Drawing.Color.White;
            this.roundButton3.ButtonHighlightForeColor = System.Drawing.Color.Black;
            this.roundButton3.ButtonPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton3.ButtonPressedColor2 = System.Drawing.Color.White;
            this.roundButton3.ButtonPressedForeColor = System.Drawing.Color.White;
            this.roundButton3.ButtonRoundRadius = 20;
            this.roundButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundButton3.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundButton3.Location = new System.Drawing.Point(93, 310);
            this.roundButton3.Name = "roundButton3";
            this.roundButton3.Size = new System.Drawing.Size(75, 23);
            this.roundButton3.TabIndex = 36;
            this.roundButton3.Text = "Discord";
            this.roundButton3.Click += new System.EventHandler(this.roundButton3_Click);
            // 
            // roundButton2
            // 
            this.roundButton2.BackColor = System.Drawing.Color.Gainsboro;
            this.roundButton2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton2.ButtonBorderColor = System.Drawing.Color.Black;
            this.roundButton2.ButtonHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton2.ButtonHighlightColor2 = System.Drawing.Color.White;
            this.roundButton2.ButtonHighlightForeColor = System.Drawing.Color.Black;
            this.roundButton2.ButtonPressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(131)))), ((int)(((byte)(232)))));
            this.roundButton2.ButtonPressedColor2 = System.Drawing.Color.White;
            this.roundButton2.ButtonPressedForeColor = System.Drawing.Color.White;
            this.roundButton2.ButtonRoundRadius = 20;
            this.roundButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundButton2.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundButton2.Location = new System.Drawing.Point(12, 310);
            this.roundButton2.Name = "roundButton2";
            this.roundButton2.Size = new System.Drawing.Size(75, 23);
            this.roundButton2.TabIndex = 35;
            this.roundButton2.Text = "Сайт";
            this.roundButton2.Click += new System.EventHandler(this.roundButton2_Click);
            // 
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.White;
            this.roundButton1.BackColor2 = System.Drawing.Color.Green;
            this.roundButton1.ButtonBorderColor = System.Drawing.Color.Black;
            this.roundButton1.ButtonHighlightColor = System.Drawing.Color.Green;
            this.roundButton1.ButtonHighlightColor2 = System.Drawing.Color.White;
            this.roundButton1.ButtonHighlightForeColor = System.Drawing.Color.Black;
            this.roundButton1.ButtonPressedColor = System.Drawing.Color.Green;
            this.roundButton1.ButtonPressedColor2 = System.Drawing.Color.White;
            this.roundButton1.ButtonPressedForeColor = System.Drawing.Color.White;
            this.roundButton1.ButtonRoundRadius = 20;
            this.roundButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundButton1.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roundButton1.Location = new System.Drawing.Point(393, 310);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.Size = new System.Drawing.Size(75, 23);
            this.roundButton1.TabIndex = 34;
            this.roundButton1.Text = "Играть!";
            this.roundButton1.Click += new System.EventHandler(this.roundButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Launcher_v2.Properties.Resources._2;
            this.ClientSize = new System.Drawing.Size(480, 337);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.roundButton3);
            this.Controls.Add(this.roundButton2);
            this.Controls.Add(this.roundButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.downloadLbl);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label downloadLbl;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private RoundButton roundButton1;
        private RoundButton roundButton2;
        private RoundButton roundButton3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

