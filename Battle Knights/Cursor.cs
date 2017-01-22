using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Battle_Knights
{
    public class Cursor
    {
        public Rectangle cursorRectangle;
        public Texture2D cursorTexture;
        Game1 manager;
        public Cursor(Rectangle cursorRec, Game1 man)
        {
            cursorRectangle = cursorRec;
            manager = man;
        }
        public void Update()
        {
            if (cursorRectangle.X < 0)
            {
                cursorRectangle.X = 0;
            }
            if (cursorRectangle.X > 800 - cursorRectangle.Width)
            {
                cursorRectangle.X = 800 - cursorRectangle.Width;
            }
            if (cursorRectangle.Y < 0)
            {
                cursorRectangle.Y = 0;
            }
            if (cursorRectangle.Y > 480 - cursorRectangle.Height)
            {
                cursorRectangle.Y = 480 - cursorRectangle.Height;
            }
            cursorTexture = manager.cursorYellowTexture;
            for (int i = 0; i < manager.knightList.Count; i++)
            {
                if (cursorRectangle.Intersects(manager.knightList[i].rectangle))
                {
                    if (manager.knightList[i].team == manager.playersTurn)
                    {
                        cursorTexture = manager.cursorGreenTexture;
                    }
                    else if (manager.knightList[i].team != manager.playersTurn)
                    {
                        cursorTexture = manager.cursorRedTexture;
                    }
                }
            }
        }
        public void Move(string direction)
        {
            if (direction == "Up")
            {
                cursorRectangle.Y -= 3;
            }
            if (direction == "Down")
            {
                cursorRectangle.Y += 3;
            }
            if (direction == "Left")
            {
                cursorRectangle.X -= 3;
            }
            if (direction == "Right")
            {
                cursorRectangle.X += 3;
            }
        }
    }
}
