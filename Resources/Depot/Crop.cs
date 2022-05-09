﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EksamensProjekt2022
{
    public enum GrowthState
    {
        Sprout,
        Budding,
        Flowering,
        Ripe
    }
    public class Crop : ResourceDepot
    {
        private Thread growthCycle;

        private GrowthState _growthState;
        public Crop(Point position, int maxAmount) : base(position, maxAmount)
        {
            this._growthState = GrowthState.Sprout;
            growthCycle = new Thread(Growth);
            growthCycle.IsBackground = true;
            growthCycle.Start();
        }

        public void Growth()
        {
            _growthState = GrowthState.Sprout;
            Thread.Sleep(6000);
            _growthState = GrowthState.Budding;
            Thread.Sleep(6000);
            _growthState = GrowthState.Flowering;
            Thread.Sleep(6000);
            _growthState = GrowthState.Ripe;
            
        }
    }
}