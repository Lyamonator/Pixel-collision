using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pixel_Collision
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Texture2D cursorTexture;
        Rectangle cursorRectangle;
        Vector2 cursor;
        Color[] cursorTextureData;
        SpriteFont Arial;

        Player player;
        public bool touched;
        
        Texture2D healthTexture;
        Rectangle healthRectangle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            cursorTexture = Content.Load<Texture2D>("star");
            cursorTextureData = new Color[cursorTexture.Width * cursorTexture.Height];
            cursorTexture.GetData(cursorTextureData);

            player = new Player(100);
            player.Load(Content, GraphicsDevice);
            
            healthTexture = Content.Load<Texture2D>("Health")

            Arial = Content.Load<SpriteFont>("Fonts/Arial");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // MouseState mouse = Mouse.GetState();
            //cursorRectangle = new Rectangle(mouse.X - (cursorTexture.Width / 2), 
            //  mouse.Y - (cursorTexture.Height / 2), cursorTexture.Width, cursorTexture.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                cursor.X -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                cursor.X += 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                cursor.Y -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                cursor.Y += 2;

            cursorRectangle = new Rectangle((int)cursor.X - (cursorTexture.Width / 2),
              (int)cursor.Y - (cursorTexture.Height / 2), cursorTexture.Width, cursorTexture.Height);

            if (player.rectangle.Intersects(cursorRectangle) && IntersectsPixel(player.rectangle, player.textureData, cursorRectangle, cursorTextureData))
                touched = true;
            else
                touched = false;

            healthRectangle = new Rectangle (50, 20, player.health, 20);
            
            if (touched)
            player.health -= 10;
            
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {


            if (touched)
            {
                GraphicsDevice.Clear(Color.Red);
                player.position = player.origin;
            }
            else
                GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            _spriteBatch.Draw(cursorTexture, cursorRectangle, Color.White);
            _spriteBatch.DrawString(Arial, "Hello World", new Vector2(0, 0), Color.Black);
            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        //https://youtu.be/asU7afngQ8U
        static bool IntersectsPixel(Rectangle rect1, Color[] data1, Rectangle rect2, Color[] data2)
        {
            int top = MathHelper.Max(rect1.Top, rect2.Top);
            int bottom = MathHelper.Min(rect1.Bottom, rect2.Bottom);
            int left = MathHelper.Max(rect1.Left, rect2.Left);
            int right = MathHelper.Min(rect1.Right, rect2.Right);

            for (int y = top; y<bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color colour1 = data1[(x - rect1.Left) +
                        (y - rect1.Top) * rect1.Width];
                    Color colour2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    if (colour1.A != 0 && colour2.A != 0)
                        return true;
                }
            return false;
        }
    }
}
