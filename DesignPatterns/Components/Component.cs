using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EksamensProjekt2022
{
    public abstract class Component
    {
        public GameObject GameObject { get; set; }

        public virtual void Start()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
