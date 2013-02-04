using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using EntityEnginev3.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components
{
    public class Emitter : Component
    {
        public Texture2D Texture { get; protected set; }

        public Vector2 TileSize { get; protected set; }

        public bool AutoEmit;
        public int EmitAmount = 1;

        public Emitter(Entity entity, string name)
            : base(entity, name)
        {
        }

        public Emitter(Entity e, string name, Texture2D texture, Vector2 tilesize)
            : base(e, name)
        {
            Texture = texture;
            TileSize = tilesize;
        }

        public override void Update()
        {
            if (AutoEmit)
                Emit(EmitAmount);
        }

        protected virtual Particle GenerateNewParticle()
        {
            var p = new Particle(Parent.StateRef, Parent.StateRef, 0, Parent.GetComponent<Body>().Position / 2, 30, this) { Physics = { Velocity = Vector2.UnitY } };
            return p;
        }

        public virtual void Emit(int amount)
        {
            for (var i = 0; i < amount; i++)
                Parent.AddEntity(GenerateNewParticle());
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name;
            Texture = EntityGame.Game.Content.Load<Texture2D>(
                xp.GetString(rootnode + "->Texture", "TextureNotSet"));
            TileSize = xp.GetVector2(rootnode + "->TileSize", Vector2.Zero);
        }
    }
}