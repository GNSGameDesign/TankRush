using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankRush;

#region Generic_Entities

public interface IDumbEntity
{
    public void Render(SpriteBatch batch);
}

public interface ISmartEntity : IDumbEntity
{
    public void UpdateLogic(GameTime time);
}

#endregion