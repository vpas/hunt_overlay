namespace hunt_overlay {
    public partial class OverlayForm : Form {
        private DataController dataController;
        private bool isMovingForm = false;
        private int mouseX;
        private int mouseY;

        public OverlayForm(DataController dataController) {
            Console.WriteLine("OverlayForm constructor");

            this.dataController = dataController;
            dataController.DataUpdated += (object? sender, Match data) => {
                this.Invoke(delegate {
                    UpdateForm(data);
                });
            };

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            InitializeComponent();
            SetTopCenterPosition();

            moveHandle.MouseDown += (object? sender, MouseEventArgs e) => {
                mouseX = e.X;
                mouseY = e.Y;
                isMovingForm = true;
            };
            moveHandle.MouseUp += delegate {
                isMovingForm = false;
            };

            this.moveHandle.MouseMove += (object? sender, MouseEventArgs e) => {
                if (isMovingForm) {
                    var offset = new Point(e.X - mouseX, e.Y - mouseY);
                    this.Location = new Point(this.Location.X + offset.X, this.Location.Y + offset.Y);
                }
            };
        }

        private void UpdateTimer(int millisec) {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(millisec);
            this.timer.Text = $"{timeSpan:mm\\:ss}";
        }

        private void UpdateForm(Match data) {
            this.playerName1.Text = data.playerName1;
            this.playerName2.Text = data.playerName2;
            this.playerScore1.Text = data.playerScore1.ToString();
            this.playerScore2.Text = data.playerScore2.ToString();
            this.roundInfo.Text = data.roundInfo;
            this.message.Text = data.message;
            UpdateTimer(data.timerMillisec);
        }

        private void SetTopCenterPosition(int marginTopPct = 5) {
            // Set the form's start position to manual
            this.StartPosition = FormStartPosition.Manual;

            // Get the primary screen dimensions
            Screen primaryScreen = Screen.PrimaryScreen;
            int screenWidth = primaryScreen.Bounds.Width;
            int screenHeight = primaryScreen.Bounds.Height;

            // Calculate the top center position for the form
            int formWidth = this.Width;
            int topCenterX = (screenWidth - formWidth) / 2;
            int topCenterY = (int)(screenHeight * marginTopPct / 100.0); // Top of the screen

            // Set the form's location to the top center
            this.Location = new Point(topCenterX, topCenterY);
        }

        private void button1_Click(object sender, EventArgs e) {
            Point pt = Cursor.Position; // Get the mouse cursor in screen coordinates

            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) {
                g.DrawEllipse(Pens.Black, pt.X - 10, pt.Y - 10, 20, 20);
            }
        }

        private void restartButton_Click(object sender, EventArgs e) {
            dataController.OnGameReset();
        }

        private void deathButton_Click(object sender, EventArgs e) {
            dataController.OnPlayerDeath();
        }

        //protected override void OnPaintBackground(PaintEventArgs e) {
        //    //empty implementation
        //    e.Graphics.FillRectangle(Brushes.LimeGreen, e.ClipRectangle);

        //}
    }
}