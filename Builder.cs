﻿using System;
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
        public Direction _direction = Direction.Up;
        private Direction nextDirection = Direction.Up; // Only used in flying mode
        public Vector2 position {
            get {
                return _position;
            }
        }
        public Point size = new Point(20, 20);
        public MovementType movement = MovementType.Still;
        public void move(Direction d) {
            if (movement == MovementType.Still) {
                // Sets velocity of builder in the chosen direction and sets the movement type
                Parallel.BlockType nextBlock = getNextBlock(d);
                if (nextBlock == Parallel.BlockType.None)
                    movement = MovementType.Flying;
                else if (nextBlock == Parallel.BlockType.Block)
                    movement = MovementType.Walking;
                else
                    movement = MovementType.Still;
                _direction = d;
                nextDirection = d;
            }
        }
        public Parallel.BlockType getNextBlock(Direction d) {
            Parallel.BlockType nextBlock = Parallel.BlockType.Void;
            switch (d)
            {
                case Direction.Up:
                    _velocity = new Vector2(0, -0.1f);
                    if (Math.Round(_position.Y) > 0)
                        nextBlock = Parallel.levelData[(int)_position.X][(int)_position.Y - 1];
                    else
                        nextBlock = Parallel.BlockType.Void;
                    break;
                case Direction.Right:
                    _velocity = new Vector2(0.1f, 0);
                    if (Math.Round(_position.X) < Parallel.levelSize.X - 1)
                        nextBlock = Parallel.levelData[(int)_position.X + 1][(int)_position.Y];
                    else
                        nextBlock = Parallel.BlockType.Void;
                    break;
                case Direction.Down:
                    _velocity = new Vector2(0, 0.1f);
                    if (Math.Round(_position.Y) < Parallel.levelSize.Y - 1)
                        nextBlock = Parallel.levelData[(int)_position.X][(int)_position.Y + 1];
                    else
                        nextBlock = Parallel.BlockType.Void;
                    break;
                case Direction.Left:
                    _velocity = new Vector2(-0.1f, 0);
                    if (Math.Round(_position.X) > 0)
                        nextBlock = Parallel.levelData[(int)_position.X - 1][(int)_position.Y];
                    else
                        nextBlock = Parallel.BlockType.Void;
                    break;
            }
            return nextBlock;
        }
        public bool tryStop() {
            if (Math.Abs(_position.X - Math.Round(_position.X)) <= 0.05 && Math.Abs(_position.Y - Math.Round(_position.Y)) <= 0.05) {
                _position.X = (float)Math.Round(_position.X);
                _position.Y = (float)Math.Round(_position.Y);
                _velocity = new Vector2(0);
                if (movement == MovementType.Flying) {
                    Parallel.BlockType currentBlock = Parallel.levelData[(int)_position.X][(int)_position.Y];
                    if (currentBlock == Parallel.BlockType.None || currentBlock == Parallel.BlockType.FloatBlock) {
                        movement = MovementType.Still;
                        move(nextDirection);
                    }
                    else {
                        movement = MovementType.Still;
                        finishFlyingPath();
                    }
                }
                else {
                    movement = MovementType.Still;
                    finishFlyingPath();
                }
                return true;
            }
            return false;
        }
        public void flyingMove(Direction d) {
            if (movement == MovementType.Flying && _direction == nextDirection) {
                if (!Parallel.isOppositeDirection(_direction, d))
                    nextDirection = d;
            }
        }
        public void update(GameTime t) {
            if (movement != MovementType.Still)
                _position += _velocity * t.ElapsedGameTime.Milliseconds / 10f;
            Parallel.BlockType current = getCurrentBlock();
            if (current == Parallel.BlockType.None) {
                Parallel.setBlock(_position.X, _position.Y, Parallel.BlockType.FloatBlock);
            }
        }
        public Parallel.BlockType getCurrentBlock() {
            int x = (int)Math.Round(_position.X);
            int y = (int)Math.Round(_position.Y);
            return Parallel.levelData[x][y];
        }
        public void finishFlyingPath() {
			Parallel.BlockType b;
            for (int x = 0; x < Parallel.levelData.Length; x++) {
                for (int y = 0; y < Parallel.levelData[x].Length; y++) {
					b = Parallel.levelData[x][y];
					if (b == Parallel.BlockType.FloatBlock || b == Parallel.BlockType.RedFloatBlock || b == Parallel.BlockType.NewRedBlock)
                        Parallel.levelData[x][y] = Parallel.BlockType.Block;
                } 
            }
			fillInFlyingPath();
        }
		public void fillInFlyingPath() {
			
		}


		public bool checkCollision() {
			foreach (Entity e in Parallel.entities) {
				float dx = Math.Abs(e.position.X - _position.X);
				float dy = Math.Abs(e.position.Y - _position.Y);
				double mag = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
				if (mag <= 1.0)
					return true;
			}
			return false;
		}

		public enum MovementType {
            Still, Walking, Flying
        }
    }
}
