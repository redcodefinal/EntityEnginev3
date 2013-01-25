using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityEnginev3.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class EntityState : List<Entity>, IComponent
    {
        public EntityGame Parent { get; private set; }
        public event Entity.EventHandler EntityAdded , EntityRemoved;

        public string Name { get; private set; }
        public int Id { get; private set; }
        public bool Default { get; private set; }
        public bool Active { get; private set; }
        public bool Visible { get; private set; }
        public int LastId { get; private set; }

        public EntityState(EntityGame eg, string name)
        {
            Parent = eg;
            Name = name;
        }

        public virtual void Start()
        {
            
        }

        public virtual void Show()
        {
            Parent.CurrentState = this;
        }

        public virtual void Hide()
        {
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

        public virtual void AddEntity(Entity e)
        {
            Add(e);
            e.DestroyEvent += RemoveEntity;
            e.CreateEvent += AddEntity;
            if (EntityAdded != null)
                EntityAdded(e);
            
        }

        public virtual void RemoveEntity(Entity e)
        {
            Remove(e);
            if (EntityRemoved != null)
                EntityRemoved(e);
        }

        public int GetId()
        {
            return LastId++;
        }

        public virtual void ParseXml(XmlParser xp, string path)
        {
        }
    }
}
