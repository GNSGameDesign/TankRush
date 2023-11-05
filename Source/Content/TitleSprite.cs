using Microsoft.Xna.Framework.Content;

namespace GameDesign_MonoGameSample1;

public class TitleSprite : ISmartEntity
{
    readonly TextureAtlas _textureAtlas;
    int _currentFrame;
    const float FrameTime = 0.15f;
    float _timeElapsed;
    bool _isForward = true;

    readonly string[] _animationSequence = { "f1", "f2", "f3", "f4", "f5" };

    public TitleSprite(ContentManager content)
    {
        SpriteDefinitions spriteDefinitions = new();
        // each frame is width: 20 height: 4
        spriteDefinitions.AddSprite("f1", new Rectangle(0, 0, 20, 4));
        spriteDefinitions.AddSprite("f2", new Rectangle(0, 4, 20, 4));
        spriteDefinitions.AddSprite("f3", new Rectangle(0, 8, 20, 4));
        spriteDefinitions.AddSprite("f4", new Rectangle(0, 12, 20, 4));
        spriteDefinitions.AddSprite("f5", new Rectangle(0, 16, 20, 4));
        _textureAtlas = new TextureAtlas(content, "MainTitle", spriteDefinitions);
    }

    public void Render(SpriteBatch batch)
    {
        string currentFrameName = _animationSequence[_currentFrame];
        Rectangle spriteRectangle = _textureAtlas.GetSprite(currentFrameName);

        if (spriteRectangle != Rectangle.Empty)
        {
            batch.Draw(
                _textureAtlas.Texture,
                new Vector2(10, 10),
                spriteRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                5f,
                SpriteEffects.None,
                0f
            );
        }
    }

    public void UpdateLogic(GameTime time)
    {
        float deltaTime = (float)time.ElapsedGameTime.TotalSeconds;
        _timeElapsed += deltaTime;
        if (!(_timeElapsed >= FrameTime)) return;
        _timeElapsed = 0;
        if (_isForward)
        {
            _currentFrame++;
            if (_currentFrame < _animationSequence.Length) return;
            _isForward = false;
            _currentFrame = Math.Max(_animationSequence.Length - 2, 0);
        }
        else
        {
            _currentFrame--;
            if (_currentFrame >= 0) return;
            _isForward = true;
            _currentFrame = 1;
        }
    }
}