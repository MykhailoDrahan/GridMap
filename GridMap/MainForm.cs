namespace GridMap
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DrawBitmap();
        }
        private void DrawBitmap()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }

            pictureBoxMain.Image = bmp;
        }
    }
}
