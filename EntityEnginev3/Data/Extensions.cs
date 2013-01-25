using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityEnginev3.Data
{
    public static class Extensions
    {
        public static bool RandomBool(this Random rand)
        {
            return (rand.Next(0, 2) == 0);
        }

        public static Color RandomColor(this Random rand)
        {
            var r = rand.Next(0, 256);
            var g = rand.Next(0, 256);
            var b = rand.Next(0, 256);
            return new Color(r, g, b);
        }

        public static Color[,] GetData(this Texture2D texture)
        {
            Color[] hold = new Color[texture.Width * texture.Height];
            Color[,] output = new Color[texture.Width, texture.Height];
            texture.GetData(hold);

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    output[x, y] = hold[x + y * texture.Width];
                }
            }
            return output;
        }
    }
}