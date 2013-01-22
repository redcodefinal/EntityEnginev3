using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class Component : IComponent
    {
        public IComponent Parent { get; private set; }

        public delegate void EventHandler(IComponent i);
        public event EventHandler DestroyEvent;

        public string Name { get; private set; }
        public int Id { get; private set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }

        public Component(Entity parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual void Destroy(IComponent i = null)
        {
        }
    }
}
