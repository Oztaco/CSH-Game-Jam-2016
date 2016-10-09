using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Parallel {
    public class Entity {
        public Vector2 position = new Vector2(0);
        public Vector2 velocity = new Vector2(0.1f);
        public int radius = 10;
        public virtual bool checkBlockCollision() {
            //Vector2 nextPos = position + (velocity * (t.ElapsedGameTime.Milliseconds / 10f));
            return false;
        }
        public virtual void update(GameTime t) {
            //if (checkBlockCollision)
            position += velocity * (t.ElapsedGameTime.Milliseconds / 10f);
        }
        public virtual void draw(GameTime t, SpriteBatch sb) {}
    }
}
