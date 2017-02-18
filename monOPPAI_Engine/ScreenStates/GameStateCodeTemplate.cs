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

//
using monOPPAI_Engine.Content;
using monOPPAI_Engine.ScreenStates;
using monOPPAI_Engine.UserInput;

using PrototypeTextInvaders.Modules;

#endregion

namespace PrototypeTextInvaders.ScreenStates
{
    public class SplashScreen : GameScreens
    {
        #region Fields & Properties Region
        //This is needed for GameScreen State identification in GSM
        const String ScreenName = "CHANGE ME IMMEDIATELY";

        //Place local variables here

        
        #endregion

        #region Constructor Region

        public SplashScreen()
            : base(ScreenName, new Rectangle(0,0,1024,768))
        {
            // do initializations during instancing object
                        
        }

        #endregion

        #region XNA Methods Region

        public override void Initialize()
        {          
            //TODO Initialize code here
            

        }

        public override void LoadContent(ContentManager Content)
        {
            //must give unique CM to base abstract class before loading any resource...
            base.LoadContent(Content);

            //TODO Load Content Resources here
            
        }

        public override void UnloadContent()
        {            
            base.UnloadContent();
            
            //Uncomment line below when already using AudioManager
            //AudioManager.StopAll_Music();            
        }


        public override void Update(GameTime gameTime)
        {
            //TODO Update Code here
                                                  
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            //TODO Draw your objects here
            
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
