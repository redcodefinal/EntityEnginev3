using EntityEnginev2.Data;
using EntityEnginev2.Engine;

namespace EntityEnginev2.Input
{
    public class Input : Component
    {
        public int HoldTime;
        public double RapidfireMiliseconds;

        public Input(Entity entity, string name) : base(entity, name)
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
        public virtual bool RapidFire()
        {
            if (Pressed())
            {
                if (HoldTime == 0)
                {
                    HoldTime = 1;
                    return true;
                }
            }

            else if (Down())
            {
                if (HoldTime == 0 || HoldTime > RapidfireMiliseconds)
                {
                    HoldTime = 1;
                    return true;
                }
            }

            //else if (Up())
            //{
            //    if (HoldTime != 0 && HoldTime > RapidfireMiliseconds)
            //    {
            //        HoldTime = 0;
            //    }
            //}
            return false;
        }

        public override void Update()
        {
            base.Update();
            if (HoldTime != 0)
            {
                HoldTime += InputHandler.Gametime.ElapsedGameTime.Milliseconds;
                if (HoldTime > RapidfireMiliseconds)
                {
                    HoldTime = 0;
                }
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name;
            RapidfireMiliseconds = xp.GetFloat(rootnode + "->RapidfireMilliseconds", 0);
        }
    }
}