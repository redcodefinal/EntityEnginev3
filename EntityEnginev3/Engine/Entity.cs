using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class Entity : List<IComponent>, IComponent
    {
        public IComponent Parent { get; private set; }

        public event Component.EventHandler DestroyEvent, ComponentAdded, ComponentRemoved;

        public string Name { get; private set; }
        public int Id { get; private set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }

        public Entity(IComponent parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public void Update()
        {
            foreach (var component in ToArray())
            {
                component.Update();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (var component in ToArray())
            {
                component.Draw(sb);
            }
        }

        public void Destroy(IComponent i = null)
        {
            if (DestroyEvent != null)
                DestroyEvent(i);

            foreach (var component in ToArray())
            {
                component.Destroy();
            }
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

        public void AddComponent(IComponent c)
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

        public void RemoveComponent(IComponent c)
        {
            Remove(c);
            if (ComponentRemoved != null)
                ComponentRemoved(c);
        }
    }
}
