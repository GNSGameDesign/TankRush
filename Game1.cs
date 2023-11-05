namespace GameDesign_MonoGameSample1;

public class Game1 : Game
{
    readonly GraphicsDeviceManager _graphics;
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
        TextEntity textEntity = new TextEntity(
            "TankRush",
            Content.Load<SpriteFont>("Pixeled"),
            new Vector2(8, 0),
            SharedConstants.MainPurpleTheme
        );
        _entities.Add(playerTank);
        _entities.Add(textEntity);
    }

    #endregion

    #region Update_Logic

    protected override void Update(GameTime gameTime)
    {
        foreach (IDumbEntity rawEntity in _entities)
        {
            if (rawEntity is Entity r)
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