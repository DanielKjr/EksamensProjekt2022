﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EksamensProjekt2022
{
    public class Button
    {

        private SpriteFont buttonFont;
        private Texture2D sprite;
        private bool isHovering;
        private Color hoverColor = Color.White;
        private Rectangle rectangle;
        private string buttonText;
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public MouseState mstate { get; set; }
        public bool IsHovering { get => isHovering; set => isHovering = value; }
        private bool mReleased = true;
        public event EventHandler CLICK;
        public Rectangle background
        {
            get
            {
                return new Rectangle(Rectangle.X * Rectangle.Width, Rectangle.Y * Rectangle.Height, Rectangle.Width, Rectangle.Height);
            }
        }
        public Button(Rectangle ButtonRectangle, String buttonText)
        {
            this.Rectangle = ButtonRectangle;
            this.buttonText = buttonText;
            sprite = GameWorld.Instance.Content.Load<Texture2D>("Pixel");
            buttonFont = GameWorld.Instance.Content.Load<SpriteFont>("Font");
        }
        //if (background.Contains(new Point(mstate.X - (int) GameWorld.Instance._camera.Position.X, mstate.Y - (int) GameWorld.Instance._camera.Position.Y)))
        public void Update(GameTime gameTime)
        {
            Hovering();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Rectangle, hoverColor);
            spriteBatch.DrawString(buttonFont, buttonText, new Vector2(rectangle.Center.X - (buttonFont.MeasureString(buttonText).X / 2), rectangle.Center.Y - (buttonFont.MeasureString(buttonText).Y) / 2), Color.DarkGray);

        }

        public virtual void Hovering()
        {
            mstate = Mouse.GetState();
            if (rectangle.Contains(new Vector2(mstate.X - (int)GameWorld.Instance._camera.Position.X, mstate.Y - (int)GameWorld.Instance._camera.Position.Y)))
            {
                IsHovering = true;
                hoverColor = Color.AntiqueWhite;
                if (mstate.LeftButton == ButtonState.Pressed && mReleased == true)
                {
                    mReleased = false;
                    CLICK?.Invoke(this, new EventArgs());
                }
                mReleased = true;
            }
            else
            {
                IsHovering = false;
                hoverColor = Color.Gray;
            }

        }
    }
    public class PLAYButton : Button
    {
        public PLAYButton(Rectangle ButtonRectangle, string buttonText) : base(ButtonRectangle, buttonText)
        {

        }

        public void Click()
        {
            if (IsHovering)
            {

            }
        }

    }
}
