using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EksamensProjekt2022
{
    public class Chest : Building
    {
        private Color Color = Color.White;
        private SpriteFont font;
        private Texture2D chestInventory;
        private bool updated = false;
        private List<Button> transferButtons = new List<Button>();
        private Rectangle inventoryBox;
        private bool click;
        private MouseState mState;

        public bool Updated { get => updated; set => updated = value; }

        public Chest(Point parentPoint) : base(parentPoint)
        {
            font = GameWorld.Instance.Content.Load<SpriteFont>("Font");
            chestInventory = GameWorld.Instance.Content.Load<Texture2D>("chestInventory");
        }
        public override void Start()
        {
            GameObject.Transform.Position = new Vector2(parentCell.cellVector.X, parentCell.cellVector.Y);
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.OffSetY += 12;
            sr.OffSetX += 19;
            Occupy();
        }
        public override void Update(GameTime gameTime)
        {
            CraftingMenu cm = GameObject.GetComponent<CraftingMenu>() as CraftingMenu;
            mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Released && Updated)
            {
                click = true;
            }
            if (IsSelected)
            {
                GameObject.Tag = "selectedChest";
                UpdateButtons();                
                InputHandler.Instance.uiBox = inventoryBox;
            }
            if (!IsSelected)
            {
                GameObject.Tag = "chest";
                Updated = false;
                InputHandler.Instance.uiBox = Rectangle.Empty;
            }
            foreach (Button button in transferButtons)
            {
                button.Update(gameTime);
            }

           
        }

        public void UpdateButtons()
        {
            Inventory inv = GameObject.GetComponent<Inventory>() as Inventory;

            if (!Updated)
            {
                transferButtons.Clear();
                int i = 0;
                int rowNumber = 0;
                Vector2 firstItemSlot = new Vector2(GameObject.Transform.Position.X - 80, GameObject.Transform.Position.Y - 130);
                foreach (Item item in inv.items)
                {
                    Button itembutton = new Button(inv.items[i]);
                    Button captured_button = itembutton;
                    itembutton.OnClicking += TransferEvent();
                    itembutton.Rectangle = new Rectangle((int)firstItemSlot.X, (int)firstItemSlot.Y, 36, 36);


                    transferButtons.Add(itembutton);
                    firstItemSlot.X += 44;
                    rowNumber++;
                    if (rowNumber >= 4)
                    {
                        firstItemSlot.Y += 44;
                        firstItemSlot.X = GameObject.Transform.Position.X - 80;
                        rowNumber = 0;
                    }
                    i++;

                }
            }
            Updated = true;

        }

        private EventHandler TransferEvent()
        {
            return Transfer;
        }

        public void Transfer(object captured_button, EventArgs e)
        {
            if (click)
            {
                click = false;
                Button button = (Button)captured_button;
                Player player = (Player)GameWorld.Instance.FindObjectOfType<Player>();
                Inventory playerInv = player.GameObject.GetComponent<Inventory>() as Inventory;
                Inventory chestInv = this.GameObject.GetComponent<Inventory>() as Inventory;
                Type type = button.Item.GetType();
                Item item1 = (Item)Activator.CreateInstance(type);
                Item item2 = (Item)Activator.CreateInstance(type);
                item1.Quantity = button.Item.Quantity;
                item2.Quantity = button.Item.Quantity;
                playerInv.AddItem(item2);
                int notRoomFor = playerInv.notAddedAmount;
                int toRemove = item1.Quantity - notRoomFor;
                chestInv.RemoveItem(item1, toRemove);
                playerInv.notAddedAmount = 0;

                GameControl.Instance.playing.userInterface.Updated = false;
                Updated = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Inventory inv = GameObject.GetComponent<Inventory>() as Inventory;
            if (IsSelected)
            {
                inventoryBox = new Rectangle((int)GameObject.Transform.Position.X - 110, (int)GameObject.Transform.Position.Y - 143, 252, 166);
                Vector2 firstItemSlot = new Vector2(GameObject.Transform.Position.X - 80, GameObject.Transform.Position.Y - 130);
                spriteBatch.Draw(chestInventory, inventoryBox, Color.White);
                if (inv != null)
                {
                    foreach (Button button in transferButtons)
                    {
                        spriteBatch.Draw(button.Item.Sprite, button.Rectangle, Color.White);
                        spriteBatch.DrawString(font, $"{button.Item.Quantity}", new Vector2(button.Rectangle.X, button.Rectangle.Y), Color);
                    }
                }
            }
        }

        public override void Occupy()
        {
            Cells.Add((GameControl.Instance.playing.currentGrid.Find(x => x.Position == parentPoint)));
            Cells.Add((GameControl.Instance.playing.currentGrid.Find(x => x.Position == new Point(parentPoint.X + 1, parentPoint.Y))));
            foreach (Cell cell in Cells)
            {
                if (GameControl.Instance.playing.currentGrid.Exists(x => (x.Position == cell.Position)))
                {
                    Cell _cell = GameControl.Instance.playing.currentGrid.First(x => x.Position == cell.Position);
                    _cell.IsWalkable = false;
                }
            }
        }
    }
}
