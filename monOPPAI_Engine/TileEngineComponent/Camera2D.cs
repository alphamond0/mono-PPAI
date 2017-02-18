using System;
using Microsoft.Xna.Framework;

namespace monOPPAI_Engine.TileEngineComponent
{
    static class Camera2D
    {
        public static Vector2 CameraPosition;

        public static Vector2 AdjustForCamera(Vector2 WorldPosition)
        {
            return new Vector2(WorldPosition.X - CameraPosition.X,
                                WorldPosition.Y - CameraPosition.Y);
        }

        public static Rectangle AdjustForCamera(Rectangle WorldPosition)
        {
            Vector2 adjusted = AdjustForCamera(new Vector2(WorldPosition.X, WorldPosition.Y));

            return new Rectangle((int)adjusted.X, (int)adjusted.Y, WorldPosition.Width, WorldPosition.Height);
        }
    }
}
