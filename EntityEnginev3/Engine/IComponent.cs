using EntityEnginev3.Data;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public interface IComponent
    {
        string Name { get; }

        int Id { get; }

        bool Default { get; }

        bool Active { get; }

        bool Visible { get; }

        void Update();

        void Draw(SpriteBatch sb);

        void Destroy(IComponent i = null);

        void ParseXml(XmlParser xp, string path);
    }
}