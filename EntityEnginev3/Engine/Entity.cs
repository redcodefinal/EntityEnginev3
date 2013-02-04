using System;
using System.Collections.Generic;
using System.Linq;
using EntityEnginev3.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class Entity : List<IComponent>, IComponent
    {
        public IComponent Parent { get; private set; }

        public EntityState StateRef { get; private set; }

        public delegate void EventHandler(Entity e);

        public event EventHandler DestroyEvent, CreateEvent;

        public event Component.EventHandler ComponentAdded, ComponentRemoved;

        public string Name { get; protected set; }

        public int Id { get; private set; }

        public bool Default { get; set; }

        public bool Active { get; set; }

        public bool Visible { get; set; }

        public Entity(EntityState stateref, IComponent parent, string name)
        {
            StateRef = stateref;
            Parent = parent;
            Name = name;
            Active = true;
            Visible = true;
        }

        public Entity(EntityState stateref, string name)
        {
            StateRef = stateref;
            Parent = stateref;
            Name = name;
            Active = true;
            Visible = true;
        }

        public virtual void Update()
        {
            foreach (var component in ToArray())
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            foreach (var component in ToArray())
            {
                component.Draw(sb);
            }
        }

        public virtual void Destroy(IComponent i = null)
        {
            if (DestroyEvent != null)
                DestroyEvent(this);

            foreach (var component in ToArray())
            {
                component.Destroy();
            }
        }

        public void AddEntity(Entity e)
        {
            if (CreateEvent != null)
                CreateEvent(e);
        }

        public T GetComponent<T>(string name) where T : IComponent
        {
            var result = this.FirstOrDefault(c => c.Name == name);
            if (result == null)
                throw new Exception("Component " + name + " does not exist in " + Name + ".");
            return (T)result;
        }

        public T GetComponent<T>() where T : IComponent
        {
            var result = this.FirstOrDefault(c => c is T && c.Default) ??
                         this.FirstOrDefault(c => c is T);
            if (result == null)
                throw new Exception("Component of type " + typeof(T) + " does not exist in " + Name + ".");
            return (T)result;
        }

        public void AddComponent(Component c)
        {
            if (this.Any(component => c.Name == component.Name))
            {
                throw new Exception(c.Name + " already exists in " + Name + "\'s list!");
            }

            Add(c);

            c.DestroyEvent += RemoveComponent;

            if (ComponentAdded != null)
                ComponentAdded(c);
        }

        public void RemoveComponent(Component c)
        {
            Remove(c);
            if (ComponentRemoved != null)
                ComponentRemoved(c);
        }

        public virtual void ParseXml(XmlParser xp, string path)
        {
        }
    }
}