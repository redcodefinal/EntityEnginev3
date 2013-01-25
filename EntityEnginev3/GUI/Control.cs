using EntityEnginev3.Engine;

namespace EntityEnginev3.GUI
{
    public class Control : Entity
    {
        public bool Enabled;
        public bool Visible;
        public bool HasFocus;

        public Control(IComponent parent, string name) : base(parent, name)
        {
        }
    }
}
