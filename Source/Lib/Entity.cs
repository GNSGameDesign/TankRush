using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameDesign_MonoGameSample1;

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