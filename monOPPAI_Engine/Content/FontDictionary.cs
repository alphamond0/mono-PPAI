#region Namespace Scope Region
using System;
using System.Collections.Generic;
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

using monOPPAI_Engine.ScreenStates;

namespace monOPPAI_Engine.Content
{
    public class FontDictionary : GameComponent
    {
        #region Fields and Properties Region
        
        ContentManager _ContentRef;
        static Dictionary<String, SpriteFont> _FontDict;

        public static Boolean DebugFlag = false;

        const String FontPath = @"Content\Fonts";
        const String fontExtension = ".xnb";
        const int extensionLength = 4;
        
        /// <summary>
        /// Returns a SpriteFont based on the given FontName
        /// </summary>
        /// <param name="FontName">Font to be loaded.</param>
        /// <returns>Spritefont of the given FontName</returns>
        public static SpriteFont getFont(String FontName)
        {
            Debug.Assert(_FontDict.ContainsKey(FontName));
            if(DebugFlag)
                Console.WriteLine("[FontDictionary]Loading Font: '" + FontName + "' on "+GameScreenManager.Instance.ScreenName());
            return _FontDict[FontName];
        }

        #endregion

        #region Constructor Region

        public FontDictionary(ContentManager Content, Game game)
            : base(game)
        {
            _ContentRef = new ContentManager(Content.ServiceProvider, "Content");
            _FontDict = new Dictionary<String, SpriteFont>();
            populateFontDictionary();
            if(DebugFlag)
                dumpDictionaryContents();
        }

        public void UnloadContent()
        {
            _ContentRef.Unload();
        }
               

        #endregion

        #region XNA Methods Region

        
        #endregion

        #region Local Methods

        private String extractFontName(String FontFilePath,int extractFlag)
        {
            if (extractFlag == 0 || extractFlag == 2)
            {
                if (FontFilePath.Contains(fontExtension))
                    FontFilePath = FontFilePath.Remove(FontFilePath.Length - extensionLength, extensionLength);
            }
            if (extractFlag == 1 || extractFlag == 2)
            {
                if (FontFilePath.Contains(FontPath))
                    FontFilePath = FontFilePath.Remove(0, FontPath.Length);
            }
            if (extractFlag == 2)
            {
                if (FontFilePath.Contains(@"\"))
                {
                    FontFilePath = FontFilePath.Remove(0, 1);
                }
            }

            return FontFilePath;
        }

        private String removeRootDirectoryFromPath(String FontFilePath)
        {
            var rootDir = @"Content\";
            if (FontFilePath.Contains(rootDir))
                FontFilePath = FontFilePath.Remove(0, rootDir.Length);

            return FontFilePath;
        }

        private void populateFontDictionary()
        {
            var fontFiles = Directory.EnumerateFiles(FontPath, "*", SearchOption.AllDirectories);

            foreach (String strPath in fontFiles)
            {
                _FontDict.Add(extractFontName(strPath, 2), 
                    _ContentRef.Load<SpriteFont>(removeRootDirectoryFromPath(extractFontName(strPath, 0))));
            }
        }

        public void dumpDictionaryContents()
        {            
            Console.WriteLine("[FontDictionary]Loaded Fonts");
            foreach (KeyValuePair<String, SpriteFont> keypair in _FontDict)
            {                
                Console.WriteLine("[FontDictionary]" + keypair.Key);
            }
        }

        #endregion

    }
}
