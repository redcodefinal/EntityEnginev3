using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Render
{
    public class ImageRender : BaseRender
    {
        public Texture2D Texture { get; set; }

        public override Rectangle DrawRect
        {
            get
            {
                Vector2 position = Parent.GetComponent<Body>().Position;
                return new Rectangle(
                    (int)((int)position.X + Origin.X * Scale.X),
                    (int)((int)position.Y + Origin.Y * Scale.Y),
                    (int)(Texture.Width * Scale.X),
                    (int)(Texture.Height * Scale.Y));
            }
        }

        public override Rectangle SourceRect
        {
            get { return new Rectangle(0, 0, Texture.Width, Texture.Height); }
        }

        public ImageRender(Entity e, string name)
            : base(e, name)
        {
            Origin = Vector2.Zero;
        }

        public ImageRender(Entity e, string name, Texture2D texture)
            : base(e, name)
        {
            Texture = texture;
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, null, Color * Alpha, Parent.GetComponent<Body>().Angle,
                    Origin, Flip, Layer);
        }

        public void LoadTexture(string location)
        {
            Texture = EntityGame.Game.Content.Load<Texture2D>(location);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name;

            if (xp.CheckElement(rootnode + "->Texture"))
            {
                LoadTexture(xp.GetString(rootnode + "->Texture", "TEXTURENOTSET"));
            }
        }
    }
}