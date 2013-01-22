﻿using EntityEnginev2.Engine;
using Microsoft.Xna.Framework.Input;

namespace EntityEnginev2.Input
{
    public sealed class KeyboardInput : EntityEnginev2.Input.Input
    {
        private Keys _key;

        public Keys Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
                InputHandler.Flush();
            }
        }

        public KeyboardInput(Entity e, string name, Keys key)
            : base(e, name)
        {
            _key = key;
        }

        public override bool Pressed()
        {
            return InputHandler.KeyPressed(Key);
        }

        public override bool Released()
        {
            return InputHandler.KeyReleased(Key);
        }

        public override bool Down()
        {
            return InputHandler.KeyDown(Key);
        }

        public override bool Up()
        {
            return InputHandler.KeyUp(Key);
        }
    }
}