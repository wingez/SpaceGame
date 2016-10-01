using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacegame
{
    public class Input
    {
        KeyboardState ks, prevKs;

        public Input()
        {
            ks = prevKs = Keyboard.GetState();
        }


        public void Update()
        {
            prevKs = ks;
            ks = Keyboard.GetState();
        }

        public bool KeyDown(Keys key)
        {
            return ks.IsKeyDown(key);
        }
        public bool KeyPressed(Keys key)
        {
            return ks.IsKeyDown(key) && prevKs.IsKeyUp(key);
        }
    }
}
