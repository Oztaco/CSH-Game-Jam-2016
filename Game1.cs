using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Parallel {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        KeyboardState oldKeyboardState;
        Keys builderLastKeyPress = Keys.None;
        Builder builder = new Builder();
        Destroyer destroyer = new Destroyer();
		Song song;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            oldKeyboardState = Keyboard.GetState();

            // Creates blank world
            for (int x = 0; x < Parallel.levelSize.X; x++) {
                Parallel.levelData[x] = new Parallel.BlockType[30];
                for (int y = 0; y < Parallel.levelSize.Y; y++) {
                    Parallel.levelData[x][y] = Parallel.BlockType.None;
                }
            }
            // Creates walls around world
            for (int x = 0; x < Parallel.levelSize.X; x++) {
                Parallel.levelData[x][0] = Parallel.BlockType.Block;
                Parallel.levelData[x][Parallel.levelSize.Y - 1] = Parallel.BlockType.Block;
            }
            for (int y = 0; y < Parallel.levelSize.Y; y++) {
                Parallel.levelData[0][y] = Parallel.BlockType.Block;
                Parallel.levelData[Parallel.levelSize.X - 1][y] = Parallel.BlockType.Block;
            }

            // Create entities
            Parallel.entities.Add(new Enemy());
            Parallel.entities.Add(new Enemy());
            Parallel.entities.Add(new Enemy());

			//song = Content.Load<Song>("FL009");
			//MediaPlayer.Play(song);


			base.Initialize();
        }

        protected override void LoadContent() {
            sb = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Parallel.Sprites.Add("background", Content.Load<Texture2D>("background"));
            Parallel.Sprites.Add("block", Content.Load<Texture2D>("block"));
            Parallel.Sprites.Add("builder", Content.Load<Texture2D>("builder"));
            Parallel.Sprites.Add("floatblock", Content.Load<Texture2D>("floatblock"));
            Parallel.Sprites.Add("redfloatblock", Content.Load<Texture2D>("redfloatblock"));
			Parallel.Sprites.Add("destroyer0", Content.Load<Texture2D>("destroyer0"));
            Parallel.Sprites.Add("destroyer1", Content.Load<Texture2D>("destroyer1"));
            Parallel.Sprites.Add("destroyer2", Content.Load<Texture2D>("destroyer2"));
            Parallel.Sprites.Add("enemy", Content.Load<Texture2D>("enemy"));
            Parallel.Sprites.Add("winbanner", Content.Load<Texture2D>("WinBanner"));
            Parallel.Sprites.Add("losebanner", Content.Load<Texture2D>("LoseBanner"));
			//Parallel.Sprites.Add("builder", Content.Load<Texture2D>("builder"));
		}

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

		private double timeSinceRedUpdate = 0;
        protected override void Update(GameTime gameTime) {
			if (Parallel.gameState == GameState.Playing) {
				KeyboardState newKeyboardState = Keyboard.GetState();
				if (builder.movement != Builder.MovementType.Still) {
					if (builder.movement == Builder.MovementType.Flying) {
						Keys k = getDirectionKey(newKeyboardState);
						if (k != Keys.None) {
							Direction keyDirection = keyToDirection(k);
							builder.flyingMove(keyDirection);
						}
					}
					if (builder.tryStop())
						builderLastKeyPress = Keys.None;
				}
				else {
					builderLastKeyPress = getDirectionKey(newKeyboardState);
					if (builderLastKeyPress != Keys.None) {
						Direction keyDirection = keyToDirection(builderLastKeyPress);
						builder.move(keyDirection);
					}
				}
				Parallel.updateEntities(gameTime);
				builder.update(gameTime);

				if (builder.checkCollision())
					Parallel.gameState = GameState.Lose;

				if (timeSinceRedUpdate >= 30) {
					Parallel.updateRedBlocks();
					Parallel.checkWin();
					timeSinceRedUpdate = 0;
				}
				else {
					timeSinceRedUpdate += gameTime.ElapsedGameTime.Milliseconds;
				}

				oldKeyboardState = newKeyboardState;
			}
			else if (Parallel.gameState == GameState.Lose) {

			}
			MouseState ms = Mouse.GetState();
			if (ms.LeftButton == ButtonState.Pressed) {
				if (Parallel.floodEnemyCheck(ms.X / 20, ms.Y / 20))
					Parallel.floodFill(ms.X / 20, ms.Y / 20);
			}
			base.Update(gameTime);
        }


		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			sb.Begin();
			
			sb.Draw(Parallel.Sprites["background"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
			for (int x = 0; x < Parallel.levelSize.X; x++) {
				for (int y = 0; y < Parallel.levelSize.Y; y++) {
					Rectangle pos;
					switch (Parallel.levelData[x][y]) {
						case Parallel.BlockType.None:
							break;
						case Parallel.BlockType.Block:
							pos = new Rectangle(x * 20, y * 20, 20, 20);
							sb.Draw(Parallel.Sprites["block"], pos, Color.White);
							break;
						case Parallel.BlockType.FloatBlock:
							pos = new Rectangle(x * 20, y * 20, 20, 20);
							sb.Draw(Parallel.Sprites["floatblock"], pos, Color.White);
							break;
						case Parallel.BlockType.RedFloatBlock:
							pos = new Rectangle(x * 20, y * 20, 20, 20);
							sb.Draw(Parallel.Sprites["redfloatblock"], pos, Color.White);
							break;
						case Parallel.BlockType.NewRedBlock:
							pos = new Rectangle(x * 20, y * 20, 20, 20);
							sb.Draw(Parallel.Sprites["redfloatblock"], pos, Color.White);
							break;
					}
				}
			}
			Parallel.drawEntities(gameTime, sb);
			sb.Draw(Parallel.Sprites["builder"], new Rectangle(new Point((int)(builder.position.X * 20), (int)(builder.position.Y * 20)), builder.size), Color.White);
			if (Parallel.gameState == GameState.Lose) {
				Rectangle pos = new Rectangle(200, 250, 400, 100);
				sb.Draw(Parallel.Sprites["losebanner"], pos, Color.White);
			}
			else if (Parallel.gameState == GameState.Win) {
				Rectangle pos = new Rectangle(200, 250, 400, 100);
				sb.Draw(Parallel.Sprites["winbanner"], pos, Color.White);
			}

			sb.End();
			base.Draw(gameTime);
		}
        
        
        
        
        // Returns ONE of whichever direction keys are pressed
        public static Keys getDirectionKey(KeyboardState newKeyboardState) {
            Keys val = Keys.None;
            if (newKeyboardState.IsKeyDown(Keys.Up)) {
                val = Keys.Up;
            }
            else if (newKeyboardState.IsKeyDown(Keys.Right)) {
                val = Keys.Right;
            }
            else if (newKeyboardState.IsKeyDown(Keys.Down)) {
                val = Keys.Down;
            }
            else if (newKeyboardState.IsKeyDown(Keys.Left)) {
                val = Keys.Left;
            }
            return val;
        }
        public static Direction keyToDirection(Keys k) {
            Direction keyDirection = Direction.Up;
            switch (k) {
                case Keys.Up:
                    break;
                case Keys.Right:
                    keyDirection = Direction.Right;
                    break;
                case Keys.Down:
                    keyDirection = Direction.Down;
                    break;
                case Keys.Left:
                    keyDirection = Direction.Left;
                    break;
            }
            return keyDirection;
        }
    }
}
