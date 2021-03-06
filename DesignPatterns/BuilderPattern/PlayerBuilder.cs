using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EksamensProjekt2022
{
    public class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject(int health, int energy, int hunger, int x, int y, int time)
        {
            gameObject = new GameObject();
            gameObject.Tag = "Player";
            BuildComponents(health, energy, hunger, x, y, time);

        }

        /// <summary>
        /// Attaches the necessary Components for the player and loads the SurvivalAspect attributes from the database.
        /// </summary>
        /// <param name="health"></param>
        /// <param name="energy"></param>
        /// <param name="hunger"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="time"></param>
        private void BuildComponents(int health, int energy, int hunger, int x, int y, int time)
        {
            Player p = (Player)gameObject.AddComponent(new Player());
            p.MyArea = Area.Camp;

            SpriteRenderer sr =  (SpriteRenderer)gameObject.AddComponent(new SpriteRenderer());
            sr.SetSprite("MinerTest");
            Collider c = (Collider)gameObject.AddComponent(new Collider());

            Animator a = (gameObject).AddComponent(new Animator()) as Animator;
            a.AddAnimation(BuildAnimation("playerWalkUp", "playerWalkUp"));
            a.AddAnimation(BuildAnimation("playerWalkDown", "playerWalkDown"));
            a.AddAnimation(BuildAnimation("playerWalkLeft", "playerWalkLeft"));
            a.AddAnimation(BuildAnimation("playerWalkRight", "playerWalkRight"));
            a.AddAnimation(BuildAnimation("playerIdle", "playerIdle"));

            Inventory inv = (Inventory)gameObject.AddComponent(new Inventory(8)) as Inventory;

            SurvivalAspect sa = (SurvivalAspect)gameObject.AddComponent(new SurvivalAspect(health, 50, hunger, energy)) as SurvivalAspect;
            Point position = new Point(x, y);
            p.currentCell = GameControl.Instance.playing.currentGrid.Find(x => x.Position == position);
            p.GameObject.Transform.Position = p.currentCell.cellVector;


            GameControl.Instance.playing.timeManager.Time = time;

        }

        /// <summary>
        /// Builds an animation by loading the spritesheet
        /// </summary>
        /// <param name="animationName"></param>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        private Animation BuildAnimation(string animationName, string spriteName)
        {
            Texture2D sprite = GameWorld.Instance.Content.Load<Texture2D>($"{spriteName}");

            Animation animation = new Animation(animationName, sprite, 3, 10);

            return animation;
        }
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
