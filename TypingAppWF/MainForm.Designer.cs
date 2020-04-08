namespace TypingAppWF
{
    partial class MainForm
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.openLessonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtxtTyping = new System.Windows.Forms.RichTextBox();
            this.btnStartTyping = new System.Windows.Forms.Button();
            this.btnPauseTyping = new System.Windows.Forms.Button();
            this.btnCancelTyping = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.nudSpeedBeeping = new System.Windows.Forms.NumericUpDown();
            this.rtxtLesson = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartBeeping = new System.Windows.Forms.Button();
            this.btnStopBeeping = new System.Windows.Forms.Button();
            this.bgwBeeping = new System.ComponentModel.BackgroundWorker();
            this.lbWritingTimeSpan = new System.Windows.Forms.Label();
            this.lbWritingTimeFixed = new System.Windows.Forms.Label();
            this.tmTimer = new System.Windows.Forms.Timer(this.components);
            this.lbStatistics = new System.Windows.Forms.Label();
            this.chUseRevisionLesson = new System.Windows.Forms.CheckBox();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeedBeeping)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLessonToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(787, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // openLessonToolStripMenuItem
            // 
            this.openLessonToolStripMenuItem.Name = "openLessonToolStripMenuItem";
            this.openLessonToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openLessonToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.openLessonToolStripMenuItem.Text = "Open Lesson";
            // 
            // rtxtTyping
            // 
            this.rtxtTyping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtxtTyping.Location = new System.Drawing.Point(12, 94);
            this.rtxtTyping.Name = "rtxtTyping";
            this.rtxtTyping.Size = new System.Drawing.Size(765, 60);
            this.rtxtTyping.TabIndex = 1;
            this.rtxtTyping.Text = "";
            this.rtxtTyping.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RtxtTyping_KeyPress);
            // 
            // btnStartTyping
            // 
            this.btnStartTyping.Location = new System.Drawing.Point(12, 162);
            this.btnStartTyping.Name = "btnStartTyping";
            this.btnStartTyping.Size = new System.Drawing.Size(90, 25);
            this.btnStartTyping.TabIndex = 3;
            this.btnStartTyping.Text = "Start Typing";
            this.btnStartTyping.UseVisualStyleBackColor = true;
            this.btnStartTyping.Click += new System.EventHandler(this.BtnStartTyping_Click);
            // 
            // btnPauseTyping
            // 
            this.btnPauseTyping.Location = new System.Drawing.Point(108, 162);
            this.btnPauseTyping.Name = "btnPauseTyping";
            this.btnPauseTyping.Size = new System.Drawing.Size(90, 25);
            this.btnPauseTyping.TabIndex = 4;
            this.btnPauseTyping.Text = "Pause Typing";
            this.btnPauseTyping.UseVisualStyleBackColor = true;
            this.btnPauseTyping.Click += new System.EventHandler(this.BtnPauseTyping_Click);
            // 
            // btnCancelTyping
            // 
            this.btnCancelTyping.Location = new System.Drawing.Point(204, 162);
            this.btnCancelTyping.Name = "btnCancelTyping";
            this.btnCancelTyping.Size = new System.Drawing.Size(90, 25);
            this.btnCancelTyping.TabIndex = 5;
            this.btnCancelTyping.Text = "Cancel Typing";
            this.btnCancelTyping.UseVisualStyleBackColor = true;
            this.btnCancelTyping.Click += new System.EventHandler(this.BtnCancelTyping_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 277);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(787, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // nudSpeedBeeping
            // 
            this.nudSpeedBeeping.Location = new System.Drawing.Point(650, 165);
            this.nudSpeedBeeping.Name = "nudSpeedBeeping";
            this.nudSpeedBeeping.Size = new System.Drawing.Size(48, 20);
            this.nudSpeedBeeping.TabIndex = 0;
            this.nudSpeedBeeping.TabStop = false;
            this.nudSpeedBeeping.ValueChanged += new System.EventHandler(this.nudSpeed_ValueChanged);
            this.nudSpeedBeeping.Leave += new System.EventHandler(this.nudSpeed_Leave);
            this.nudSpeedBeeping.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NudSpeed_MouseDown);
            // 
            // rtxtLesson
            // 
            this.rtxtLesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtxtLesson.Location = new System.Drawing.Point(12, 27);
            this.rtxtLesson.Name = "rtxtLesson";
            this.rtxtLesson.Size = new System.Drawing.Size(765, 60);
            this.rtxtLesson.TabIndex = 11;
            this.rtxtLesson.TabStop = false;
            this.rtxtLesson.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(556, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Set Typing Speed";
            // 
            // btnStartBeeping
            // 
            this.btnStartBeeping.Location = new System.Drawing.Point(701, 162);
            this.btnStartBeeping.Name = "btnStartBeeping";
            this.btnStartBeeping.Size = new System.Drawing.Size(37, 23);
            this.btnStartBeeping.TabIndex = 13;
            this.btnStartBeeping.TabStop = false;
            this.btnStartBeeping.Text = "Start";
            this.btnStartBeeping.UseVisualStyleBackColor = true;
            this.btnStartBeeping.Click += new System.EventHandler(this.BtnStartBeeping_Click);
            // 
            // btnStopBeeping
            // 
            this.btnStopBeeping.Location = new System.Drawing.Point(740, 162);
            this.btnStopBeeping.Name = "btnStopBeeping";
            this.btnStopBeeping.Size = new System.Drawing.Size(37, 23);
            this.btnStopBeeping.TabIndex = 14;
            this.btnStopBeeping.TabStop = false;
            this.btnStopBeeping.Text = "Stop";
            this.btnStopBeeping.UseVisualStyleBackColor = true;
            this.btnStopBeeping.Click += new System.EventHandler(this.btnStopBeeping_Click);
            // 
            // bgwBeeping
            // 
            this.bgwBeeping.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwBeeping_DoWork);
            // 
            // lbWritingTimeSpan
            // 
            this.lbWritingTimeSpan.AutoSize = true;
            this.lbWritingTimeSpan.Location = new System.Drawing.Point(725, 195);
            this.lbWritingTimeSpan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWritingTimeSpan.Name = "lbWritingTimeSpan";
            this.lbWritingTimeSpan.Size = new System.Drawing.Size(0, 13);
            this.lbWritingTimeSpan.TabIndex = 15;
            // 
            // lbWritingTimeFixed
            // 
            this.lbWritingTimeFixed.AutoSize = true;
            this.lbWritingTimeFixed.Location = new System.Drawing.Point(654, 195);
            this.lbWritingTimeFixed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWritingTimeFixed.Name = "lbWritingTimeFixed";
            this.lbWritingTimeFixed.Size = new System.Drawing.Size(75, 13);
            this.lbWritingTimeFixed.TabIndex = 16;
            this.lbWritingTimeFixed.Text = "Writing Time : ";
            // 
            // tmTimer
            // 
            this.tmTimer.Enabled = true;
            this.tmTimer.Interval = 1;
            this.tmTimer.Tick += new System.EventHandler(this.TmTimer_Tick);
            // 
            // lbStatistics
            // 
            this.lbStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbStatistics.Location = new System.Drawing.Point(12, 195);
            this.lbStatistics.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStatistics.Name = "lbStatistics";
            this.lbStatistics.Size = new System.Drawing.Size(598, 79);
            this.lbStatistics.TabIndex = 17;
            // 
            // chUseRevisionLesson
            // 
            this.chUseRevisionLesson.AutoSize = true;
            this.chUseRevisionLesson.Location = new System.Drawing.Point(300, 167);
            this.chUseRevisionLesson.Name = "chUseRevisionLesson";
            this.chUseRevisionLesson.Size = new System.Drawing.Size(208, 17);
            this.chUseRevisionLesson.TabIndex = 18;
            this.chUseRevisionLesson.Text = "Type the revision of the chosen lesson";
            this.chUseRevisionLesson.UseVisualStyleBackColor = true;
            this.chUseRevisionLesson.CheckedChanged += new System.EventHandler(this.chUseRevisionLesson_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 299);
            this.Controls.Add(this.chUseRevisionLesson);
            this.Controls.Add(this.lbStatistics);
            this.Controls.Add(this.lbWritingTimeFixed);
            this.Controls.Add(this.lbWritingTimeSpan);
            this.Controls.Add(this.btnStopBeeping);
            this.Controls.Add(this.btnStartBeeping);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtxtLesson);
            this.Controls.Add(this.nudSpeedBeeping);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancelTyping);
            this.Controls.Add(this.btnPauseTyping);
            this.Controls.Add(this.btnStartTyping);
            this.Controls.Add(this.rtxtTyping);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Typing App";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeedBeeping)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem openLessonToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtxtTyping;
        private System.Windows.Forms.Button btnStartTyping;
        private System.Windows.Forms.Button btnPauseTyping;
        private System.Windows.Forms.Button btnCancelTyping;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.NumericUpDown nudSpeedBeeping;
        private System.Windows.Forms.RichTextBox rtxtLesson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartBeeping;
        private System.Windows.Forms.Button btnStopBeeping;
        private System.ComponentModel.BackgroundWorker bgwBeeping;
        private System.Windows.Forms.Label lbWritingTimeSpan;
        private System.Windows.Forms.Label lbWritingTimeFixed;
        private System.Windows.Forms.Timer tmTimer;
        private System.Windows.Forms.Label lbStatistics;
        private System.Windows.Forms.CheckBox chUseRevisionLesson;
    }
}

