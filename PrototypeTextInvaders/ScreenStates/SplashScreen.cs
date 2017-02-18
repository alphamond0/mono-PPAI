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


namespace PrototypeTextInvaders.ScreenStates
{
    public class SplashScreen : GameScreens
    {
        #region Fields & Properties Region

        const String ScreenName = "SplashScreen";      

        //AnimatedSprite loading;
        RunningText PromptUser;

        Texture2D Girl404;
        //Texture2D wPixl;
        List<Texture2D> RandomSplashes;
        int randomIntSplash;


        //Texture2D splashScreenTextureSheet1;

        //Flags
        Boolean go4BGM;
        //Flags

        
        //For faux pause:
        float TimeVar, TimeDelay;
        Boolean FlagForFaux;


        //float _textureOpacity;
        //float _textureOpacityIncValue;

        String prompt1 = "Press Space To Continue...";
        String powered = "Powered By:";

        

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

        }

        public override void UnloadContent()
        {            
            base.UnloadContent();

            
            PromptUser = null;
            AudioManager.StopAll_Music();            
        }

        int pauseFlag = -1;

        public override void Update(GameTime gameTime)
        {            
            /*
            if (!go4BGM)
            {
                AudioManager.Play_BGMusic("door-heartbeat");                
                go4BGM = true;
            }
            */                       
            
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            PromptUser.Draw(spriteBatch);
            
           

            spriteBatch.DrawString(FontDictionary.getFont("[acknowTT20]"), powered, new Vector2(278,140), Color.White);

            
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
