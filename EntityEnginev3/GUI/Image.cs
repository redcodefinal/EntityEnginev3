using EntityEnginev3.Components;
using EntityEnginev3.Components.Render;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.GUI
{
    public class Image : Entity
    {
        public Body Body;
        public Physics Physics;
        public ImageRender ImageBaseRender;

        public Image(EntityState stateref, IComponent parent, string name)
            : base(stateref, parent, name)
        {
            Body = new Body(this, "Body");
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            ImageBaseRender = new ImageRender(this, "ImageRender");
            AddComponent(ImageBaseRender);
        }

        public Image(EntityState stateref, IComponent parent, string name, Texture2D texture, Vector2 position)
            : base(stateref, parent, name)
        {
            Body = new Body(this, "Body", position);
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            ImageBaseRender = new ImageRender(this, "ImageRender", texture);
            AddComponent(ImageBaseRender);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}