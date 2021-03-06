using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace EksamensProjekt2022
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<Collider> Colliders = new List<Collider>();
        public Texture2D pixel;
        public Random rand = new Random();
        public static float DeltaTime;
        public Thread createDBThread;
        public SoundEffect woodChop;
        public SoundEffect rockHit;
        public Song backgroundBlues;

        public GraphicsDeviceManager Graphics { get => _graphics; }

        private static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }

        }

        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {

            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();
            GameControl.Instance.currentGameState = CurrentGameState.StartMenu;

            createDBThread = new Thread(CreateDB);
            createDBThread.IsBackground = true;
            createDBThread.Start();


            base.Initialize();

        }
        /// <summary>
        /// Creates the database if the file doesn't exist.
        /// </summary>
        private void CreateDB()
        {
            
            if (File.Exists("userinfo.db") == false)
            {
                string sqlConnectionString = "Data Source=userinfo.db;new=True;";
                var sqlConnection = new SQLiteConnection(sqlConnectionString);
                sqlConnection.Open();
                string cmd = File.ReadAllText("CreateUserInfoDB.sql");
                var createDB = new SQLiteCommand(cmd, sqlConnection);
                createDB.ExecuteNonQuery();
                sqlConnection.Close();

            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = Content.Load<Texture2D>("Pixel");
            woodChop = Content.Load<SoundEffect>("SoundEffects/Woodchop");
            rockHit = Content.Load<SoundEffect>("SoundEffects/Pickaxe");
            backgroundBlues = Content.Load<Song>("SoundEffects/Backgroundblues");
            MediaPlayer.Play(backgroundBlues);
            MediaPlayer.IsRepeating = true;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && !StartScreen.databaseIsLoading || Keyboard.GetState().IsKeyDown(Keys.Escape) && !StartScreen.databaseIsLoading && MapCreator.DevMode)
                Exit();
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (MapCreator.DevMode && GameControl.Instance.currentGameState == CurrentGameState.Playing)
            {
                MapCreator.Instance.Update(gameTime);
            }


            GameControl.Instance.UpdateGameState(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (MapCreator.DevMode)
            {//if in devmode/MapCreation, use the MapCreators camera 
                var screenScale = MapCreator.Instance.Camera.GetScreenScale();
                var viewMatrix = MapCreator.Instance.Camera.GetTransform();
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
               null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));

            }
            else
            {//use the GameControl camera
                var screenScale = GameControl.Instance.camera.GetScreenScale();
                var viewMatrix = GameControl.Instance.camera.GetTransform();
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
               null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));
            }


            if (GameControl.Instance.playing.currentGrid != null)
                foreach (Cell item in GameControl.Instance.playing.currentGrid)
                {
                    item.Draw(_spriteBatch);

                }

            for (int i = 0; i < GameControl.Instance.playing.currentGameObjects.Count; i++)
            {
                GameControl.Instance.playing.currentGameObjects[i].Draw(_spriteBatch);

            }

            if (GameControl.Instance.currentGameState == CurrentGameState.StartMenu)
                GameControl.Instance.startScreen.Draw(_spriteBatch);


            if (GameControl.Instance.currentGameState == CurrentGameState.Playing)
                GameControl.Instance.playing.Draw(_spriteBatch);


            if (MapCreator.DevMode)
            {
                GameControl.Instance.playing._debugTools.Draw(_spriteBatch);
                MapCreator.Instance.Draw(_spriteBatch);
            }
          


            if (GameControl.Instance.playing.timeManager != null)
            {
                GameControl.Instance.playing.timeManager.Draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }


        
        public Component FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject gameObject in GameControl.Instance.playing.currentGameObjects)
            {
                Component c = gameObject.GetComponent<T>();

                if (c != null)
                {
                    return c;
                }
            }

            return null;


        }

    }
}
