using Microsoft.Xna.Framework.Content;

namespace GameDesign_MonoGameSample1;

public class TitleSprite : IDumbEntity
{
    Texture2D _texture;
    Dictionary<string, Rectangle> _sourceRectangles;

    public TitleSprite(ContentManager content)
    {
        _texture = content.Load<Texture2D>("");
    }

    public void Render(SpriteBatch batch)
    {
        throw new System.NotImplementedException();
    }
}