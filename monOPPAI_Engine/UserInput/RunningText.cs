#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace monOPPAI_Engine.UserInput
{
    /// <summary>
    /// This class will display a string
    /// one character at a time from the left to the right
    /// and stops when it already displayed all of the 
    /// characters in the given string
    /// </summary>
    public class RunningText
    {
        #region Fields and Properties Region

        String _toBeDisplayedText;
        String _displayedText;
        Vector2 _textPosition;
        SpriteFont _textFont;
        Color _textColor;

        Boolean _readyNextLetter;
        Boolean _RunOnOrigin;

        int _textLength;
        float _timeValue;
        float _displayDelay;

        public Color Colour
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Instantiates an instance of RunningText
        /// </summary>
        /// <param name="Text">Text to be displayed</param>
        /// <param name="Position">Position of Text on screen</param>
        /// <param name="Font">Font to be used for Text</param>
        /// <param name="Colour">Color of the Text to be displayed on Screen</param>
        public RunningText(String Text, Vector2 Position, SpriteFont Font, Boolean RunOnOrigin, float DisplayDelay,Color Colour)
        {
            _toBeDisplayedText = Text;
            _textPosition = Position;
            _textFont = Font;
            _textColor = Colour;
            _displayedText = String.Empty;

            _textLength = Text.Length;

            _displayDelay = DisplayDelay;
            _timeValue = 0f;
            _readyNextLetter = false;

            _RunOnOrigin = RunOnOrigin;
        }

        #endregion

        #region XNA Methods Region

        public void Initialize() 
        {
            _displayedText = String.Empty;
        }

        public void Initialize(String Text)
        {
            _toBeDisplayedText = Text;
            _displayedText = String.Empty;
        }

        public void Update(GameTime gameTime)
        {
            if (_displayedText.Length != _toBeDisplayedText.Length)
            {
                if (_readyNextLetter)
                    _readyNextLetter = !_readyNextLetter;

                if (_timeValue <= _displayDelay)
                    _timeValue += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                else
                {
                    _readyNextLetter = !_readyNextLetter;
                    _timeValue = 0f;
                }                
                if (_readyNextLetter)
                {
                    var idx = (int)MathHelper.Clamp((float)_displayedText.Length, 0,
                        (float)_toBeDisplayedText.Length - 1);
                    _displayedText += _toBeDisplayedText[idx];
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_RunOnOrigin)            
                spriteBatch.DrawString(_textFont, _displayedText, _textPosition, _textColor, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, 0f);            
            else
                spriteBatch.DrawString(_textFont, _displayedText, _textPosition, _textColor, 0f,
                        new Vector2(_textFont.MeasureString(_displayedText).X / 2, _textFont.MeasureString(_displayedText).Y / 2),
                        1f, SpriteEffects.None, 0f);
        }

        #endregion

        #region Local Methods

        /// <summary>
        /// Returns a Boolean value indicating if the text to be displayed is already
        /// completely displayed on screen.
        /// </summary>
        /// <returns>Boolean value</returns>
        public Boolean allTextDisplayed()
        {
            return _displayedText.Length == _toBeDisplayedText.Length;
        }

        #endregion

    }
}
