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
    public class InputHandler
    {
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;
        public bool wait;
        Game1 manager;
        public InputHandler()
        {
        }
        public InputHandler(Game1 man)
        {
            manager = man;
            wait = false;
            keyboardState = Keyboard.GetState();
            lastKeyboardState = Keyboard.GetState();
        }
        public void InputControl()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space) && wait == false && manager.turnPhase == 0)
            {
                if (manager.playersTurn == "Blue")
                {
                    manager.playersTurn = "Red";
                }
                else if (manager.playersTurn == "Red")
                {
                    manager.playersTurn = "Blue";
                }
                wait = true;
                for (int i = 0; i < manager.knightList.Count; i++)
                {
                    manager.knightList[i].moved = false;
                    manager.knightList[i].attacked = "";
                }
            }
            if (manager.turnPhase == 0)
            {
                for (int i = 0; i < manager.knightList.Count; i++)
                {
                    if (keyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter) && manager.cursor.cursorRectangle.Intersects(manager.knightList[i].rectangle) && wait == false && manager.knightList[i].moved == false && manager.knightList[i].team == manager.playersTurn)
                    {
                        manager.turnPhase = 1;
                        manager.unit = i;
                        wait = true;
                        manager.movesquareRectangle = new Rectangle((int)manager.knightList[i].center.X - manager.knightList[i].speed * 50, (int)manager.knightList[i].center.Y - manager.knightList[i].speed * 50, manager.knightList[i].speed * 100, manager.knightList[i].speed * 100);
                    
                    }
                }
            }
            if (manager.turnPhase == 1)
            {
                manager.waypointRectangle = new Rectangle(0, 0, 0, 0);
                if (keyboardState.IsKeyDown(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Enter) && wait == false && manager.cursor.cursorRectangle.Intersects(manager.movesquareRectangle))
                {
                    manager.waypointRectangle = new Rectangle(manager.cursor.cursorRectangle.X + (manager.cursor.cursorRectangle.Width / 2) - manager.knightList[manager.unit].speed, manager.cursor.cursorRectangle.Y + (manager.cursor.cursorRectangle.Height / 2) - manager.knightList[manager.unit].speed, manager.knightList[manager.unit].speed*2, manager.knightList[manager.unit].speed*2);
                    wait = true;
                    for (int j = 0; j < manager.knightList.Count; j++)
                    {
                        if (manager.cursor.cursorRectangle.Intersects(manager.knightList[j].rectangle) && manager.knightList[manager.unit].team != manager.knightList[j].team)
                        {
                            manager.knightList[manager.unit].attacking = true;
                            manager.knightList[manager.unit].enemy = manager.knightList[j];
                        }
                    }
                    if (manager.knightList[manager.unit].attacking != true) 
                    {
                        manager.knightList[manager.unit].moving = true;
                    }
                    manager.turnPhase = 0;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                manager.cursor.Move("Up");
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                manager.cursor.Move("Down");
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                manager.cursor.Move("Right");
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                manager.cursor.Move("Left");
            }
            if (wait == true && lastKeyboardState.IsKeyUp(Keys.Enter) && lastKeyboardState.IsKeyUp(Keys.Space))
            {
                wait = false;
            }
            lastKeyboardState = Keyboard.GetState();
        }
    }
}