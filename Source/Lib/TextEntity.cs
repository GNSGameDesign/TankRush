namespace GameDesign_MonoGameSample1;

public class TextEntity : IDumbEntity
{
    #region Text_Properties

    string Representation { set; get; }
    SpriteFont Font { set; get; }
    Vector2 Location { set; get; }
    Color Color { set; get; }

    #endregion

    public TextEntity(string representation, SpriteFont font, Vector2 location, Color color)
    {
        Representation = representation;
        Font = font;
        Location = location;
        Color = color;
    }

    public void Render(SpriteBatch batch)
    {
        batch.DrawString(
            Font,
            Representation,
            Location,
            Color,
            0,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            0
        );
    }
}