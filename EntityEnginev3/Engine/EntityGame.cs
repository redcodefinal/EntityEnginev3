using System;
using System.Collections.Generic;
using System.Linq;
using EntityEnginev3.Data;
using EntityEnginev3.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Engine
{
    public class EntityGame
    {
        public bool Paused { get; protected set; }

        public static Game Game { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public static GameTime GameTime { get; private set; }

        private Rectangle _viewport;

        public EntityState CurrentState;

        public Rectangle Viewport
        {
            get { return _viewport; }
            set { _viewport = value; }
        }

        public List<Service> Services;
        public  Color BackgroundColor = new Color(230,230,255);

        public EntityGame(Game game, GraphicsDeviceManager g, SpriteBatch spriteBatch, Rectangle viewport)
        {
            Game = game;
            SpriteBatch = spriteBatch;
            _viewport = viewport;
            Services = new List<Service> { new InputHandler() };
            Assets.LoadConent(game);

            MakeWindow(g, viewport);
        }

        public virtual void Update(GameTime gt)
        {
            GameTime = gt;

            CurrentState.Update();

            foreach (var service in Services)
            {
                service.Update(GameTime);
            }
        }

        public virtual void Draw()
        {
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp,
                              DepthStencilState.Default, RasterizerState.CullNone);
            Game.GraphicsDevice.Clear(BackgroundColor);

            CurrentState.Draw(SpriteBatch);

            foreach (var service in Services)
            {
                service.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public virtual void Exit()
        {
            CurrentState.Destroy();

            foreach (var service in Services)
            {
                service.Destroy();
            }
        }

        public static void MakeWindow(GraphicsDeviceManager g, Rectangle r)
        {
            if ((r.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) ||
                (r.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)) return;
            g.PreferredBackBufferWidth = r.Width;
            g.PreferredBackBufferHeight = r.Height;
            g.IsFullScreen = false;
            g.ApplyChanges();
        }
    }
}