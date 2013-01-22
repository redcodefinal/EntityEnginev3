using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class EntityState : List<Entity>, IComponent
    {
        public IComponent Parent { get; private set; }

        public event Component.EventHandler DestroyEvent;

        public string Name { get; private set; }
        public int Id { get; private set; }
        public bool Default { get; private set; }
        public bool Active { get; private set; }
        public bool Visible { get; private set; }

        public EntityState(IComponent parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public virtual void Start()
        {
            
        }

        public virtual void Show()
        {
            Active = true;
            Visible = true;
        }

        public virtual void Hide()
        {
            Active = false;
            Visible = false;
        }

        public virtual void Update()
        {
            foreach (var entity in ToArray().Where(e => e.Active))
            {
                entity.Update();
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var entity in ToArray().Where(e => e.Visible))
            {
                entity.Draw(sb);
            }
        }

        public virtual void Destroy(IComponent i = null)
        {
            foreach (var entity in ToArray())
            {
                entity.Destroy();
            }
        }
    }
}
