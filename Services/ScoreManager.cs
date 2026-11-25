namespace YMH219_FlappyBird.Services;

public class ScoreManager
{
    private readonly string _filePath;
    public int HighScore { get; private set; }

    public ScoreManager(string fileName = "highscore.txt")
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        try
        {
            if (File.Exists(_filePath) && int.TryParse(File.ReadAllText(_filePath).Trim(), out int saved))
            {
                HighScore = saved;
            }
        }
        catch
        {
            HighScore = 0;
        }
    }

    public void UpdateHighScore(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;
            try
            {
                File.WriteAllText(_filePath, HighScore.ToString());
            }
            catch
            {
                // Dosya yazma hatası oyunu kesintiye uğratmamalıdır.
            }
        }
    }
}
