using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Paddle : GameObject
{
    public Paddle(Game myGame) :
        base(myGame)
    {
        textureName = "paddle";
    }
}
