using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Ball : GameObject
{
    public Vector2 Direction = new Vector2(0.707f, -0.707f);
    public float speed = 666;

    public Ball(Game myGame) :
        base(myGame)
    {
        textureName = "ball";
    }

    public override void Update(float deltaTime)
    {
        // movement
        position += Direction * speed * deltaTime;

        // update deltatime
        base.Update(deltaTime);
    }
}
