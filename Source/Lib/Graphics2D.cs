namespace TankRush;

public static class Graphics2D
{
    public static void DrawSprite(
        SpriteBatch batch,
        Texture2D texture,
        float scale,
        Vector2 location,
        float rotation,
        Rectangle? sourceRect
    )
    {
        batch.Draw(texture, location, sourceRect, Color.White, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}