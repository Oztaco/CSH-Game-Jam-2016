using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Parallel
{
    public static class Parallel
    {
        public static Dictionary<String, Texture2D> Sprites = new Dictionary<String, Texture2D>();
        public static BlockType[][] levelData = new BlockType[40][];
        public static Point levelSize = new Point(40, 30);
        public static List<Entity> entities = new List<Entity>();
		public static GameState gameState = GameState.Playing;

        public enum BlockType {
            Void, None, Block, FloatBlock, RedFloatBlock, NewRedBlock
        }
		public static BlockType getBlock(double x, double y) {
			BlockType b = BlockType.Void;
			try {
				b = Parallel.levelData[(int)Math.Round(x)][(int)Math.Round(y)];
			}
			catch (Exception e) { }
			return b;
		}
        public static void setBlock(double x, double y, BlockType b) {
            int ix = (int)Math.Round(x);
            int iy = (int)Math.Round(y);
            if (ix >= 0 && y >= 0) {
                if (ix < levelSize.X && iy < levelSize.Y) {
                    levelData[ix][iy] = b;
                }
            }
        }
        public static bool isOppositeDirection(Direction a, Direction b) {
            switch (a) {
                case Direction.Up:
                    if (b == Direction.Down)
                        return true;
                    break;
                case Direction.Right:
                    if (b == Direction.Left)
                        return true;
                    break;
                case Direction.Down:
                    if (b == Direction.Up)
                        return true;
                    break;
                case Direction.Left:
                    if (b == Direction.Right)
                        return true;
                    break;
            }
            return false;
        }
        public static void updateEntities(GameTime t) {
            foreach (Entity e in entities) {
                e.update(t);
				if (e.checkPathCollision())
					gameState = GameState.Lose;
            }
        }
		public static void updateRedBlocks() {
			updateRedBlocksHelper();
			updateRedBlocksHelper();
			updateRedBlocksHelper();
		}
		private static void updateRedBlocksHelper() {
			for (int x = 1; x <= levelSize.X - 1; x++) {
				for (int y = 1; y <= levelSize.Y - 1; y++) {
					if (levelData[x][y] == BlockType.RedFloatBlock) {
						BlockType blockUp = levelData[x][y - 1];
						BlockType blockRight = levelData[x + 1][y];
						BlockType blockDown = levelData[x][y + 1];
						BlockType blockLeft = levelData[x - 1][y];
						if (blockUp == BlockType.FloatBlock)
							levelData[x][y - 1] = BlockType.NewRedBlock;
						else if (blockUp == BlockType.NewRedBlock)
							levelData[x][y - 1] = BlockType.RedFloatBlock;
						if (blockRight == BlockType.FloatBlock)
							levelData[x + 1][y] = BlockType.NewRedBlock;
						else if (blockRight == BlockType.NewRedBlock)
							levelData[x + 1][y] = BlockType.RedFloatBlock;
						if (blockDown == BlockType.FloatBlock)
							levelData[x][y + 1] = BlockType.NewRedBlock;
						else if (blockDown == BlockType.NewRedBlock)
							levelData[x][y + 1] = BlockType.RedFloatBlock;
						if (blockLeft == BlockType.FloatBlock)
							levelData[x - 1][y] = BlockType.NewRedBlock;
						else if (blockLeft == BlockType.NewRedBlock)
							levelData[x - 1][y] = BlockType.RedFloatBlock;
					}
				}
			}
		}
		public static void checkWin() {
			float count = 0;
			for (int x = 0; x < levelSize.X; x++) {
				for (int y = 0; y < levelSize.Y; y++) {
					if (levelData[x][y] == BlockType.Block)
						count++;
				}
			}
			float size = levelSize.X * levelSize.Y;
			if (count / size >= 0.8)
				gameState = GameState.Win;
		}
		public static void floodFill(int x, int y) {
			if (Parallel.levelData[x][y] != Parallel.BlockType.None) {
				return;
			}
			Parallel.levelData[x][y] = Parallel.BlockType.Block;
			floodFill(x, y - 1);
			floodFill(x + 1, y);
			floodFill(x, y + 1);
			floodFill(x - 1, y);
		}
		public static bool floodEnemyCheck(int x, int y) {
			if (enemyAt(x, y)) return true;
			List<Point> checkedPoints = new List<Point>();
			List<Point> queue = new List<Point>();
			queue.Add(new Point(x, y));
			checkedPoints.Add(new Point(x, y));
			while(queue.Count > 0) {
				Point n = queue.ElementAt(0);
				queue.RemoveAt(0);
				if (checkedPoints.Contains(n)) {
					continue;
				}
				if (enemyAt(n.X, n.Y))
					return true;
				if (getBlock(n.X, n.Y - 1) == BlockType.None) {
					queue.Add(new Point(n.X, n.Y - 1));
					checkedPoints.Add(new Point(n.X, n.Y - 1));
				}
				if (getBlock(n.X + 1, n.Y) == BlockType.None) {
					queue.Add(new Point(n.X + 1, n.Y));
					checkedPoints.Add(new Point(n.X + 1, n.Y));
				}
				if (getBlock(n.X, n.Y + 1) == BlockType.None) {
					queue.Add(new Point(n.X, n.Y + 1));
					checkedPoints.Add(new Point(n.X, n.Y + 1));
				}
				if (getBlock(n.X - 1, n.Y) == BlockType.None) {
					queue.Add(new Point(n.X - 1, n.Y));
					checkedPoints.Add(new Point(n.X - 1, n.Y));
				}
			}
			return false;
		}
		public static bool enemyAt(int x, int y) {
			foreach (Entity e in entities) {
				if (Math.Round(e.position.X) == x && Math.Round(e.position.Y) == y)
					return true;
			}
			return false;
		}
		public static void drawEntities(GameTime t, SpriteBatch sb) {
            foreach (Entity e in entities) {
                e.draw(t, sb);
            }
        }
		public static float vectorToAngle(Vector2 vec) {
			return (float)Math.Atan2(vec.X, -vec.Y);
		}
		public static Vector2 angleToVector(float angle, float magnitude) {
			return new Vector2((float)(Math.Sin(angle) * magnitude), -(float)(Math.Cos(angle) * magnitude));
		}
    }
}
