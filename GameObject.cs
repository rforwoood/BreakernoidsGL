﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameObject
{
    protected string textureName = "";
    protected Texture2D texture;
    protected Game game;
    public Vector2 position = Vector2.Zero;

    public GameObject(Game myGame)
    {
        game = myGame;
    }

    public virtual void LoadContent()
    {
        if (textureName != "")
        {
            texture = game.Content.Load(textureName);
        }
    }

    public virtual void Update(float deltaTime)
    {

    }

    public virtual void Draw(SpriteBatch batch)
    {
        if (texture != null)
        {
            Vector2 drawPosition = position;
            drawPosition.X -= texture.Width / 2;
            drawPosition.Y -= texture.Height / 2;
            batch.Draw(texture, drawPosition, Color.White);
        }
    }

    
}
