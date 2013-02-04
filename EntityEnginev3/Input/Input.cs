using EntityEnginev3.Data;
using EntityEnginev3.Engine;

namespace EntityEnginev3.Input
{
    public class Input : Component
    {
        private int _holdtime;
        private double _rapidfire;

        public Input(Entity entity, string name)
            : base(entity, name)
        {
        }

        public virtual bool Released()
        {
            return false;
        }

        public virtual bool Pressed()
        {
            return false;
        }

        public virtual bool Down()
        {
            return false;
        }

        public virtual bool Up()
        {
            return false;
        }

        /// <summary>
        /// Will return true if the button is down and a certian amount of time has passed
        /// </summary>
        /// <param name="milliseconds">The milliseconds between firing.</param>
        /// <returns></returns>
        public virtual bool RapidFire(int milliseconds)
        {
            _rapidfire = milliseconds;
            if (Pressed())
            {
                if (_holdtime == 0)
                {
                    _holdtime = 1;
                    return true;
                }
            }

            else if (Down())
            {
                if (_holdtime == 0 || _holdtime > _rapidfire)
                {
                    _holdtime = 1;
                    return true;
                }
            }
            return false;
        }

        public override void Update()
        {
            base.Update();
            if (_holdtime != 0)
            {
                _holdtime += InputHandler.GameTime.ElapsedGameTime.Milliseconds;
                if (_holdtime > _rapidfire)
                {
                    _holdtime = 0;
                }
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name;
            _rapidfire = xp.GetFloat(rootnode + "->RapidfireMilliseconds", 0);
        }
    }
}