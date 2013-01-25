using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Render
{
    public class TextRender : BaseRender
    {
        public string Text;
        public SpriteFont Font;

        public override Rectangle DrawRect
        {
            get
            {
                Vector2 position;
                position = Parent.GetComponent<Body>().Position;
                return new Rectangle((int)position.X, (int)position.Y, (int)(Font.MeasureString(Text).X * Scale.X), (int)(Font.MeasureString(Text).Y * Scale.Y));
            }
        }

        public TextRender(Entity entity, string name)
            : base(entity, name)
        {
        }

        public TextRender(Entity entity, string name, SpriteFont font, string text)
            : base(entity, name)
        {
            Text = text;
            Font = font;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, Text, Parent.GetComponent<Body>().Position, Color * Alpha,
                          Parent.GetComponent<Body>().Angle, Origin, Scale, Flip, Layer);
        }

        public void LoadFont(string location)
        {
            Font = EntityGame.Game.Content.Load<SpriteFont>(location);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";

            LoadFont(xp.GetString(rootnode + "Font", "FontNotSet"));
            Text = xp.GetString(rootnode + "Text", "");
        }
    }
}