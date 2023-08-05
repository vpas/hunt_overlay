using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace hunt_overlay {
    internal class GameEventsDetector {
        const string HUNT_WINDOW_TITLE = "Hunt: Showdown";

        public event EventHandler PlayerDeath;
        public event EventHandler PlayerRespawn;

        private Bitmap referenceRegionBitmap;
        private Rectangle screenRegionRect;
        private readonly int checkIntervalMillisec;
        private readonly int maxNormalizedPerPixelDiff;

        public GameEventsDetector(
            int checkIntervalMillisec = 1000, 
            int maxNormalizedPerPixelDiff = 20)
        {
            this.checkIntervalMillisec = checkIntervalMillisec;
            this.maxNormalizedPerPixelDiff = maxNormalizedPerPixelDiff;

            PlayerDeath += delegate { };
            PlayerRespawn += delegate { };
            LoadReferenceRegion();

            var detectorThread = new Thread(new ThreadStart(this.RunDetector)) {
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            };
            detectorThread.Start();
        }

        private void LoadReferenceRegion() {
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;
            if (screenHeight == 1080) {
                referenceRegionBitmap = Properties.Resources.hunt_death_reference_region_1080p;
                screenRegionRect = new Rectangle(
                    location: new Point(
                        int.Parse(Properties.Resources.reference_region_1080p_left_x),
                        int.Parse(Properties.Resources.reference_region_1080p_top_y)
                    ),
                    size: referenceRegionBitmap.Size
                );
            } else if (screenHeight == 1440) {
                referenceRegionBitmap = Properties.Resources.hunt_death_reference_region_1440p;
                screenRegionRect = new Rectangle(
                    location: new Point(
                        int.Parse(Properties.Resources.reference_region_1440p_left_x),
                        int.Parse(Properties.Resources.reference_region_1440p_top_y)
                    ),
                    size: referenceRegionBitmap.Size
                );
            } else if (screenHeight == 1200) {
                referenceRegionBitmap = Properties.Resources.hunt_death_reference_region_1200p;
                screenRegionRect = new Rectangle(
                    location: new Point(
                        int.Parse(Properties.Resources.reference_region_1200p_left_x),
                        int.Parse(Properties.Resources.reference_region_1200p_top_y)
                    ),
                    size: referenceRegionBitmap.Size
                );
            } else {
                Console.WriteLine($"Unsupported screen Height: {screenHeight}");
            }
        }

        private void RunDetector() {
            Console.WriteLine("RunDetector");
            var isPlayerDead = false;
            while (true) {
                Thread.Sleep(checkIntervalMillisec);
                var curWindowTitle = GetCaptionOfActiveWindow();
                Console.WriteLine($"curWindowTitle: {curWindowTitle}");
                if (curWindowTitle == HUNT_WINDOW_TITLE) {
                    var capturedScreenRegionBitmap = CaptureScreenRegion(screenRegionRect);
                    capturedScreenRegionBitmap = NormalizeBitmap(capturedScreenRegionBitmap);
                    var diff = CalcBitmapDiff(referenceRegionBitmap, capturedScreenRegionBitmap);
                    Console.WriteLine($"RunDetector diff: {diff}");
                    var newIsPlayerDead = diff <= maxNormalizedPerPixelDiff;
                    Console.WriteLine($"RunDetector newIsPlayerDead: {newIsPlayerDead}");

                    if (newIsPlayerDead && !isPlayerDead) {
                        PlayerDeath(this, new EventArgs());
                    } else if (!newIsPlayerDead && isPlayerDead) {
                        PlayerRespawn(this, new EventArgs());
                    }
                    isPlayerDead = newIsPlayerDead;
                }
            }
        }

        private static Bitmap NormalizeBitmap(Bitmap bitmap) {
            //int rSum = 0;
            //int gSum = 0;
            //int bSum = 0;
            //for (int x = 0; x < bitmap.Width; x++) {
            //    for (int y = 0; y < bitmap.Height; y++) {
            //        var p = bitmap.GetPixel(x, y);
            //        rSum += p.R;
            //        gSum += p.G;
            //        bSum += p.B;
            //    }
            //}
            return bitmap;
        }

        private static double CalcBitmapDiff(Bitmap bitmap1, Bitmap bitmap2) {
            int diff_sqr_sum = 0;
            Debug.Assert(bitmap1.Size == bitmap2.Size);
            for (int x = 0; x < bitmap1.Size.Width; x++) {
                for (int y = 0; y < bitmap1.Size.Height; y++) {
                    var p1 = bitmap1.GetPixel(x, y);
                    var p2 = bitmap2.GetPixel(x, y);
                    diff_sqr_sum += Math.Abs(p1.R - p2.R) * Math.Abs(p1.R - p2.R);
                    diff_sqr_sum += Math.Abs(p1.G - p2.G) * Math.Abs(p1.G - p2.G);
                    diff_sqr_sum += Math.Abs(p1.B - p2.B) * Math.Abs(p1.B - p2.B);
                }
            }
            return Math.Sqrt(((double)diff_sqr_sum) / (bitmap1.Size.Width * bitmap1.Size.Height * 3));
        }

        private static Bitmap CaptureScreenRegion(Rectangle rect) {
            Bitmap bmp = new(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bmp)) {
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
            }

            return bmp;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        private string GetCaptionOfActiveWindow() {
            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            // Obtain the length of the text   
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0) {
                strTitle = stringBuilder.ToString();
            }
            return strTitle;
        }
    }
}
