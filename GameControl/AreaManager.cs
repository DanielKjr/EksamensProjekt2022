﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EksamensProjekt2022
{
    public class AreaManager
    {
        public List<GameObject>[] currentGameObjects = new List<GameObject>[4];
        public List<Cell>[] currentGrid = new List<Cell>[4];

        public Dictionary<Point, Cell>[] currentCells = new Dictionary<Point, Cell>[4];


        private Area currentArea;
        private Thread loadAreasThread;
        private bool HasRun = false;


        public AreaManager()
        {

            currentArea = Area.Camp;
            for (int i = 0; i < currentGameObjects.Length; i++)
            {
                currentGameObjects[i] = new List<GameObject>();

            }
            if (!HasRun)
            {//To ensure it only runs once
                CreateGrids();
                HasRun = true;
            }

        }

        /// <summary>
        /// Creates a thread that creates the grids used in all areas, intended so that the main thread only creates the first zone.
        /// </summary>
        public void CreateGrids()
        {

            loadAreasThread = new Thread(LoadArea);
            loadAreasThread.IsBackground = true;
            loadAreasThread.Start();
        }

        /// <summary>
        /// Saves any changes on the active list onto the AreaManager instances' list before changing to the next area and loading them.
        /// </summary>
        /// <param name="currentArea"></param>
        /// <param name="nextArea"></param>
        public void AreaChange(Area currentArea, Area nextArea)
        {
            if (GameControl.Instance.playing.newGameObjects.Count == 0)
            {
                currentGameObjects[(int)currentArea] = GameControl.Instance.playing.currentGameObjects;
                GameControl.Instance.playing.currentGameObjects = currentGameObjects[(int)nextArea];

                LoadGameObjects();

                currentGrid[(int)currentArea] = GameControl.Instance.playing.currentGrid;
                GameControl.Instance.playing.currentGrid = currentGrid[(int)nextArea];
                this.currentArea = nextArea;
            }



        }


        /// <summary>
        /// Creates Grid and Cells before running their load content methods.
        /// Method exists as an overview of the Threads tasks.
        /// </summary>
        public void LoadArea()
        {
            CreateGrid();
            CreateCells();
            LoadGridAndCell();

        }

        public void CreateGrid()
        {
            for (int i = 1; i < currentGrid.Length; i++)
            {
                currentGrid[i] = GameControl.Instance.playing.grid.CreateGrid();
            }
        }

        public void CreateCells()
        {
            for (int i = 1; i < currentCells.Length; i++)
            {
                currentCells[i] = GameControl.Instance.playing.grid.CreateCells();
            }
        }

        public void LoadGridAndCell()
        {


            for (int i = 1; i < currentGrid.Length; i++)
            {
                foreach (Cell c in currentGrid[i])
                {
                    c.LoadContent();

                }

                //måske unødning
                foreach (Cell item in currentCells[i].Values)
                {
                    item.LoadContent();
                }
            }

        }

        public void LoadGameObjects()
        {
            foreach (GameObject go in GameControl.Instance.playing.currentGameObjects)
            {
                go.Start();
            }

        }

    }
}
