﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EksamensProjekt2022
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Camera _camera;
        private DebugTool _debugTools;
        private AreaManager areaManager;
        private TimeManager timeManager;
        private MainMenu mainmenu;

        public List<GameObject> currentGameObjects = new List<GameObject>();
        public List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destroyedGameObjects = new List<GameObject>();

        static public Dictionary<Point, Cell> Cells = new Dictionary<Point, Cell>();
        public Dictionary<Point, Cell> currentCells = new Dictionary<Point, Cell>();
        public List<Collider> Colliders = new List<Collider>();

        public Grid _grid;
        public List<Cell> currentGrid;
    
        public static float DeltaTime;

        public GraphicsDeviceManager Graphics { get => _graphics; }
        public int CellSize { get; set; }
        public int CellCount { get; set; }

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
            //CellCount = 80;
            //CellSize = 36;
            //_grid = new Grid(CellCount, CellSize, CellSize);
            //_camera = new Camera();

        }

        protected override void Initialize()
        {

            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            Graphics.ApplyChanges();
          //  mainmenu = new MainMenu();
            //GameObject player = new GameObject();
            //player.AddComponent(new Player());
            //player.AddComponent(new SpriteRenderer());
            //player.AddComponent(new Collider());
            //Instantiate(player);

            //_debugTools = new DebugTool();
            //areaManager = new AreaManager();
            //timeManager = new TimeManager();


            //areaManager.currentGrid[0] = _grid.CreateGrid();
            //areaManager.currentCells[0] = _grid.CreateCells();

            //currentCells = areaManager.currentCells[0];
            //currentGameObjects = areaManager.currentGameObjects[0];
            //currentGrid = areaManager.currentGrid[0];

            //TODO lav en method der instantiere med 1/8 del af teksten
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((TreeFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(5, 5)), 500)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((TreeFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(5, 6)), 500)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((TreeFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(6, 5)), 500)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((TreeFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(5, 7)), 500)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((BoulderFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(10, 15)), 100)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((BoulderFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(10, 12)), 100)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((BoulderFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(10, 11)), 100)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((BoulderFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(8, 5)), 100)));
            //areaManager.currentGameObjects[(int)CurrentArea.River].Add((BoulderFactory.Instance.CreateGameObject(areaManager.currentGrid[(int)CurrentArea.River].Find(x => x.Position == new Point(7, 15)), 100)));



            //for (int i = 0; i < currentGameObjects.Count; i++)
            //{
            //    currentGameObjects[i].Awake();
            //}

            base.Initialize();

        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //for (int i = 0; i < currentGameObjects.Count; i++)
            //{
            //    currentGameObjects[i].Start();
            //}
            //foreach (Cell item in currentGrid)
            //{
            //    item.LoadContent();
            //}
           

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //_debugTools.Update(gameTime);
            KeyboardState keystate = Keyboard.GetState();

            //TODO flyt ind i inputhandler eller lignende
            if (keystate.IsKeyDown(Keys.W))
            {
                GameControl.Instance.camera.Move(new Vector2(0, 8));
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                GameControl.Instance.camera.Move(new Vector2(8, 0));
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                GameControl.Instance.camera.Move(new Vector2(-8, 0));
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                GameControl.Instance.camera.Move(new Vector2(0, -8));
            }


            //foreach (Cell item in currentGrid)
            //{
            //    item.Update(gameTime);
            //}
            //for (int i = 0; i < currentGameObjects.Count; i++)
            //{
            //    currentGameObjects[i].Update(gameTime);
            //}

            //timeManager.Update(gameTime);

            foreach (Button item in GameControl.Instance.mainmenu.Buttons)
            {
                item.Update(gameTime);
            }
            //CleanUp();

            ////TODO flyt ind i anden klasser der håndtere skiftet
            //Player player = (Player)GameWorld.Instance.FindObjectOfType<Player>();
            //if (player != null && player.currentCell.Position == currentCells[new Point(1, 1)].Position)
            //{
            //    player.MyArea = CurrentArea.River;
            //    areaManager.ChangeArea(player.MyArea);
            //    currentGameObjects.Remove(player.GameObject);
            //    currentCells = areaManager.currentCells[(int)player.MyArea];
            //    currentGrid = areaManager.currentGrid[(int)player.MyArea];
            //    currentGameObjects = areaManager.currentGameObjects[(int)player.MyArea];
            //    currentGameObjects.Add(player.GameObject);

            //    foreach (GameObject go in currentGameObjects)
            //    {
            //        if (go.GetComponent<Player>() == null)
            //        {
            //            go.Start();
            //        }
            //    }


            //   }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            var screenScale = GameControl.Instance.camera.GetScreenScale();
            var viewMatrix = GameControl.Instance.camera.GetTransform();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
                null, null, null, null, viewMatrix * Matrix.CreateScale(screenScale));

            if (currentGrid != null)
                foreach (Cell item in GameControl.Instance.currentGrid)
                {
                    item.Draw(_spriteBatch);

                }

            for (int i = 0; i < GameControl.Instance.currentGameObjects.Count; i++)
            {
                GameControl.Instance.currentGameObjects[i].Draw(_spriteBatch);

            }

          
            foreach (Button item in GameControl.Instance.mainmenu.Buttons)
            {
                item.Draw(_spriteBatch);
            }

         //   GameControl.Instance.timeManager.draw(_spriteBatch);
            GameControl.Instance._debugTools.Draw(_spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        public void Destroy(GameObject go)
        {
            destroyedGameObjects.Add(go);
        }

        /// <summary>
        /// Adds new objects to gameObjects list, runs Awake and Start.
        /// while also removing all the objects that are destroyed before clearing both lists
        /// </summary>
        public void CleanUp()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                currentGameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();

            }

            for (int i = 0; i < destroyedGameObjects.Count; i++)
            {
                currentGameObjects.Remove(destroyedGameObjects[i]);
            }

            destroyedGameObjects.Clear();
            newGameObjects.Clear();

        }

        public Component FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject gameObject in currentGameObjects)
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
