namespace YMH219_FlappyBird.Models;

public class Bird
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public int Width { get; } = 36;
    public int Height { get; } = 28;
    public float Velocity { get; private set; }
    public float Gravity { get; } = 0.65f;
    public float JumpStrength { get; } = -9.5f;

    public RectangleF Bounds => new RectangleF(X, Y, Width, Height);

    public Bird(float startX, float startY)
    {
        Reset(startX, startY);
    }

    public void Reset(float startX, float startY)
    {
        X = startX;
        Y = startY;
        Velocity = 0;
    }

    public void Jump()
    {
        Velocity = JumpStrength;
    }

    public void Update()
    {
        Velocity += Gravity;
        if (Velocity > 12f)
            Velocity = 12f;
        Y += Velocity;
    }
}
