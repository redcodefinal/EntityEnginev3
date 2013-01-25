using EntityEnginev3.Components;
using EntityEnginev3.Components.Render;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;

namespace EntityEnginev3.Object
{
    public class Particle : Entity
    {
        public int TimeToLive { get; set; }

        public int MaxTimeToLive { get; private set; }

        public Emitter Emitter;
        public TileRender TileBaseRender;
        public Body Body;
        public Physics Physics;

        public Particle(int index, Vector2 position, int ttl, Emitter e)
            : base(e.Parent.Parent, e.Name + ".Particle")
        {
            Name = Name + Id;

            Body = new Body(this, "Body", position);
            AddComponent(Body);

            TileBaseRender = new TileRender(this, "TileRender", e.Texture, e.TileSize);
            TileBaseRender.Index = index;
            AddComponent(TileBaseRender);

            Physics = new Physics(this, "Physics");
            AddComponent(Physics);

            Emitter = e;
            TimeToLive = ttl;
            MaxTimeToLive = TimeToLive;
        }

        public override void Update()
        {
            base.Update();

            TimeToLive--;
            if (TimeToLive <= 0)
                Destroy();
        }
    }

    public class FadeParticle : Particle
    {
        public int FadeAge;

        public FadeParticle(int index, Vector2 position, int fadeage, int ttl, Emitter e)
            : base(index, position, ttl, e)
        {
            FadeAge = fadeage;
        }

        public override void Update()
        {
            base.Update();
            if (TimeToLive < FadeAge)
            {
                TileBaseRender.Alpha -= 1f / FadeAge;
            }
        }
    }
}