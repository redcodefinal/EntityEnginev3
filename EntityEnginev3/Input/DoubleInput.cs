﻿using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityEnginev3.Input
{
    public sealed class DoubleInput : Input
    {
        public KeyboardInput Key;
        public GamePadInput Button;

        public DoubleInput(Entity parent, string name, Keys key, Buttons button, PlayerIndex pi)
            : base(parent, name)
        {
            Key = new KeyboardInput(parent, name + "key", key);
            Button = new GamePadInput(parent, name + "gamepad", button, pi);
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