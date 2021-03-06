﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Personaje
    {
        public const int MAX_VIDAS = 3;
        static public int limiteX = 78;
        static public int limiteY = 23;
        static private int vidas;
        int oldPosX;
        int oldPosY;
        int locationX = 0;
        int locationY = 0;
        bool j2 = false;
        static public int getVidas()
        {
            return vidas;
        }
        static public void setVidas(int _vidas)
        {
            vidas = _vidas;
        }
        public int oldX()
        {
            return oldPosX;
        }
        public int oldY()
        {
            return oldPosY;
        }
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

        static public void UpdateVidas()
        {
            Console.SetCursorPosition(35, 1);
            Console.Write("Vidas: " + getVidas());
        }
        public void Draw()
        {
            Console.SetCursorPosition(oldPosX, oldPosY);
            Console.Write(" ");
            Console.SetCursorPosition(locationX, locationY);
            if (j2 == false)
                Console.Write("0");
            else if (j2 == true)
                Console.Write("O");
        }
        public void MoveCharacter(ConsoleKeyInfo userKey)
        {
            oldPosX = locationX;
            oldPosY = locationY;
            if (j2 == false)
            {
                switch (userKey.Key)
                {
                    case ConsoleKey.LeftArrow:

                        if (locationX > 1)
                        {
                            locationX = locationX - 1;
                            Juego.giveScoreToPlayer(1, 10);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (locationX < 78)
                        {
                            locationX = locationX + 1;
                            Juego.giveScoreToPlayer(1, 10);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (locationY > 3)
                        {
                            locationY = locationY - 1;
                            Juego.giveScoreToPlayer(1, 10);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (locationY < 23)
                        {
                            locationY = locationY + 1;
                            Juego.giveScoreToPlayer(1, 10);
                        }
                        break;
                }

            }
            else if (j2 == true)
            {
                switch (userKey.Key)
                {
                    case ConsoleKey.A:

                        if (locationX > 1)
                        {
                            locationX = locationX - 1;
                            Juego.giveScoreToPlayer(2, 10);
                        }
                        break;

                    case ConsoleKey.D:
                        if (locationX < 78)
                        {
                            locationX = locationX + 1;
                            Juego.giveScoreToPlayer(2, 10);
                        }
                        break;

                    case ConsoleKey.W:
                        if (locationY > 3)
                        {
                            locationY = locationY - 1;
                            Juego.giveScoreToPlayer(2, 10);
                        }
                        break;

                    case ConsoleKey.S:
                        if (locationY < 23)
                        {

                            locationY = locationY + 1;
                            Juego.giveScoreToPlayer(2, 10);
                        }
                        break;
                }
              }
            Draw();
        }
        public void ResetPersonaje()
        {
            if (j2 == false)
            {
                locationX = 15;
                locationY = 15;
            }
            else
            {
                locationX = 30;
                locationY = 15;
            }
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
            setVidas(MAX_VIDAS);
        }
        public void P2(bool _j2)
        {
            locationX = 30;
            j2 = _j2;
            oldPosX = locationX;
        }
    }
}
