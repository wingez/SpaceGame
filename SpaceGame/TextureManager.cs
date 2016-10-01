using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacegame
{
    public class TextureManager
    {
        private ContentManager content;

        private Dictionary<string,Texture2D> textures;

        public TextureManager(IServiceProvider serviceProvider, string rootDirectory)
        {
            content = new ContentManager(serviceProvider, rootDirectory);
            
        }



        public Texture2D GetTexture(string filename)
        {
            return null;

        }




    }
}
