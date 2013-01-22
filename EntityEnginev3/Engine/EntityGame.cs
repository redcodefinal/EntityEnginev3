using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class EntityGame : List<EntityState>
    {
        public bool Paused { get; protected set; }
        public Game Game { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public GameTime GameTime { get; private set; }

        private Rectangle _viewport;
        public Rectangle Viewport
        {
            get { return _viewport; }
            set { _viewport = value; }
        }

        public List<Service> Services; 

        public virtual void Update()
        {
            foreach (var es in ToArray().Where(es => es.Active))
            {
                es.Update();
            }

            foreach (var service in Services)
            {
                service.Update();
            }
        }

        public virtual void Draw()
        {
            foreach (var es in ToArray().Where(es => es.Visible))
            {
                es.Draw(SpriteBatch);
            }

            foreach (var service in Services)
            {
                service.Draw(SpriteBatch);
            }
        }

        public virtual void Exit()
        {
            foreach (var es in ToArray())
            {
                es.Destroy();
            }
            foreach (var service in Services)
            {
                service.Destroy();
            }
        }

        public T GetComponent<T>() where T : EntityState
        {
            var result = this.FirstOrDefault(c => c.GetType() == typeof(T));
            if (result == null)
                throw new Exception("Component of type " + typeof(T) + " does not exist in" + this + ".");
            return (T)result;
        }
        public static void MakeWindow(GraphicsDeviceManager g, Rectangle r)
        {
            if ((r.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) ||
                (r.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)) return;
            g.PreferredBackBufferWidth = r.Width;
            g.PreferredBackBufferHeight = r.Height;
            g.IsFullScreen = false;
            g.ApplyChanges();
        }
    }
}
