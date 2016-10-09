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
    public class Destroyer
    {
        private Vector2 _pos = new Vector2(0, 0);
        private Vector2 _velocity = new Vector2(0, 0);
        public Vector2 position{
            get {
                return _pos;
            }
        }
        public MovementType movement = MovementType.Still;
        public void move(Direction d) {
            //TODO: WRITE CODE FOR DESTROYER MOVEMENT AFTER BUILDER IS COMPLETED
            if (movement == MovementType.Still) {
                switch (d)
                {
                    case Direction.Up:
                        break;

                    case Direction.Right:
                        break;

                    case Direction.Down:
                        break;

                    case Direction.Left:
                        break;
                }
            }
        }
        public void tryStop(){

        }

        public enum MovementType{
            Forward, Reverse, Still
        }
    }
}
