using EntityEnginev3.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class Component : IComponent
    {
        public Entity Parent { get; private set; }

        public delegate void EventHandler(Component i);

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
            if (DestroyEvent != null)
                DestroyEvent(this);
        }

        public virtual void ParseXml(XmlParser xp, string path)
        {
            string rootnode = path + "->" + Name;
            Active = xp.GetBool(rootnode + "->Active", Active);
            Default = xp.GetBool(rootnode + "->Default", Default);
        }
    }
}