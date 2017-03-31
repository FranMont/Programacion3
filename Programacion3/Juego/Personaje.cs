using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Personaje
    {
        static public int limiteX = 78;
        static public int limiteY = 23;
        int oldPosX;
        int oldPosY;
        int locationX = 0;
        int locationY = 0;
        public int posX()
        {
            return locationX;
        }
        public int posY()
        {
            return locationY;
        }
        public void SetPos(int x, int y)
        {
            locationX = x;
            locationY = y;
            Draw();
        }
        public void Draw()
        {
            Console.SetCursorPosition(oldPosX, oldPosY);
            Console.Write(" ");
            Console.SetCursorPosition(locationX, locationY);
            Console.Write("0");
        }
        public void MoveCharacter(ConsoleKeyInfo userKey)
        {
            oldPosX = locationX;
            oldPosY = locationY;
            switch (userKey.Key)
            {
                case ConsoleKey.LeftArrow:

                    if (locationX > 1)
                    {
                        locationX = locationX - 1;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (locationX < 78)
                    {
                        locationX = locationX + 1;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (locationY > 1)
                    {
                        locationY = locationY - 1;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (locationY < 23)
                    {
                            
                        locationY = locationY + 1;
                    }
                    break;

            }
            Draw();
        }
        public void ResetPersonaje()
        {
            locationX = 15;
            locationY = 15;
            oldPosX = locationX;
            oldPosY = locationY;
            Draw();
        }
        public Personaje()
        {
            Console.CursorVisible = false;
            locationX = 15;
            locationY = 15;
            oldPosX = locationX;
            oldPosY = locationY;
        }
    }
}
