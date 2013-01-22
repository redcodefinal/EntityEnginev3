using EntityEnginev2.Data;
using EntityEnginev2.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityEnginev2.Input
{
    public sealed class GamePadInput : Input
    {
        private readonly PlayerIndex _pi;
        private Buttons _button;

        public GamePadInput(Entity e, string name, Buttons button, PlayerIndex pi) : base(e, name)
        {
            _button = button;
            _pi = pi;
        }

        public Buttons Button
        {
            get { return _button; }

            set
            {
                _button = value;
                InputHandler.Flush();
            }
        }

        public PlayerIndex PlayerIndex
        {
            get { return _pi; }
        }

        //Read-Only

        public override bool Pressed()
        {
            return InputHandler.ButtonPressed(Button, PlayerIndex);
        }

        public override bool Released()
        {
            return InputHandler.ButtonReleased(Button, PlayerIndex);
        }

        public override bool Down()
        {
            return InputHandler.ButtonDown(Button, PlayerIndex);
        }

        public override bool Up()
        {
            return InputHandler.ButtonUp(Button, PlayerIndex);
        }
    }

    public enum Sticks
    {
        Left,
        Right
    }

    public class GamePadAnalog : Component
    {
        public PlayerIndex PlayerIndex;
        public Sticks Stick;
        public float Threshold;

        public GamePadAnalog(Entity entity, string name)
            : base(entity, name)
        {
        }

        public GamePadAnalog(Entity entity, string name, Sticks stick, PlayerIndex pi)
            : base(entity, name)
        {
            Stick = stick;
            PlayerIndex = pi;
        }

        public Vector2 Position { get; private set; }

        public bool Left
        {
            get { return (Position.X > Threshold); }
        }

        public bool Right
        {
            get { return (Position.X < -Threshold); }
        }

        public bool Up
        {
            get { return (Position.Y > Threshold); }
        }

        public bool Down
        {
            get { return (Position.Y < -Threshold); }
        }

        public override void Update()
        {
            base.Update();
            switch (Stick)
            {
                case Sticks.Left:
                    Position = new Vector2(InputHandler.GamePadStates[(int) PlayerIndex].ThumbSticks.Left.X,
                                           -InputHandler.GamePadStates[0].ThumbSticks.Left.Y);
                    break;
                case Sticks.Right:
                    Position =
                        Position =
                        new Vector2(InputHandler.GamePadStates[(int) PlayerIndex].ThumbSticks.Right.X,
                                    -InputHandler.GamePadStates[0].ThumbSticks.Right.Y);
                    break;
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);

            string s = xp.GetString(path + "->Stick", Stick.ToString());
            if (s == "Left")
            {
                Stick = Sticks.Left;
            }
            else if (s == "Right")
            {
                Stick = Sticks.Right;
            }

            PlayerIndex = (PlayerIndex) xp.GetInt(path + "->PlayerIndex", (int) PlayerIndex);
        }
    }

    public enum Triggers
    {
        Left,
        Right
    }

    public class GamePadTrigger : Input
    {
        public PlayerIndex PlayerIndex;
        public Triggers Trigger;

        public GamePadTrigger(Entity entity, string name) : base(entity, name)
        {
        }

        public GamePadTrigger(Entity entity, string name, Triggers trigger, PlayerIndex pi) : base(entity, name)
        {
            Trigger = trigger;
            PlayerIndex = pi;
        }

        public float Value { get; private set; }

        public override void Update()
        {
            base.Update();
            switch (Trigger)
            {
                case Triggers.Left:
                    Value = InputHandler.GamePadStates[(int) PlayerIndex].Triggers.Left;
                    break;
                case Triggers.Right:
                    Value = InputHandler.GamePadStates[(int) PlayerIndex].Triggers.Right;
                    break;
            }
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);

            string s = xp.GetString(path + "->Trigger", Trigger.ToString());
            if (s == "Left")
            {
                Trigger = Triggers.Left;
            }
            else if (s == "Right")
            {
                Trigger = Triggers.Right;
            }

            PlayerIndex = (PlayerIndex) xp.GetInt(path + "->PlayerIndex", (int) PlayerIndex);
        }
    }
}