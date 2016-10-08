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
    public class Builder
    {
        private Vector2 _position = new Vector2(0, 0);
        private Vector2 _velocity = new Vector2(0, 0);
        public Vector2 position {
            get {
                return _position;
            }
        }
        public MovementType movement = MovementType.Still;
        public void move(Direction d) {
            if (movement == MovementType.Still) {
                // Sets velocity of builder in the chosen direction and sets the movement type
                Parallel.BlockType nextBlock;
                switch (d)
                {
                    case Direction.Up:
                        _velocity = new Vector2(0, -0.1f);
                        nextBlock = Parallel.levelData[(int)_position.X][(int)_position.Y - 1];
                        break;
                    case Direction.Right:
                        _velocity = new Vector2(0.1f, 0);
                        nextBlock = Parallel.levelData[(int)_position.X + 1][(int)_position.Y];
                        break;
                    case Direction.Down:
                        _velocity = new Vector2(0, 0.1f);
                        nextBlock = Parallel.levelData[(int)_position.X][(int)_position.Y + 1];
                        break;
                    case Direction.Left:
                        _velocity = new Vector2(-0.1f, 0);
                        nextBlock = Parallel.levelData[(int)_position.X - 1][(int)_position.Y];
                        break;
                }
                //if (nextBlock == Parallel.BlockType.None)
                //    movement = MovementType.Flying;
                //else
            }
        }
        public void tryStop() {

        }


        public enum MovementType {
            Still, Walking, Flying
        }
    }
}
