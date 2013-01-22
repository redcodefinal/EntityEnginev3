using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityEnginev2.Input
{
    public sealed class DoubleInput : Input
    {
        public KeyboardInput Key;
        public GamePadInput Button;

        public DoubleInput(Entity e, string name, Keys key, Buttons button, PlayerIndex pi) : base(e, name)
        {
            Key = new KeyboardInput(e, name + "key", key);
            Button = new GamePadInput(e, name + "gamepad", button, pi);
        }

        public override bool Pressed()
        {
            return Key.Pressed() || Button.Pressed();
        }

        public override bool Released()
        {
            return Key.Released() || Button.Released();
        }

        public override bool Down()
        {
            return Key.Down() || Button.Down();
        }

        public override bool Up()
        {
            return Key.Up() && Button.Up();
        }
    }
}