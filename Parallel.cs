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

        public enum BlockType {
            Void, None, Block, FloatBlock
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
            }
        }
        public static void drawEntities(GameTime t, SpriteBatch sb) {
            foreach (Entity e in entities) {
                e.draw(t, sb);
            }
        }
    }
}
