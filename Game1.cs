using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Parallel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            // Creates blank world
            for (int x = 0; x < Parallel.levelSize.X; x++) {
                Parallel.levelData[x] = new Parallel.BlockType[30];
                for (int y = 0; y < Parallel.levelSize.Y; y++) {
                    Parallel.levelData[x][y] = 0;
                }
            }
            // Creates walls around world
            for (int x = 0; x < Parallel.levelSize.X; x++)
            {
                Parallel.levelData[x][0] = Parallel.BlockType.Block;
                Parallel.levelData[x][Parallel.levelSize.Y - 1] = Parallel.BlockType.Block;
            }
            for (int y = 0; y < Parallel.levelSize.Y; y++)
            {
                Parallel.levelData[0][y] = Parallel.BlockType.Block;
                Parallel.levelData[Parallel.levelSize.X - 1][y] = Parallel.BlockType.Block;
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Parallel.Sprites.Add("background", Content.Load<Texture2D>("background"));
            Parallel.Sprites.Add("block", Content.Load<Texture2D>("block"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            sb.Begin();

            sb.Draw(Parallel.Sprites["background"], new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            for (int x = 0; x < Parallel.levelSize.X; x++) {
                for (int y = 0; y < Parallel.levelSize.Y; y++) {
                    switch (Parallel.levelData[x][y]) {
                        case Parallel.BlockType.None:
                            break;
                        case Parallel.BlockType.Block:
                            Rectangle pos = new Rectangle(x * 20, y * 20, 20, 20);
                            sb.Draw(Parallel.Sprites["block"], pos, Color.White);
                            break;
                    }
                }
            }


            sb.End();
            base.Draw(gameTime);
        }
    }
}
