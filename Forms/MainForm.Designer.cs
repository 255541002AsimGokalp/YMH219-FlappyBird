namespace YMH219_FlappyBird.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private Panel pnlGame = null!;
    private Panel pnlTop = null!;
    private Label lblScore = null!;
    private Label lblHighScore = null!;
    private Label lblSpeed = null!;
    private Button btnRestart = null!;
    private System.Windows.Forms.Timer gameTimer = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        pnlTop = new Panel();
        lblScore = new Label();
        lblHighScore = new Label();
        lblSpeed = new Label();
        btnRestart = new Button();
        pnlGame = new DoubleBufferedPanel();
        gameTimer = new System.Windows.Forms.Timer(components);

        SuspendLayout();

        // pnlTop
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Height = 52;
        pnlTop.BackColor = Color.FromArgb(30, 41, 59);
        pnlTop.Padding = new Padding(12, 8, 12, 8);

        lblScore.AutoSize = true;
        lblScore.ForeColor = Color.White;
        lblScore.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblScore.Location = new Point(14, 14);
        lblScore.Text = "Skor: 0";

        lblHighScore.AutoSize = true;
        lblHighScore.ForeColor = Color.Gold;
        lblHighScore.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblHighScore.Location = new Point(150, 15);
        lblHighScore.Text = "En Yüksek: 0";

        lblSpeed.AutoSize = true;
        lblSpeed.ForeColor = Color.LightSkyBlue;
        lblSpeed.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        lblSpeed.Location = new Point(320, 16);
        lblSpeed.Text = "Hız: 4.2";

        btnRestart.Text = "Yeniden Başlat (R)";
        btnRestart.Size = new Size(150, 34);
        btnRestart.Location = new Point(515, 9);
        btnRestart.BackColor = Color.FromArgb(16, 185, 129);
        btnRestart.ForeColor = Color.White;
        btnRestart.FlatStyle = FlatStyle.Flat;
        btnRestart.TabStop = false;
        btnRestart.Click += BtnRestart_Click;

        pnlTop.Controls.Add(lblScore);
        pnlTop.Controls.Add(lblHighScore);
        pnlTop.Controls.Add(lblSpeed);
        pnlTop.Controls.Add(btnRestart);

        // pnlGame
        pnlGame.Dock = DockStyle.Fill;
        pnlGame.BackColor = Color.FromArgb(135, 206, 235);
        pnlGame.Paint += PnlGame_Paint;
        pnlGame.MouseDown += PnlGame_MouseDown;

        // gameTimer
        gameTimer.Interval = 20;
        gameTimer.Tick += GameTimer_Tick;

        // MainForm
        ClientSize = new Size(680, 560);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        KeyPreview = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "YMH 219 - Flappy Bird";
        Controls.Add(pnlGame);
        Controls.Add(pnlTop);
        KeyDown += MainForm_KeyDown;

        ResumeLayout(false);
    }
}

public class DoubleBufferedPanel : Panel
{
    public DoubleBufferedPanel()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }
}
