#region Namespace Scope Region
using System;
using System.Collections.Generic;
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
    public class GameScreenManager : DrawableGameComponent
    {
        #region Fields and Properties

        Game gameRef;
        Stack<GameScreens> _screenStack;
        static GameScreenManager _instance;

        GameScreens _newScreen;
        GameScreens _currentScreen;

        public static Boolean DebugFlag = false;

        public static Boolean Delaying = false;
        public float TimeVal, TimeDelay = 1500f;
        

        // Singleton POWAH!
        public static GameScreenManager Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Constructor Region

        public GameScreenManager(Game game, GameScreens initialScreen)
            : base(game)
        {
            _screenStack = new Stack<GameScreens>();
            _currentScreen = initialScreen;            
            gameRef = game;
            TimeVal = 0f;            
            _instance = this;
        }

        #endregion

        #region XNA Methods Region

        public override void Initialize() // recommended initial Screen is splash screen...
        {
            if(DebugFlag)
                Console.WriteLine("[GameScreenManager] Instantiated!: ");

            PushGameScreen(_currentScreen);
        }

        protected override void LoadContent()
        {
            if(DebugFlag)
                Console.WriteLine("[GameScreenManager] LoadContent: " + _currentScreen.GameScreenName);

            _screenStack.Peek().LoadContent(gameRef.Content);            
        }
        
        public override void Update(GameTime gameTime)
        {   
            _screenStack.Peek().Update(gameTime);            
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            _screenStack.Peek().Draw(spriteBatch);
        }

        #endregion

        #region Local Methods Region

        public void Delay(float DelayTime)
        {
            TimeDelay = DelayTime;
            Delaying = true;
        }

        public void PopGameScreen()
        {
            if (_instance == null || _screenStack.Count == 1)
            {
                Console.WriteLine("[GameScreenManager]Unable to Deactivate " + _instance.ScreenName());             
                return;
            }
            else
            {
                if (DebugFlag)
                    Console.WriteLine("[GameScreenManager]Deactivating Screen: " + _currentScreen.GameScreenName);

                _screenStack.Pop().UnloadContent();
                if(_screenStack.Peek().GameScreenName == "LoadingScreen")
                    _screenStack.Pop().UnloadContent();
                _currentScreen = _instance._screenStack.Peek();
                _currentScreen.Initialize();
                _currentScreen.LoadContent(gameRef.Content);

                if (DebugFlag)
                    Console.WriteLine("[GameScreenManager]Activating Screen: " + _currentScreen.GameScreenName);
            }            
        }

        public void PushGameScreen(GameScreens gameScreen)
        {
            if (_instance == null)
            {
                Console.WriteLine("[GameScreenManager]Manager is not instantiated!");
                return;
            }
            else
            {
                Debug.Assert(!_screenStack.Contains(gameScreen) || gameScreen.GameScreenName != "LoadingScreen", "GameScreen already in the stack... TRY AGAIN!");
                
                if (_screenStack.Count != 0)
                {
                    

                    _newScreen = gameScreen;
                    _screenStack.Push(Instance._newScreen);
                    _currentScreen.UnloadContent();
                    _newScreen.Initialize();
                    _newScreen.LoadContent(gameRef.Content);
                    _currentScreen = Instance._newScreen;

                }
                else
                {
                    _screenStack.Push(gameScreen);
                    _screenStack.Peek().Initialize();
                    _screenStack.Peek().LoadContent(gameRef.Content);
                }
                if(DebugFlag)
                    Console.WriteLine("[GameScreenManager]Active Screen: " + _currentScreen.GameScreenName);
            }            
        }

        public int ScreenStackCount()
        {
            if (_instance == null)
                return -1;

            return _instance._screenStack.Count;
        }

        public String ScreenName()
        {
            return _screenStack.Peek().GameScreenName;
        }

        public static void RefreshCurrentScreen()
        {
            _instance._screenStack.Peek().Initialize();
        }

        public void ReinitializeBackToSplash(GameScreens Splash)
        {
            while (_instance._screenStack.Count != 0)
            {
                _instance._screenStack.Pop();
            }
            _instance.PushGameScreen(Splash);
        }



        #endregion
    }
}
