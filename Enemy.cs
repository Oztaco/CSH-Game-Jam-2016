using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Parallel {
    public class Enemy : Entity {
        public override void draw(GameTime t, SpriteBatch sb) {
            Rectangle pos = new Rectangle((int)(position.X * 20), (int)(position.Y * 20), 20, 20);
            sb.Draw(Parallel.Sprites["enemy"], pos, Color.White);
        }
    }
}
