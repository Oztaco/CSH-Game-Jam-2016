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
        public void move(Direction d) {
            //Basic velocity movemetn setup.
            switch (d)
            {
                case Direction.Up:
                    _velocity = new Vector2(0, -0.1f);
                    break;

                case Direction.Right:
                    _velocity = new Vector2(0.1f, 0);
                    break;

                case Direction.Down:
                    _velocity = new Vector2(0, 0.1f);
                    break;

                case Direction.Left:
                    _velocity = new Vector2(-0.1f, 0);
                    break;

            }
        }
        public void tryStop(){

        }

        public enum MovementType{
            //TODO: HOW DO WE WANT THE SHIP TO MOVE? PURE SLIPPERY ASTROIDS STYLE MOVEMENT MIGHT BE 
            //TOO HARD FOR THIS TYPE OF GAME
            accelerate, decelerate
        }
    }
}
