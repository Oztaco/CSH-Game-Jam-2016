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
        public Vector2 position = new Vector2(5);
        public Vector2 velocity = new Vector2(0.1f);
        public int radius = 10;
   //     public virtual bool checkBlockCollision(GameTime t, Vector2 vel) {
   //         Vector2 nextPos = position + (vel * (t.ElapsedGameTime.Milliseconds / 10f));
			//Parallel.BlockType nextBlockX = Parallel.getBlock(nextPos.X, position.Y);
			//Parallel.BlockType nextBlockY = Parallel.getBlock(position.X, nextPos.Y);
			//Parallel.BlockType nextBlockBoth = Parallel.getBlock(nextPos.X, nextPos.Y);
			//if (nextBlockBoth == Parallel.BlockType.Block || nextBlockBoth == Parallel.BlockType.FloatBlock) {
			//	return true;
			//}
   //         return false;
   //     }
		public virtual bool checkPathCollision() {
			Parallel.BlockType b = Parallel.getBlock(position.X, position.Y);
			if (b == Parallel.BlockType.FloatBlock) {
				Parallel.setBlock(position.X, position.Y, Parallel.BlockType.RedFloatBlock);
				return false;
			}
			return false;
		}
		public virtual bool checkBlockCollision() {
			Vector2 nextPos = position + velocity;
			Parallel.BlockType xBlock = Parallel.getBlock(nextPos.X, position.Y);
			Parallel.BlockType yBlock = Parallel.getBlock(position.X, nextPos.Y);
			Parallel.BlockType bothBlock = Parallel.getBlock(nextPos.X, nextPos.Y);
			if (xBlock == Parallel.BlockType.Block || xBlock == Parallel.BlockType.Void)
				velocity.X *= -1;
			if (yBlock == Parallel.BlockType.Block || yBlock == Parallel.BlockType.Void)
				velocity.Y *= -1;
			return false;
		}
        public virtual void update(GameTime t) {
			//int collidesX = checkBlockCollision(t, new Vector2(velocity.X, 0));
			//int collidesY = checkBlockCollision(t, new Vector2(0, velocity.Y));
			//bool collidesBoth = checkBlockCollision(t, new Vector2(velocity.X, velocity.Y));
			//if (collidesBoth)
			//	for (int i = 0; i < 20; i++) {
			//		float mag = velocity.Length();
			//		float angle = Parallel.vectorToAngle(velocity);
			//		angle += 30;
			//		Vector2 newVel = Parallel.angleToVector(angle, mag);
			//		collidesBoth = checkBlockCollision(t, new Vector2(newVel.X, newVel.Y));
			//		if (!collidesBoth)
			//			break;
			//	}
			//if (!collidesBoth)
			//	position += velocity * (t.ElapsedGameTime.Milliseconds / 10f);
			//else {
			//	//if (colli)
			//}
			checkBlockCollision();
			if (position.X <= 1 || position.X >= Parallel.levelSize.X - 2)
				velocity.X *= -1;
			if (position.Y <= 1 || position.Y >= Parallel.levelSize.Y - 2)
				velocity.Y *= -1;
			position += velocity * (t.ElapsedGameTime.Milliseconds / 10f);
		}
        public virtual void draw(GameTime t, SpriteBatch sb) {}
    }
}
