using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace OpenOrShut
{
    class Utilities
    {
        public static string pluginLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Texture2D shopNormalTex;
        public static Texture2D shopHoverTex;
        public static Sprite shopNormalSprite;
        public static Sprite shopHoverSprite;

        public class SpriteService
        {
            public static Sprite FromTexture(Texture2D texture)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        public class TextureService
        {
            public static Texture2D LoadTextureFromFile(string filePath)
            {
                var data = File.ReadAllBytes(filePath);

                // Do not create mip levels for this texture, use it as-is.
                var tex = new Texture2D(0, 0, TextureFormat.ARGB32, false, false)
                {
                    filterMode = FilterMode.Bilinear,
                };

                if (!tex.LoadImage(data))
                {
                    throw new Exception($"Failed to load image from file at \"{filePath}\".");
                }

                return tex;
            }

            public static void LoadTextures()
            {
                shopNormalTex = LoadTextureFromFile(pluginLoc + "/ShopIdle.png");
                shopHoverTex = LoadTextureFromFile(pluginLoc + "/ShopHover.png");
                shopNormalSprite = SpriteService.FromTexture(shopNormalTex);
                shopHoverSprite = SpriteService.FromTexture(shopHoverTex);
            }
        }
    }
}
