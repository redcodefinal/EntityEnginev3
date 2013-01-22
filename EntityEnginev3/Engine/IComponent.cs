using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public interface IComponent
    {
        IComponent Parent { get; }

        event Component.EventHandler DestroyEvent;

        string Name { get; }
        int Id { get; }
        bool Default { get; }
        bool Active { get; }
        bool Visible { get; }

        void Update();
        void Draw(SpriteBatch sb);
        void Destroy(IComponent i = null);
    }
}
