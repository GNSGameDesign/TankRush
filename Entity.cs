using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDesign_MonoGameSample1;

#region Dumb_Entity

public interface IDumbEntity
{
    public void Render(SpriteBatch batch);
}

#endregion

#region Logical_Entity

/// <summary>
/// Represents the basic implementation of all of the entities in our game.
/// </summary>
public abstract class Entity : IDumbEntity
{
    /// <summary>
    /// Ask yourself: "Where is it?"
    /// </summary>
    protected Vector2 Location { get; set; }

    /// <summary>
    /// Ask yourself: "Where is it looking at?"
    /// </summary>
    protected float InternalRotation
    {
        get;
        set;
    } = MathHelper.PiOver2;

    protected float Rotation
    {
        get => InternalRotation;
        set
        {
            InternalRotation = value;
            if (InternalRotation < 0)
                InternalRotation += MathHelper.TwoPi;
            if (InternalRotation >= MathHelper.TwoPi)
                InternalRotation -= MathHelper.TwoPi;
        }
    }

    float Scale { get; }

    /// <summary>
    /// Ask yourself: "What does it look like?"
    /// </summary>
    readonly Texture2D _texture;

    protected Entity(Texture2D texture, float scale = 3.5f)
    {
        _texture = texture;
        Location = Vector2.Zero;
        Rotation = MathHelper.PiOver2;
        Scale = scale;
    }

    /// <summary>
    /// Accepts the location and sets the location of this sprite to be at the center of the canvas
    /// </summary>
    /// <param name="graphicsDeviceManager">Required from the Game Class</param>
    public void InitializeLocation(GraphicsDeviceManager graphicsDeviceManager)
    {
        Location = new Vector2(
            graphicsDeviceManager.GraphicsDevice.Viewport.Width / 2f - _texture.Width,
            graphicsDeviceManager.GraphicsDevice.Viewport.Height / 2f - _texture.Height
        );
    }

    /// <summary>
    /// To be inherited by Entities that needs to update their logic over time
    /// </summary>
    /// <param name="time">Used for delta time from Game Class</param>
    public abstract void UpdateLogic(GameTime time);

    /// <summary>
    /// Not really to be inherited and usually just for rendering the sprite. It can be overriden.
    /// </summary>
    /// <param name="batch">SpriteBatch from Game Class</param>
    public virtual void Render(SpriteBatch batch)
    {
        if (!batch.IsDisposed)
        {
            batch.Draw(
                texture: _texture,
                position: Location,
                sourceRectangle: null,
                color: Color.White,
                rotation: Rotation,
                origin: new Vector2(_texture.Width / 2f, _texture.Height / 2f),
                scale: Scale,
                effects: SpriteEffects.None,
                layerDepth: 0
            );
        }
    }
}

#endregion