namespace TankRush;

public class Game1 : Game
{
    internal static GraphicsDeviceManager Graphics;
    SpriteBatch _spriteBatch;
    readonly List<IDumbEntity> _entities;

    #region Constructor

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Window.Title = "TankRush";
        Window.AllowAltF4 = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.PreparingDeviceSettings += (_, args) =>
        {
            Graphics.PreferMultiSampling = true;
            args.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
        };
        Graphics.ApplyChanges();
        _entities = new List<IDumbEntity>();
    }

    #endregion

    #region Load_Assets

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _entities.Add(new TitleSprite(Content));
        _entities.Add(new Tank(Content));
    }

    #endregion

    #region Update_Logic

    protected override void Update(GameTime gameTime)
    {
        foreach (IDumbEntity rawEntity in _entities)
        {
            if (rawEntity is ISmartEntity r)
            {
                r.UpdateLogic(gameTime);
            }
        }

        base.Update(gameTime);
    }

    #endregion

    #region Render_Game

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGray);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        foreach (IDumbEntity rawEntity in _entities)
        {
            rawEntity.Render(_spriteBatch);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    #endregion
}