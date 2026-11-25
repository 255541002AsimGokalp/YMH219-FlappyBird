using YMH219_FlappyBird.Models;
using YMH219_FlappyBird.Services;

namespace YMH219_FlappyBird.Engine;

public enum GameState
{
    Ready,
    Running,
    GameOver
}

public class GameEngine
{
    private readonly Random _random = new();
    private readonly ScoreManager _scoreManager = new();

    public int FieldWidth { get; }
    public int FieldHeight { get; }
    public int GroundHeight { get; } = 60;
    public int GroundY => FieldHeight - GroundHeight;

    public Bird Bird { get; }
    public List<PipePair> Pipes { get; } = new();
    public GameState State { get; private set; } = GameState.Ready;
    public int Score { get; private set; }
    public int HighScore => _scoreManager.HighScore;
    public float PipeSpeed { get; private set; } = 4.2f;

    public GameEngine(int fieldWidth, int fieldHeight)
    {
        FieldWidth = fieldWidth;
        FieldHeight = fieldHeight;
        Bird = new Bird(110, fieldHeight / 2f - 30);
        ResetGame();
    }

    public void ResetGame()
    {
        Bird.Reset(110, FieldHeight / 2f - 30);
        Score = 0;
        PipeSpeed = 4.2f;
        State = GameState.Ready;
        Pipes.Clear();

        int spacing = 260;
        for (int i = 0; i < 3; i++)
        {
            Pipes.Add(new PipePair(FieldWidth + 120 + i * spacing, GenerateRandomTopHeight()));
        }
    }

    public void StartOrJump()
    {
        if (State == GameState.Ready)
        {
            State = GameState.Running;
            Bird.Jump();
        }
        else if (State == GameState.Running)
        {
            Bird.Jump();
        }
    }

    public void Update()
    {
        if (State != GameState.Running)
            return;

        Bird.Update();

        // Tavan veya zemin kontrolü
        if (Bird.Y < 0)
        {
            Bird.Reset(Bird.X, 0);
        }

        if (Bird.Y + Bird.Height >= GroundY)
        {
            TriggerGameOver();
            return;
        }

        float rightmostX = Pipes.Max(p => p.X);

        foreach (var pipe in Pipes)
        {
            pipe.Move(PipeSpeed);

            // Skor kontrolü
            if (!pipe.IsScored && pipe.X + pipe.Width < Bird.X)
            {
                pipe.IsScored = true;
                Score++;
                // Zorluk artışı
                if (Score % 5 == 0 && PipeSpeed < 8.5f)
                {
                    PipeSpeed += 0.45f;
                }
            }

            // Çarpışma kontrolü
            if (Bird.Bounds.IntersectsWith(pipe.GetTopBounds()) ||
                Bird.Bounds.IntersectsWith(pipe.GetBottomBounds(GroundY)))
            {
                TriggerGameOver();
                return;
            }

            // Ekran dışına çıkan engeli yeniden konumlandır
            if (pipe.X + pipe.Width < 0)
            {
                rightmostX += 260;
                pipe.X = rightmostX;
                pipe.TopPipeHeight = GenerateRandomTopHeight();
                pipe.IsScored = false;
            }
        }
    }

    private int GenerateRandomTopHeight()
    {
        int minTop = 60;
        int maxTop = GroundY - 210;
        return _random.Next(minTop, Math.Max(minTop + 20, maxTop));
    }

    private void TriggerGameOver()
    {
        State = GameState.GameOver;
        _scoreManager.UpdateHighScore(Score);
    }
}
