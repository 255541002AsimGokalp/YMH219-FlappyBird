using System.Drawing.Drawing2D;
using YMH219_FlappyBird.Engine;

namespace YMH219_FlappyBird.Forms;

public partial class MainForm : Form
{
    private readonly GameEngine _engine;

    public MainForm()
    {
        InitializeComponent();
        PerformLayout();
        _engine = new GameEngine(ClientSize.Width, ClientSize.Height - pnlTop.Height);

        UpdateHud();
        gameTimer.Start();
    }

    private void GameTimer_Tick(object? sender, EventArgs e)
    {
        _engine.Update();
        UpdateHud();
        pnlGame.Invalidate();
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
        {
            if (_engine.State == GameState.GameOver)
            {
                _engine.ResetGame();
                _engine.StartOrJump();
            }
            else
            {
                _engine.StartOrJump();
            }
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.R)
        {
            _engine.ResetGame();
            UpdateHud();
            pnlGame.Invalidate();
        }
    }

    private void PnlGame_MouseDown(object? sender, MouseEventArgs e)
    {
        if (_engine.State == GameState.GameOver)
            _engine.ResetGame();
        _engine.StartOrJump();
    }

    private void BtnRestart_Click(object? sender, EventArgs e)
    {
        _engine.ResetGame();
        UpdateHud();
        pnlGame.Invalidate();
        Focus();
    }

    private void UpdateHud()
    {
        lblScore.Text = $"Skor: {_engine.Score}";
        lblHighScore.Text = $"En Yüksek: {_engine.HighScore}";
        lblSpeed.Text = $"Hız: {_engine.PipeSpeed:F1}";
    }

    private void PnlGame_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Engeller (Borular)
        using var pipeBrush = new SolidBrush(Color.FromArgb(34, 197, 94));
        using var pipeBorder = new Pen(Color.FromArgb(21, 128, 61), 2);
        foreach (var pipe in _engine.Pipes)
        {
            var topRect = pipe.GetTopBounds();
            var botRect = pipe.GetBottomBounds(_engine.GroundY);

            g.FillRectangle(pipeBrush, topRect);
            g.DrawRectangle(pipeBorder, topRect.X, topRect.Y, topRect.Width, topRect.Height);

            g.FillRectangle(pipeBrush, botRect);
            g.DrawRectangle(pipeBorder, botRect.X, botRect.Y, botRect.Width, botRect.Height);
        }

        // Zemin
        using var groundBrush = new SolidBrush(Color.FromArgb(180, 130, 70));
        using var grassBrush = new SolidBrush(Color.FromArgb(22, 163, 74));
        g.FillRectangle(groundBrush, 0, _engine.GroundY, pnlGame.Width, _engine.GroundHeight);
        g.FillRectangle(grassBrush, 0, _engine.GroundY, pnlGame.Width, 12);

        // Kuş
        var b = _engine.Bird.Bounds;
        using var birdBrush = new SolidBrush(Color.Gold);
        using var birdPen = new Pen(Color.DarkOrange, 2);
        g.FillEllipse(birdBrush, b);
        g.DrawEllipse(birdPen, b);
        // Göz ve gaga
        g.FillEllipse(Brushes.White, b.X + 21, b.Y + 6, 9, 9);
        g.FillEllipse(Brushes.Black, b.X + 25, b.Y + 8, 4, 4);
        PointF[] beak = { new(b.Right - 2, b.Y + 12), new(b.Right + 10, b.Y + 16), new(b.Right - 2, b.Y + 20) };
        g.FillPolygon(Brushes.OrangeRed, beak);

        // Durum mesajları
        if (_engine.State == GameState.Ready)
        {
            DrawCenterOverlay(g, "FLAPPY BIRD", "Başlamak için SPACE veya Yukarı Ok tuşuna basın");
        }
        else if (_engine.State == GameState.GameOver)
        {
            DrawCenterOverlay(g, "GAME OVER", $"Skor: {_engine.Score} | En Yüksek: {_engine.HighScore}" + Environment.NewLine + "Tekrar oynamak için R veya SPACE tuşuna basın");
        }
    }

    private void DrawCenterOverlay(Graphics g, string title, string subtitle)
    {
        var box = new Rectangle(pnlGame.Width / 2 - 220, pnlGame.Height / 2 - 85, 440, 140);
        using var bg = new SolidBrush(Color.FromArgb(190, 15, 23, 42));
        g.FillRectangle(bg, box);
        g.DrawRectangle(Pens.White, box);

        using var titleFont = new Font("Segoe UI", 22, FontStyle.Bold);
        using var subFont = new Font("Segoe UI", 11, FontStyle.Regular);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        g.DrawString(title, titleFont, Brushes.Gold, new Rectangle(box.X, box.Y + 15, box.Width, 45), sf);
        g.DrawString(subtitle, subFont, Brushes.White, new Rectangle(box.X + 10, box.Y + 62, box.Width - 20, 65), sf);
    }
}
