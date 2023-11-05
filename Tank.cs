using Microsoft.Xna.Framework.Input;

namespace GameDesign_MonoGameSample1;

/// <summary>
/// This is a nvery naive implementation of a tank.
/// </summary>
public class Tank : Entity
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

    #endregion

    #region Constructor

    public Tank(Texture2D texture) : base(texture)
    {
    }

    #endregion

    public override void UpdateLogic(GameTime time)
    {
        #region Logic_Init

        KeyboardState keyboardState = Keyboard.GetState();
        float forwardPower = 0;
        float rotationDelta = 0;

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

        rotationDelta = (_rightTrackPower - _leftTrackPower) * RotationSpeed * (float)time.ElapsedGameTime.TotalSeconds;
        InternalRotation += rotationDelta;
        Vector2 heading = Vector2.Transform(Vector2.UnitY, Matrix.CreateRotationZ(InternalRotation));
        Vector2 movement = heading * forwardPower * TankSpeed * (float)time.ElapsedGameTime.TotalSeconds;
        Location += movement;
        Rotation = InternalRotation;

        #endregion
    }
}