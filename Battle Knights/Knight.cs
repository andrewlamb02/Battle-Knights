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
    public class Knight
    {
        public Rectangle rectangle;
        public Rectangle weaponRectangle;
        public Rectangle healthBar;
        public Texture2D texture;
        public Texture2D weaponTexture;
        public Color color;
        public bool moved;
        public String attacked;
        public int health;
        public int maxHealth;
        public string type;
        public String team;
        public int speed;
        public Rectangle center;
        public Rectangle hand;
        public String direction;
        public int verticleHeight;
        public int verticleWidth;
        public String weaponTex;
        public String tex;
        public bool moving;
        public bool attacking;
        public int attackRange;
        public Knight enemy;
        public int weaponWidth;
        public int weaponHeight;
        public int distanceToHandX;
        public int distanceToHandY;
        public int testDelay;
        public Boolean xMoved;
        public Boolean yMoved;
        Game1 manager;
        public Knight(Vector2 rec, string typechoice, String teamChoice, Game1 man)
        {
            manager = man;
            moved = false;
            color = Color.White;
            health = 100;
            maxHealth = health;
            attacked = "";
            type = typechoice;
            team = teamChoice;
            moving = false;
            xMoved = false;
            yMoved = false;
            attacking = false;
            direction = "Down";
            if (type == "archer")
            {
                rectangle = new Rectangle((int)rec.X, (int)rec.Y, 48, 44);
                center = new Rectangle(rectangle.X + (rectangle.Width / 2) - speed, rectangle.Y + (rectangle.Height / 2) - speed, speed * 2, speed * 2);
                hand = new Rectangle(rectangle.X + 2, rectangle.Y + 18, 0, 0);
                weaponRectangle = new Rectangle(hand.X - 38 / 2, hand.Y, 25, 38);
                attackRange = 300;
                speed = 2;
            }
            if (type == "footman")
            {
                rectangle = new Rectangle((int)rec.X, (int)rec.Y, 60, 40);
                center = new Rectangle(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2), speed * 2, speed * 2);
                hand = new Rectangle(rectangle.X + 3 , rectangle.Y + 19, 20, 20);
                weaponRectangle = new Rectangle(hand.X - 61 / 2, hand.Y, 17, 61);
                attackRange = 50;
                speed = 2;
            }
            verticleHeight = rectangle.Height;
            verticleWidth = rectangle.Width;
            weaponHeight = weaponRectangle.Height;
            weaponWidth = weaponRectangle.Width;
            distanceToHandX = hand.X - rectangle.X;
            distanceToHandY = hand.Y - rectangle.X;
        }
        public void Update()
        {
            if (moving == true)
            {
                Move();
            }
            if (attacking == true)
            {
                Attack();
            }
            center = new Rectangle(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2), speed*2, speed * 2);
            if (type == "archer")
            {
                weaponTex = @"images\bow" + attacked + direction;
                tex = @"images\Hunter" + direction;
            }
            if (type == "footman")
            {
                weaponTex = @"images\sword" + attacked + direction;
                tex = @"images\Knight" + direction;
            }
            weaponTexture = manager.Content.Load<Texture2D>(weaponTex);
            texture = manager.Content.Load<Texture2D>(tex);
            if (direction == "Down")
            {
                hand = new Rectangle(rectangle.X + distanceToHandX, rectangle.Y + distanceToHandY, 0, 0);
                weaponRectangle = new Rectangle(hand.X - weaponRectangle.Width / 2, hand.Y, weaponWidth, weaponHeight);
                healthBar = new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, health * rectangle.Width / maxHealth, 5);
                rectangle.Height = verticleHeight;
                rectangle.Width = verticleWidth;
            }
            if (direction == "Up")
            {
                hand = new Rectangle(rectangle.X + rectangle.Width - distanceToHandX, rectangle.Y + rectangle.Height - distanceToHandY, 0, 0);
                weaponRectangle = new Rectangle(hand.X - weaponWidth / 2, hand.Y - weaponHeight, weaponWidth, weaponHeight);
                healthBar = new Rectangle(rectangle.X, rectangle.Y - 5, health * rectangle.Width / maxHealth, 5);
                rectangle.Height = verticleHeight;
                rectangle.Width = verticleWidth;
            }
            if (direction == "Left")
            {
                hand = new Rectangle(rectangle.X + rectangle.Width - distanceToHandY, rectangle.Y + distanceToHandX, 0, 0);
                weaponRectangle = new Rectangle(hand.X - weaponHeight, hand.Y - weaponWidth / 2, weaponHeight, weaponWidth);
                healthBar = new Rectangle(rectangle.X - 5, rectangle.Y, 5, health * rectangle.Height / maxHealth);
                rectangle.Height = verticleWidth;
                rectangle.Width = verticleHeight;
            }
            if (direction == "Right")
            {
                hand = new Rectangle(rectangle.X + distanceToHandY, rectangle.Y + rectangle.Height - distanceToHandX, 0, 0);
                weaponRectangle = new Rectangle(hand.X, hand.Y - weaponWidth / 2, weaponHeight, weaponWidth);
                healthBar = new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 5, health * rectangle.Height / maxHealth);
                rectangle.Height = verticleWidth;
                rectangle.Width = verticleHeight;
            }
            if (moved == true)
            {
                color = Color.Gray;
            }
            else
            {
                color = Color.White;
            }

        }
        public void Move()
        {
            if (manager.waypointRectangle.X - center.X <= 2 && manager.waypointRectangle.X - center.X >= -2)
            {
                xMoved = true;
            }
            if (manager.waypointRectangle.Y - center.Y <= 2 && manager.waypointRectangle.Y - center.Y >= -2)
            {
                yMoved = true;
            }
            if (manager.waypointRectangle.Y >= center.Y + speed && yMoved == false)//manager.yDistanceToWaypoint <= -speed)
            {
                rectangle.Y += speed;
                direction = "Down";
            }
            if (manager.waypointRectangle.Y <= center.Y - speed && yMoved == false)
            {
                rectangle.Y -= speed;
                direction = "Up";
            }
            if (manager.waypointRectangle.X <= center.X - speed && xMoved == false)
            {
                rectangle.X -= speed;
                direction = "Left";
            }
            if (manager.waypointRectangle.X >= center.X + speed && xMoved == false)
            {
                rectangle.X += speed;
                direction = "Right";
            }
            if (xMoved == true && yMoved == true)
            {
                manager.turnPhase = 0;
                moved = true;
                moving = false;
                xMoved = false;
                yMoved = false;
                if (rectangle.X < 0)
                {
                    rectangle.X = 0;
                }
                if (rectangle.X > 800 - rectangle.Width)
                {
                    rectangle.X = 800 - rectangle.Width;
                }
                if (rectangle.Y < 0)
                {
                    rectangle.Y = 0;
                }
                if (rectangle.Y > 480 - rectangle.Height)
                {
                    rectangle.Y = 480 - rectangle.Height;
                }
                manager.unit = 0;
            }
        }
        public void Attack()
        {
            if (manager.distanceToWaypoint > attackRange || manager.distanceToWaypoint > -attackRange)
            {
                if (manager.waypointRectangle.Y < rectangle.Y)//manager.yDistanceToWaypoint <= -speed)
                {
                    rectangle.Y += speed;
                    direction = "Down";
                }
                if (manager.waypointRectangle.Y > rectangle.Y)
                {
                    rectangle.Y -= speed;
                    direction = "Up";
                }
                if (manager.waypointRectangle.X < rectangle.X)
                {
                    rectangle.X -= speed;
                    direction = "Left";
                }
                if (manager.waypointRectangle.X > rectangle.X)
                {
                    rectangle.X += speed;
                    direction = "Right";
                }
            }
            else
            {
                enemy.health = enemy.health - 25;
                moved = true;
                attacking = false;
                attacked = "Attacked";
            }
        }
    }
}
