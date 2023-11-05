using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameDesign_MonoGameSample1;

/// <summary>
/// This is a nvery naive implementation of a tank.
/// </summary>
public class Tank : ISmartEntity
{
    #region Tank_Constants

    /// <summary>
    /// Represents the tank speed. Bigger values make it faster
    /// </summary>
    const float TankSpeed = 125f;

    /// <summary>
    /// How fast the tank should spin/turn
    /// </summary>
    const float RotationSpeed = MathHelper.PiOver2;

    const float AccelerationRate = 0.5f; // Adjust the acceleration rate
    const float DecelerationRate = 0.2f; // Adjust the deceleration rate

    #endregion

    #region Tank_InternalProperties

    float _leftTrackPower;
    float _rightTrackPower;

    /// <summary>
    /// Ask yourself: "Where is it?"
    /// </summary>
    Vector2 Location { get; set; }

    /// <summary>
    /// Ask yourself: "Where is it looking at?"
    /// </summary>
    float InternalRotation
    {
        get;
        set;
    } = MathHelper.PiOver2;

    float Rotation
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

    readonly Texture2D _baseTexture;

    #endregion

    #region Constructor

    public Tank(ContentManager content)
    {
        _baseTexture = content.Load<Texture2D>("Tonk");
        Location = Vector2.Zero;
        Rotation = MathHelper.PiOver2;
        Scale = 3f;
    }

    #endregion

    public void Render(SpriteBatch batch)
    {
        Graphics2D.DrawSprite(
            batch: batch,
            location: Location,
            rotation: Rotation,
            texture: _baseTexture,
            scale: Scale,
            sourceRect: null
        );
    }

    public void UpdateLogic(GameTime time)
    {
        #region Logic_Init

        KeyboardState keyboardState = Keyboard.GetState();
        float forwardPower = 0;

        #endregion

        // The F key is used to turn the tank
        // - This literally just means the left tracks get power
        // - The tank will spin clockwisee
        //
        // The J key is the opposite of the F key
        // - This literally just means the right tracks get power
        // - The tank will spin counter-clockwise
        //
        // The R key is used to move the tank backward
        // - Note that I have purposefully made it so that going backwards is slower than forwards by 50%
        // 
        // The I key is used to move the tank forward

        #region Handle_Key_Input

        if (keyboardState.IsKeyDown(Keys.F))
        {
            _leftTrackPower = 1f;
        }
        else if (keyboardState.IsKeyUp(Keys.F))
        {
            _leftTrackPower = 0f;
        }

        if (keyboardState.IsKeyDown(Keys.J))
        {
            _rightTrackPower = 1f;
        }
        else if (keyboardState.IsKeyUp(Keys.J))
        {
            _rightTrackPower = 0f;
        }

        if (keyboardState.IsKeyDown(Keys.R))
        {
            forwardPower = 0.50f; // 100% - 50% 
        }
        else if (keyboardState.IsKeyDown(Keys.I))
        {
            forwardPower = -1f;
        }

        #endregion

        #region Calculations

        float rotationDelta = (_rightTrackPower - _leftTrackPower) * RotationSpeed *
                              (float)time.ElapsedGameTime.TotalSeconds;
        InternalRotation += rotationDelta;
        Vector2 heading = Vector2.Transform(Vector2.UnitY, Matrix.CreateRotationZ(InternalRotation));
        Vector2 movement = heading * forwardPower * TankSpeed * (float)time.ElapsedGameTime.TotalSeconds;

        if (Location.X < 0)
        {
            Location = new Vector2(0, Location.Y);
        }
        else if (Location.X > Game1.Graphics.PreferredBackBufferWidth)
        {
            Location = new Vector2(Game1.Graphics.PreferredBackBufferWidth, Location.Y);
        }
        else if (Location.Y > Game1.Graphics.PreferredBackBufferHeight)
        {
            Location = new Vector2(Location.X, Game1.Graphics.PreferredBackBufferHeight);
        }
        else if (Location.Y < 0)
        {
            Location = new Vector2(Location.X, 0);
        }
        else
        {
            Location += movement;
            Rotation = InternalRotation;
        }

        #endregion
    }
}