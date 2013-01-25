using EntityEnginev3.Data;
using EntityEnginev3.Engine;

namespace EntityEnginev3.Components
{
    public class Health : Component
    {
        public Health(Entity e, string name)
            : base(e, name)
        {
        }

        public Health(Entity e, string name, int hp)
            : base(e, name)
        {
            HitPoints = hp;
        }

        public float HitPoints { get; set; }

        public bool Alive
        {
            get { return !(HitPoints <= 0); }
        }

        public event Entity.EventHandler HurtEvent;

        public event Entity.EventHandler DiedEvent;

        public void Hurt(float points)
        {
            if (!Alive) return;

            HitPoints -= points;
            if (HurtEvent != null)
                HurtEvent(Parent);

            if (!Alive)
            {
                if (DiedEvent != null)
                    DiedEvent(Parent);
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            HitPoints = xp.GetInt(rootnode + "HitPoints", 1);
        }
    }
}