#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace monOPPAI_Engine.UserInput
{
    public class ControlHandler : GameComponent
    {
        #region Fields and Properties Region
        static KeyboardState kbState, prev_kbState;
        static GamePadState gpState, prev_gpState;
        static MouseState mState, prev_mState;
        const PlayerIndex pIndex = PlayerIndex.One;

        public static Boolean DebugFlag;

        public static KeyboardState KBState { get { return kbState; } }
        public static GamePadState GPState { get { return gpState; } }
        public static MouseState MSState { get { return mState; } }
        #endregion

        #region Constructor Region
        public ControlHandler(Game game)
            : base(game)
        {
            kbState = Keyboard.GetState();
            gpState = GamePad.GetState(pIndex);
            mState = Mouse.GetState();
            DebugFlag = false;
        }
        #endregion

        #region XNA Methods Region
        public override void Initialize() { base.Initialize(); }

        public override void Update(GameTime gameTime)
        {
            FlushInputBuffer();
            base.Update(gameTime);
        }
        #endregion

        #region ControlHandler General Methods Region
        public static void FlushInputBuffer()
        {
            prev_gpState = gpState;
            prev_kbState = kbState;
            prev_mState = mState;
            gpState = GamePad.GetState(pIndex);
            kbState = Keyboard.GetState();
            mState = Mouse.GetState();            
        }
        #endregion

        #region ControlHandler KeyBoard Methods Region

        public static bool KeyPressed(Keys key)
        {
            Boolean KeyPressedState = prev_kbState.IsKeyUp(key) && kbState.IsKeyDown(key);
            if (KeyPressedState && DebugFlag)
                Console.WriteLine("[ControlHandler_KB]Pressed Key: " + key.ToString());            
            return KeyPressedState;
        }
        public static bool KeyReleased(Keys key)
        {
            Boolean KeyReleasedState = prev_kbState.IsKeyDown(key) && kbState.IsKeyUp(key);
            if (KeyReleasedState && DebugFlag)
                Console.WriteLine("[ControlHandler_KB]Released Key: " + key.ToString());                        
            return KeyReleasedState;
        }
        public static bool KeyDown(Keys key)
        {
            if (kbState.IsKeyDown(key) && DebugFlag)
                Console.WriteLine("[ControlHandler_KB]Down Pressed Key: " + key.ToString());
            return kbState.IsKeyDown(key);
        }
        #endregion

        #region ControlHandler GamePad Methods Region
        public static bool ButtonPressed(Buttons button) { return prev_gpState.IsButtonUp(button) && gpState.IsButtonDown(button); }
        public static bool ButtonReleased(Buttons button) { return prev_gpState.IsButtonDown(button) && gpState.IsButtonUp(button); }
        public static bool ButtonDown(Buttons button) { return gpState.IsButtonDown(button); }
        #endregion

        #region ControlHandler Mouse Methods Region
        public static bool MouseLeftButtonPressed() { return mState.LeftButton == ButtonState.Pressed && prev_mState.LeftButton == ButtonState.Released; }
        public static bool MouseLeftButtonDown() { return mState.LeftButton == ButtonState.Pressed; }
        public static bool MouseRightButtonPressed() { return mState.RightButton == ButtonState.Pressed && prev_mState.RightButton == ButtonState.Released; }
        public static Vector2 MouseCursorPosition() { return new Vector2(mState.X, mState.Y); }
        #endregion
    }
}
