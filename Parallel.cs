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
        public enum BlockType {
            Void, None, Block, FloatBlock
        }
    }
}
