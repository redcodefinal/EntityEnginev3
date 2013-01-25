using System;
using System.Collections.Generic;
using EntityEnginev3.Components.Render;
using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Collision
{
    public class Collision : Component
    {
        public List<Entity> Partners = new List<Entity>();
        public List<Entity> CollidedWith = new List<Entity>();
        public Rectangle Bounds;
        public bool Debug;
        public Color DebugColor = Color.PowderBlue;

        public bool Colliding
        {
            get { return (CollidedWith.Count > 0); }
        }

        public Rectangle BoundingBox
        {
            get
            {
                Vector2 position = Parent.GetComponent<Body>().Position; ;
                Vector2 scale;
                try
                {
                    scale = Parent.GetComponent<BaseRender>().Scale;
                }
                catch
                {
                    scale = Vector2.One;
                }

                return new Rectangle(
                    (int)(Bounds.X + position.X),
                    (int)(Bounds.Y + position.Y),
                    (int)(Bounds.Width * scale.X),
                    (int)(Bounds.Height * scale.Y));
            }
        }

        public event Entity.EventHandler CollideEvent;

        public Collision(Entity e, string name)
            : base(e, name)
        {
            try
            {
                var width = Parent.GetComponent<BaseRender>().DrawRect.Width;
                var height = Parent.GetComponent<BaseRender>().DrawRect.Height;
                Bounds = new Rectangle(0, 0, width, height);
            }
            catch (Exception)
            {
                Bounds = new Rectangle();
            }
        }

        public override void Update()
        {
            //Erase the collided with list every frame
            CollidedWith.Clear();
            foreach (var p in Partners.ToArray())
            {
                if (TestCollision(p))
                {
                    CollidedWith.Add(p);
                    if (CollideEvent != null)
                        CollideEvent(p);
                }
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            if (Debug)
            {
                Rectangle drawwindow;
                //Draw top
                drawwindow = new Rectangle(BoundingBox.X, BoundingBox.Y, BoundingBox.Width, 1);
                sb.Draw(Assets.Pixel, drawwindow, null, DebugColor, 0, Vector2.Zero, SpriteEffects.None, 1f);

                //Draw bottom
                drawwindow = new Rectangle(BoundingBox.X, BoundingBox.Bottom, BoundingBox.Width, 1);
                sb.Draw(Assets.Pixel, drawwindow, null, DebugColor, 0, Vector2.Zero, SpriteEffects.None, 1f);

                //Draw left
                drawwindow = new Rectangle(BoundingBox.X, BoundingBox.Y, 1, BoundingBox.Height);
                sb.Draw(Assets.Pixel, drawwindow, null, DebugColor, 0, Vector2.Zero, SpriteEffects.None, 1f);

                //Draw right
                drawwindow = new Rectangle(BoundingBox.Right, BoundingBox.Y, 1, BoundingBox.Height);
                sb.Draw(Assets.Pixel, drawwindow, null, DebugColor, 0, Vector2.Zero, SpriteEffects.None, 1f);
            }
        }

        public override void Destroy(IComponent i = null)
        {
            Partners = new List<Entity>();
        }

        virtual public bool TestCollision(Entity e)
        {
            return (BoundingBox.Intersects(e.GetComponent<Collision>().BoundingBox));
        }

        public void AddPartner(Entity e)
        {
            Partners.Add(e);
            e.DestroyEvent += RemovePartner;
        }

        public void RemovePartner(Entity e)
        {
            Partners.Remove(e);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            Debug = xp.GetBool(rootnode + "Debug", false);
            DebugColor = xp.GetColor(rootnode + "DebugColor", Color.PowderBlue);
            Bounds = xp.GetRectangle(rootnode + "Bounds", new Rectangle());
        }
    }
}