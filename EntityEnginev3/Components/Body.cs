using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev3.Components
{
    public class Body : Component
    {
        public float Angle;
        public Vector2 Position;

        public Body(Entity e, string name)
            : base(e, name)
        {
        }

        public Body(Entity e, string name, Vector2 position)
            : base(e, name)
        {
            Position = position;
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            Position = xp.GetVector2(rootnode + "Position", Vector2.Zero);
            Angle = xp.GetFloat(rootnode + "Angle", 0);
        }

        public Body Clone()
        {
            var b = new Body(Parent as Entity, Name);
            b.Position = Position;
            b.Angle = Angle;
            return b;
        }
    }
}