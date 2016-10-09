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
		public static Random r = new Random();
		public static Vector2[] velocities = new Vector2[] {
			new Vector2(0.15f, 0.05f),
			new Vector2(0.11f, 0.09f),
			new Vector2(0.09f, 0.11f),
			new Vector2(0.06f, 0.14f),
			new Vector2(0.15f, 0.05f)
		};
		public Enemy() {
			position = new Vector2(r.Next(5, 35), r.Next(5, 25));
			velocity = velocities[r.Next(0, velocities.Length - 1)];
			velocity.X += r.Next(-30, 30) / 1000f;
			velocity.Y += r.Next(-30, 30) / 1000f;
		}
        public override void draw(GameTime t, SpriteBatch sb) {
            Rectangle pos = new Rectangle((int)(position.X * 20), (int)(position.Y * 20), 20, 20);
            sb.Draw(Parallel.Sprites["enemy"], pos, Color.White);
        }
    }
}
