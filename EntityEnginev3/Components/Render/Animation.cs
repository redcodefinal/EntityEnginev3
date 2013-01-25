using EntityEnginev3.Data;
using EntityEnginev3.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Components.Render
{
    public class Animation : ImageRender
    {
        public Vector2 TileSize;
        public int FramesPerSecond;

        public int CurrentFrame { get; set; }

        public event Timer.TimerEvent LastFrameEvent;

        public Timer FrameTimer;

        public bool HitLastFrame
        {
            get { return (CurrentFrame >= Tiles - 1); }
        }

        public int Tiles
        {
            get { return Texture.Width / (int)TileSize.X; }
        }

        public int MillisecondsPerFrame
        {
            get { return 1000 / FramesPerSecond; }
        }

        public Rectangle CurrentFrameRect
        {
            get
            {
                return new Rectangle((int)(TileSize.X * CurrentFrame), 0, (int)TileSize.X, (int)TileSize.Y);
            }
        }

        public override Rectangle DrawRect
        {
            get
            {
                Vector2 position = Parent.GetComponent<Body>().Position;
                return new Rectangle(
                    (int)(position.X + Origin.X * Scale.X),
                    (int)(position.Y + Origin.Y * Scale.Y),
                    (int)(TileSize.X * Scale.X),
                    (int)(TileSize.Y * Scale.Y));
            }
        }

        public Animation(Entity e, string name, Texture2D texture, Vector2 tileSize, int framesPerSecond)
            : base(e, name, texture)
        {
            TileSize = tileSize;
            FramesPerSecond = framesPerSecond;

            Origin = new Vector2(TileSize.X / 2.0f, TileSize.Y / 2.0f);

            FrameTimer = new Timer(e, Name + ".FrameTimer") { Milliseconds = MillisecondsPerFrame };
            FrameTimer.LastEvent += AdvanceNextFrame;
        }

        public Animation(Entity e, string name)
            : base(e, name)
        {
            FrameTimer = new Timer(e, "FrameTimer");
            FrameTimer.LastEvent += AdvanceNextFrame;
        }

        public override void Update()
        {
            FrameTimer.Update();
            if (HitLastFrame)
            {
                if (LastFrameEvent != null)
                    LastFrameEvent();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, DrawRect, CurrentFrameRect, Color * Alpha, Parent.GetComponent<Body>().Angle,
                    Origin, Flip, Layer);
        }

        public void AdvanceNextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame >= Tiles)
                CurrentFrame = 0;
        }

        public void AdvanceLastFrame()
        {
            CurrentFrame--;
            if (CurrentFrame < 0)
                CurrentFrame = Tiles;
        }

        public void Start()
        {
            FrameTimer.Start();
        }

        public void Stop()
        {
            FrameTimer.Stop();
        }

        public override void ParseXml(XmlParser xp, string path)
        {
            base.ParseXml(xp, path);
            string rootnode = path + "->" + Name + "->";
            FrameTimer.ParseXml(xp, path + "->" + Name);
            TileSize = xp.GetVector2(rootnode + "TileSize", Vector2.Zero);
            FramesPerSecond = xp.GetInt(rootnode + "FramesPerSecond", 0);
            CurrentFrame = xp.GetInt(rootnode + "CurrentFrame", 0);
            if (xp.GetBool(rootnode + "StartAfterCreation", false))
                Start();
        }
    }
}