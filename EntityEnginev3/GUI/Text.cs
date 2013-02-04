using EntityEnginev3.Components;
using EntityEnginev3.Components.Render;
using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.GUI
{
    public class Text : Entity
    {
        public Body Body;
        public Physics Physics;
        public TextRender TextRender;

        public Text(EntityState stateref, IComponent parent, string name)
            : base(stateref, parent, name)
        {
            Body = new Body(this, "Body");
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            TextRender = new TextRender(this, "TextRender");
            AddComponent(TextRender);
        }

        public Text(EntityState stateref, IComponent parent, string name, SpriteFont font, string text, Vector2 position)
            : base(stateref,parent, name)
        {
            Body = new Body(this, "Body", position);
            AddComponent(Body);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            TextRender = new TextRender(this, "TextRender", font, text);
            AddComponent(TextRender);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
        }
    }
}