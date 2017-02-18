#region Namespace Scope Region
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using monOPPAI_Engine.UserInput;

#endregion

namespace PrototypeTextInvaders.Modules
{
    //TODO Code here

   // #region Fields & Properties Region
    public class LetterObject
    {
        Char _tgoletter;
        Vector2 _tgoPosition;
        Rectangle _tgoRect;
        Boolean _isVisible;
        SpriteFont sf;

        //Getters and Setters
        public Char letter
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
        
        //Constructors

        public LetterObject()
        {
            _tgoletter = 'X';
            _tgoPosition = new Vector2(0, 0);
            _tgoRect = new Rectangle((int)_tgoPosition.X, (int)_tgoPosition.Y, 1, 1);
            _isVisible = true;
            Initialize();
        }
        
        public LetterObject(Char letter, Vector2 position, 
            Rectangle rectangle, Boolean visible)
        {
            _tgoletter = letter;
            _tgoPosition = position;
            _tgoRect = rectangle;
            _isVisible = visible;
            Initialize();
        }

        //MonoGame functions

        public void Initialize()
        {
            //TODO Initialize code here
            

        }

        public void LoadContent(ContentManager Content)
        {
            //TODO Content Load local to GameObject is here

            sf = Content.Load<SpriteFont>("Text");

            //Have the object rectangle be the same size as the letter.
            _tgoRect.Width = (int)StringMethods.GetStringWidth(sf, _tgoletter.ToString());
            _tgoRect.Height = (int)StringMethods.GetStringHeight(sf, _tgoletter.ToString());

        }

        public void UnloadContent()
        {
            //TODO Release content when destroying instance of object
            
        }

        public void Update(GameTime gameTime)
        {
            //TODO Update logic code here

            //update _tgoPosition to be the same as the _tgoRect
            _tgoPosition.X = _tgoRect.X;
            _tgoPosition.Y = _tgoRect.Y;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO Drawing the object code here
            //spriteBatch.Begin();

            spriteBatch.DrawString(sf, _tgoletter.ToString(), _tgoPosition, Color.Red);

            //spriteBatch.End();

        }

    }
}    




