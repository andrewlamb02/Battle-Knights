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
    public class Goblin
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public bool moved;
        public Color color;
        public int health;
        Game1 manager;
        public Goblin(Rectangle rec, Game1 man)
        {
            rectangle = rec;
            manager = man;
            moved = false;
            color = Color.White;
            health = 100;
        }
        public void Update()
        {
            if (moved == true)
            {
                color = Color.Gray;
            }
            else
            {
                color = Color.White;
            }
        }
        public void Move(string direction)
        {
            if (direction == "Up")
            {
                rectangle.Y -= 1;
            }
            if (direction == "Down")
            {
                rectangle.Y += 1;
            }
            if (direction == "Left")
            {
                rectangle.X -= 1;
            }
            if (direction == "Right")
            {
                rectangle.X += 1;
            }
        }
    }
}
