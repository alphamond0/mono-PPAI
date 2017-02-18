#region Namespace Scope Region
using System;
using System.Diagnostics;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion


namespace monOPPAI_Engine.ScreenStates
{
    public class GameScreens
    {
        #region Fields & Properties Region

        protected ContentManager _Content;
        protected String _GameScreenName;
        protected static Rectangle _ScreenBounds;

        protected Boolean _isVisible;
        protected Boolean _isActive;

        protected static Game _GameRef;

        public String GameScreenName
        {
            get { return _GameScreenName; }            
        }
        protected Boolean IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }
        protected Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public static void ReInitScreenBounds(int ScreenWidth, int ScreenHeight)
        {
            _ScreenBounds.Width = ScreenWidth;
            _ScreenBounds.Height = ScreenHeight;
        }

        #endregion

        #region Constructor Region

        public GameScreens(String ScreenName)
        {
            _GameScreenName = ScreenName;
            _isActive = _isVisible = true;            
        }

        public GameScreens(String ScreenName, Rectangle ScreenBounds)
        {
            _GameScreenName = ScreenName;
            _isActive = _isVisible = true;
            _ScreenBounds = ScreenBounds;
        }

        public GameScreens(Game game)
        {
            _GameRef = game;
        }

        #endregion

        #region XNA Methods Region

        public virtual void Initialize() { }

        public virtual void LoadContent(ContentManager Content)
        {
            _Content = new ContentManager(Content.ServiceProvider, "Content");            
        }

        public virtual void UnloadContent()
        {
            _Content.Unload();
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        #endregion

        #region Local Methods Region

        

        #endregion
    }
}
