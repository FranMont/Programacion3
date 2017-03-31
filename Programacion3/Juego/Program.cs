using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego
{
    class Program
    {
        static void gameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(20, 12);
            Console.Write("GAME OVER! Precione 'x' para reiniciar.");
        }
        static void DrawMap()// ╣ ╠ ╩ ╦ ╚ ╝ ╗ ╔ ═ ║
        {
            Console.SetCursorPosition(0, 0); Console.Write("╔");
            Console.SetCursorPosition(79, 0); Console.Write("╗");
            Console.SetCursorPosition(0, 24); Console.Write("╚");
            Console.SetCursorPosition(79, 24); Console.Write("╝");
            for (int i = 1; i < 79; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("═");
                Console.SetCursorPosition(i, 24);
                Console.Write("═");
            }
            for (int i = 1; i < 24; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("║");
                Console.SetCursorPosition(79, i);
                Console.Write("║");
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();

            int randspawnY = rnd.Next(1, 23);
            int randspawnX = rnd.Next(1, 77);
            bool playerDead = false;
            bool finishGame = false;
            bool gameOverShow = false;
            ConsoleKeyInfo userKey;
            Personaje player = new Personaje();            
            Enemigo[] enemies = new Enemigo[5];
            for (int i = 0; i < enemies.Length; i ++)
            {
                enemies[i] = new Enemigo(randspawnX, randspawnY, Convert.ToChar(i/*+48*/));
                randspawnY = rnd.Next(1, 23);
                randspawnX = rnd.Next(1, 77);                
            }
            DrawMap();
            player.Draw();            
            while (finishGame == false)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    if(enemies[i].collision(player.posX(), player.posY()) == true)
                    {
                        playerDead = true;
                    }
                }
                if (playerDead == true && gameOverShow == false)
                {
                    gameOverShow = true;
                    gameOver();
                }
                if (Console.KeyAvailable)
                {
                    userKey = Console.ReadKey(true);                 
                    
                    if (userKey.Key == ConsoleKey.X)
                    {
                        Console.Clear();
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            enemies[i].ResetEnemigo();
                        }
                        playerDead = false;
                        DrawMap();
                        player.ResetPersonaje();
                        gameOverShow = false;
                    }
                    if (playerDead == false)
                    {
                        player.MoveCharacter(userKey);
                        switch (userKey.Key)
                        {
                            case ConsoleKey.NumPad1:
                                player.SetPos(79, 24);//Limits
                                break;
                            case ConsoleKey.Spacebar:
                                player.SetPos(0, 0);
                                break;
                            case ConsoleKey.Escape:
                                finishGame = true;
                                break;
                        }
                    }
                }
            }
        }
    }
}
