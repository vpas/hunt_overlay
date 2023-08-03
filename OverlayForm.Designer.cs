namespace hunt_overlay {
    partial class OverlayForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            playerName1 = new Label();
            playerScore1 = new Label();
            playerScore2 = new Label();
            playerName2 = new Label();
            moveHandle = new PictureBox();
            timer = new Label();
            message = new Label();
            roundInfo = new Label();
            restartButton = new Button();
            deathButton = new Button();
            ((System.ComponentModel.ISupportInitialize)moveHandle).BeginInit();
            SuspendLayout();
            // 
            // playerName1
            // 
            playerName1.AutoSize = true;
            playerName1.BackColor = Color.Gray;
            playerName1.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            playerName1.ForeColor = Color.Navy;
            playerName1.Location = new Point(113, 22);
            playerName1.Name = "playerName1";
            playerName1.Size = new Size(143, 34);
            playerName1.TabIndex = 2;
            playerName1.Text = "PLAYER_1";
            // 
            // playerScore1
            // 
            playerScore1.AutoSize = true;
            playerScore1.BackColor = Color.Gray;
            playerScore1.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            playerScore1.ForeColor = Color.Navy;
            playerScore1.Location = new Point(326, 22);
            playerScore1.Name = "playerScore1";
            playerScore1.Size = new Size(31, 34);
            playerScore1.TabIndex = 3;
            playerScore1.Text = "0";
            // 
            // playerScore2
            // 
            playerScore2.AutoSize = true;
            playerScore2.BackColor = Color.Gray;
            playerScore2.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            playerScore2.ForeColor = Color.FromArgb(192, 0, 0);
            playerScore2.Location = new Point(757, 22);
            playerScore2.Name = "playerScore2";
            playerScore2.Size = new Size(31, 34);
            playerScore2.TabIndex = 5;
            playerScore2.Text = "0";
            // 
            // playerName2
            // 
            playerName2.AutoSize = true;
            playerName2.BackColor = Color.Gray;
            playerName2.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            playerName2.ForeColor = Color.FromArgb(192, 0, 0);
            playerName2.Location = new Point(545, 22);
            playerName2.Name = "playerName2";
            playerName2.Size = new Size(143, 34);
            playerName2.TabIndex = 4;
            playerName2.Text = "PLAYER_1";
            // 
            // moveHandle
            // 
            moveHandle.BackColor = Color.Gray;
            moveHandle.Cursor = Cursors.SizeAll;
            moveHandle.Image = Properties.Resources.move_icon;
            moveHandle.Location = new Point(43, 22);
            moveHandle.Margin = new Padding(3, 2, 3, 2);
            moveHandle.Name = "moveHandle";
            moveHandle.Size = new Size(24, 20);
            moveHandle.SizeMode = PictureBoxSizeMode.Zoom;
            moveHandle.TabIndex = 6;
            moveHandle.TabStop = false;
            // 
            // timer
            // 
            timer.AutoSize = true;
            timer.BackColor = Color.Gray;
            timer.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            timer.ForeColor = Color.Green;
            timer.Location = new Point(412, 22);
            timer.Name = "timer";
            timer.Size = new Size(79, 34);
            timer.TabIndex = 7;
            timer.Text = "5:00";
            // 
            // message
            // 
            message.AutoSize = true;
            message.BackColor = Color.Gray;
            message.Font = new Font("Source Code Pro Black", 40F, FontStyle.Regular, GraphicsUnit.Point);
            message.ForeColor = Color.Green;
            message.Location = new Point(287, 259);
            message.Name = "message";
            message.Size = new Size(349, 68);
            message.TabIndex = 8;
            message.Text = "Round Won!";
            message.TextAlign = ContentAlignment.TopCenter;
            // 
            // roundInfo
            // 
            roundInfo.Anchor = AnchorStyles.Top;
            roundInfo.AutoSize = true;
            roundInfo.BackColor = Color.Gray;
            roundInfo.Font = new Font("Source Code Pro Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            roundInfo.ForeColor = Color.Green;
            roundInfo.Location = new Point(313, 78);
            roundInfo.Name = "roundInfo";
            roundInfo.Size = new Size(287, 34);
            roundInfo.TabIndex = 9;
            roundInfo.Text = "you are attacking";
            roundInfo.TextAlign = ContentAlignment.TopCenter;
            // 
            // restartButton
            // 
            restartButton.Location = new Point(412, 52);
            restartButton.Name = "restartButton";
            restartButton.Size = new Size(79, 23);
            restartButton.TabIndex = 10;
            restartButton.Text = "RESTART";
            restartButton.UseVisualStyleBackColor = true;
            restartButton.Click += restartButton_Click;
            // 
            // deathButton
            // 
            deathButton.Location = new Point(511, 54);
            deathButton.Name = "deathButton";
            deathButton.Size = new Size(75, 23);
            deathButton.TabIndex = 11;
            deathButton.Text = "death";
            deathButton.UseVisualStyleBackColor = true;
            deathButton.Click += deathButton_Click;
            // 
            // OverlayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(879, 450);
            Controls.Add(deathButton);
            Controls.Add(restartButton);
            Controls.Add(roundInfo);
            Controls.Add(message);
            Controls.Add(timer);
            Controls.Add(moveHandle);
            Controls.Add(playerScore2);
            Controls.Add(playerName2);
            Controls.Add(playerScore1);
            Controls.Add(playerName1);
            Name = "OverlayForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)moveHandle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label playerName1;
        private Label playerScore1;
        private Label playerScore2;
        private Label playerName2;
        private PictureBox moveHandle;
        private Label timer;
        private Label message;
        private Label roundInfo;
        private Button restartButton;
        private Button deathButton;
    }
}