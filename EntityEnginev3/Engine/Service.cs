using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public abstract class Service
    {
        public bool Active;
        public bool Visible;

        public delegate void EventHandler(Service s);
        public event EventHandler DestroyEvent;

        public abstract void Update();
        public abstract void Draw(SpriteBatch sb);
        public virtual void Destroy()
        {
            if (DestroyEvent != null)
                DestroyEvent(this);
        }
    }
}
