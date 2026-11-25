namespace YMH219_FlappyBird.Models;

public class PipePair
{
    public float X { get; set; }
    public int Width { get; } = 68;
    public int GapHeight { get; set; } = 145;
    public int TopPipeHeight { get; set; }
    public bool IsScored { get; set; }

    public PipePair(float startX, int topPipeHeight, int gapHeight = 145)
    {
        X = startX;
        TopPipeHeight = topPipeHeight;
        GapHeight = gapHeight;
        IsScored = false;
    }

    public RectangleF GetTopBounds() => new RectangleF(X, 0, Width, TopPipeHeight);

    public RectangleF GetBottomBounds(int groundY)
    {
        int bottomY = TopPipeHeight + GapHeight;
        int bottomHeight = Math.Max(0, groundY - bottomY);
        return new RectangleF(X, bottomY, Width, bottomHeight);
    }

    public void Move(float speed)
    {
        X -= speed;
    }
}
