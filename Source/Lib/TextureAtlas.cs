using Microsoft.Xna.Framework.Content;

namespace GameDesign_MonoGameSample1;

public class SpriteDefinition
{
    public Rectangle SourceRectangle { get; init; }
}

public class SpriteDefinitions
{
    public Dictionary<string, SpriteDefinition> Sprites { get; } = new();

    public void AddSprite(string name, Rectangle sourceRectangle)
    {
        Sprites.Add(name, new SpriteDefinition { SourceRectangle = sourceRectangle });
    }
}

public class TextureAtlas
{
    readonly Dictionary<string, Rectangle> _spriteRectangles;

    public TextureAtlas(ContentManager content, string assetName, SpriteDefinitions spriteDefinitions)
    {
        Texture = content.Load<Texture2D>(assetName);
        _spriteRectangles = spriteDefinitions.Sprites.ToDictionary(kv => kv.Key, kv => kv.Value.SourceRectangle);
    }

    public Rectangle GetSprite(string spriteName)
    {
        if (_spriteRectangles.TryGetValue(spriteName, out Rectangle rectangle))
        {
            return rectangle;
        }

        throw new ArgumentException($"Sprite rectangle not found for {spriteName}");
    }

    public Texture2D Texture { get; }
}