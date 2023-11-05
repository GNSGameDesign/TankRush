namespace GameDesign_MonoGameSample1;

public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;
    readonly List<IDumbEntity> _entities;

    #region Constructor

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Window.Title = "TankRush";
        Window.AllowAltF4 = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        _graphics.PreparingDeviceSettings += (_, args) =>
        {
            _graphics.PreferMultiSampling = true;
            args.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
        };
        _graphics.ApplyChanges();
        _entities = new List<IDumbEntity>();
    }

    #endregion

    #region Intialize_Logic

    protected override void Initialize()
    {
        GraphicsDevice.PresentationParameters.MultiSampleCount = 4;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    #endregion

    #region Load_Assets

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Tank playerTank = new Tank(Content.Load<Texture2D>("Asset_MC"));
        playerTank.InitializeLocation(_graphics);
        _entities.Add(new TitleSprite(Content));
        _entities.Add(playerTank);
    }

    #endregion

    #region Update_Logic

    protected override void Update(GameTime gameTime)
    {
        foreach (IDumbEntity rawEntity in _entities)
        {
            switch (rawEntity)
            {
                case Entity r:
                    r.UpdateLogic(gameTime);
                    break;
                case ISmartEntity r2:
                    r2.UpdateLogic(gameTime);
                    break;
            }
        }

        base.Update(gameTime);
    }

    #endregion

    #region Render_Game

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGray);
        foreach (IDumbEntity rawEntity in _entities)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            rawEntity.Render(_spriteBatch);
            _spriteBatch.End();
        }
        base.Draw(gameTime);
    }

    #endregion
}