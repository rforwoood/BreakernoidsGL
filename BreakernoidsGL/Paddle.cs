using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Paddle : GameObject
{
    public float speed = 777;

    public Paddle(Game myGame) :
        base(myGame)
    {
        textureName = "paddle";
    }

    public override void Update(float deltaTime)
    {
        // move left
        KeyboardState keyState = Keyboard.GetState();
        if (keyState.IsKeyDown(Keys.Left))
        {
            position.X -= speed * deltaTime;
        }
        // move right
        else if (keyState.IsKeyDown(Keys.Right))
        {
            position.X += speed * deltaTime;
        }

        position.X = MathHelper.Clamp(position.X,
                                      32 + texture.Width / 2,
                                      992 - texture.Width / 2);

        // update deltatime
        base.Update(deltaTime);
    }
}
