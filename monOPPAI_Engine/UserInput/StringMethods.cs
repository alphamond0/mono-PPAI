using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monOPPAI_Engine.UserInput
{
    public static class StringMethods
    {
        public static Vector2 GetStringOrigin(SpriteFont sf, String str)
        {
            int x = (int)sf.MeasureString(str).X / 2;
            int y = (int)sf.MeasureString(str).Y / 2;
            return new Vector2(x, y);
        }

        public static float GetStringHeight(SpriteFont sf, String str)
        {
            return sf.MeasureString(str).Y;
        }
        
        public static float GetStringWidth(SpriteFont sf, String str)
        {
            return sf.MeasureString(str).X;
        }
    }
}
