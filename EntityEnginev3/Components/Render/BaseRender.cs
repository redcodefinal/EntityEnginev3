using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Render
{
    public class BaseRender : Component
    {
        public float Alpha = 1f;
        public Color Color = Color.White;
        public SpriteEffects Flip = SpriteEffects.None;
        public float Layer;
        public Vector2 Scale = Vector2.One;

        public BaseRender(Entity entity, string name)
            : base(entity, name)
        {
        }

        public Vector2 Origin { get; set; }

        public virtual Rectangle DrawRect { get; set; }

        public virtual Rectangle SourceRect { get; set; }

        //public virtual Vector2 Bounds { get; set; }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);

            string rootnode = path + "->" + Name;
            Color = xp.GetColor(rootnode + "->Color", Color);
            Alpha = xp.GetFloat(rootnode + "->Alpha", Alpha);
            Scale = xp.GetVector2(rootnode + "->Scale", Scale);
            Layer = xp.GetFloat(rootnode + "->Layer", Layer);

            if (xp.CheckElement(rootnode + "->Flip"))
            {
                string s = xp.GetString(rootnode + "->Flip", "None");
                switch (s)
                {
                    case "None":
                        Flip = SpriteEffects.None;
                        break;

                    case "FlipH":
                        Flip = SpriteEffects.FlipHorizontally;
                        break;

                    case "FlipV":
                        Flip = SpriteEffects.FlipVertically;
                        break;
                }
            }
        }

        public virtual BaseRender Clone()
        {
            var r = new BaseRender(Parent, Name);
            r.Color = Color;
            r.Alpha = Alpha;
            r.Scale = Scale;
            r.Layer = Layer;
            r.Flip = Flip;
            r.Origin = Origin;
            r.DrawRect = DrawRect;
            return r;
        }
    }
}