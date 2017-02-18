#region Namespace Scope Region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#endregion

//using PhalANX_Engine.GameScreen;
//using PhalANX_Engine.Audio;
//using PhalANX_Engine.Control;
//using PhalANX_Engine.Fonts;
//using PhalANX_Engine.Text;
//using PhalANX_Engine.Sprites;
using monOPPAI_Engine.Content;
using monOPPAI_Engine.ScreenStates;
using monOPPAI_Engine.UserInput;

using PrototypeTextInvaders.Modules;


namespace PrototypeTextInvaders.ScreenStates
{
    public class SplashScreen : GameScreens
    {
        #region Fields & Properties Region

        //This is needed for GameScreen State identification in GSM
        const String ScreenName = "SplashScreen";

        private SpriteFont font;
        //RNGenerator Foo = new RNGenerator();
        private int text = RNGenerator.GiveRandom();

        #endregion

        #region Constructor Region

        public SplashScreen()
            : base(ScreenName, new Rectangle(0,0,1024,768))
        {
            // do any pre initialization code here
            
            
        }

        #endregion

        #region XNA Methods Region

        public override void Initialize()
        {
          
            //TODO Initialize code here
            

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content); //must give unique CM to base abstract class before loading any resource...

            //TODO Load Content Resources here
            font = Content.Load<SpriteFont>("Text");

        }

        public override void UnloadContent()
        {            
            base.UnloadContent();

            
            AudioManager.StopAll_Music();            
        }


        public override void Update(GameTime gameTime)
        {
            /*
            if (!go4BGM)
            {
                AudioManager.Play_BGMusic("door-heartbeat");                
                go4BGM = true;
            }
            */
            text = RNGenerator.GiveRandom();
            
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //TODO Draw your objects here
            spriteBatch.DrawString(font, text.ToString(), new Vector2(100, 100), Color.Red);

            spriteBatch.End();
        }

        #endregion

        #region Local Methods Region

        public Vector2 getOrigin(Rectangle rect)
        {
            return new Vector2(rect.Width / 2, rect.Height / 2);
        }
 
        #endregion
    }
}
