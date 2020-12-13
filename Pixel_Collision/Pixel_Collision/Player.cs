using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pixel_Collision
{
    public class Player
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;

        public Vector2 origin;

        GraphicsDevice graphics;

        public Color[] textureData;


        public Player()
        {

        }

 

        public void Load (ContentManager content, GraphicsDevice newGraphics)
        {
            texture = content.Load<Texture2D>("man");
            graphics = newGraphics;

            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            // TODO: use this.Content to load your game content here
        }

        public void Update(GameTime gameTime)
        {


           rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);


            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += 2;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                position.Y -= 2;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                position.Y += 2;



        }


      

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(
                texture,
                position,
                Color.White
                );
        }
    }
}
