using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Jumping
{
    class Character
    {
        Texture2D texture;
        Vector2 position = new Vector2 (250,10);
        Vector2 velocity = new Vector2(0,0);
        float velocityXspeed;
        bool jumping;
        bool walking;

        public Character(Texture2D newTexture)
        {
            texture = newTexture;
            //position = newPosition;
            //newPosition.X = 250;
            //newPosition.Y = 100;

            //if (newPosition.Y >= 700)
                //newPosition.Y = 500;
            jumping = true;
        }
        public void Update(GameTime gameTime)
        {
            JumpInput();
            MoveInput();
        }
        private void MoveInput()
        {
            //Moves the Character right 
            GamePadState gamePad1 = GamePad.GetState(PlayerIndex.One);
             
            velocity.X = (velocityXspeed * gamePad1.ThumbSticks.Left.X);

            velocityXspeed = 4f;

            walking = true;

            // DPad keys
            if (gamePad1.DPad.Right == ButtonState.Pressed)
            {
                velocity.X = velocityXspeed;
                walking = true;
            }
            else if (gamePad1.DPad.Left == ButtonState.Pressed)
            {
                velocity.X = -velocityXspeed;
                walking = true;
            }

            // if walking true 
            if (walking == true)
            {
                velocityXspeed = 14f;
            }
            else if (walking == false)
            {
                velocityXspeed = 0; 
            }
        }
        private void JumpInput()
        {
            position += velocity;

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && jumping == false)
            {
                position.Y -= 1f;
                velocity.Y = -5f;
                jumping = true;
            }

            if (jumping == true)
            {
                float i = 1.6f;
                velocity.Y += 0.15f * i;
            }

            if (position.Y + texture.Height >= 900)
                jumping = false;

            if (jumping == false)
                velocity.Y = 0f;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
