#region Namespace Scope Region
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#endregion

namespace PrototypeTextInvaders.Modules
{
    //TODO Code here

   // #region Fields & Properties Region
    public class TextGameObject
    {
        String _tgoletter;
        Vector2 _tgoPosition;
        Rectangle _tgoRect;
        Boolean _isVisible;

        //Getters and Setters
        public String letter
        {
            get { return _tgoletter; }
            set { _tgoletter = value; }
        }

        public Vector2 position
        {
            get { return _tgoPosition; }
            set { _tgoPosition = value; }
        }
        
        public Rectangle rectangle
        {
            get { return _tgoRect; }
            set { _tgoRect = value; }
        }

        public Boolean visible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }
        
        //Constructor

        public TextGameObject()
        {
            _tgoletter = "";
            _tgoPosition = new Vector2(0, 0);
            _tgoRect = new Rectangle((int)_tgoPosition.X, (int)_tgoPosition.Y, 1, 1);
        }

        //MonoGame functions

        public void Initialize()
        {
            //TODO Initialize code here
        }

        public void LoadContent(ContentManager Content)
        {
            //TODO Content Load local to GameObject is here

        }

        public void UnloadContent()
        {
            //TODO Release content when destroying instance of object

        }

        public void Update(GameTime gameTime)
        {
            //TODO Update logic code here

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO Drawing the object code here

        }

    }
}    




