using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Render
{
    public class TileRender : ImageRender
    {
        public Vector2 TileSize { get; private set; }

        public int Index;

        public int Columns
        {
            get
            {
                return (int)(Texture.Width / TileSize.X);
            }
        }

        public int Rows { get { return (int)(Texture.Height / TileSize.Y); } }

        public override Rectangle DrawRect
        {
            get
            {
                Vector2 position = Parent.GetComponent<Body>().Position;
                return new Rectangle(
                    (int)(position.X + Origin.X * Scale.X),
                    (int)(position.Y + Origin.Y * Scale.Y),
                    (int)(TileSize.X * Scale.X),
                    (int)(TileSize.Y * Scale.Y));
            }
        }

        public Rectangle SourceRectangle
        {
            get
            {
                var r = new Rectangle();
                for (var i = 0; i <= Index; i += Columns)
                {
                    var ypos = Index - i;

                    if (ypos >= Columns) continue;

                    var p = new Point { Y = (i / Columns) * (int)TileSize.Y, X = ypos * (int)TileSize.X };
                    r = new Rectangle(p.X, p.Y, (int)TileSize.X, (int)TileSize.Y);
                }
                return r;
            }
        }

        public TileRender(Entity e, string name)
            : base(e, name)
        {
        }

        public TileRender(Entity e, string name, Texture2D texture, Vector2 tilesize)
            : base(e, name, texture)
        {
            TileSize = tilesize;
            Origin = new Vector2(TileSize.X / 2.0f, TileSize.Y / 2.0f);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, SourceRectangle, Color * Alpha, Parent.GetComponent<Body>().Angle, Origin, Flip, Layer);
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            TileSize = xp.GetVector2(rootnode + "TileSize", Vector2.Zero);
            Index = xp.GetInt(rootnode + "Index", 0);
        }
    }
}