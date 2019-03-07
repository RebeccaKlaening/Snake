﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
    {
        class Program
        {
            static void Main(string[] args)
            {
                int[] xPosition = new int[50];
                xPosition[0] = 35;
                int[] yPosition = new int[50];
                yPosition[0] = 20;

                int appleX = 10;
                int appleY = 10;
                int applesEaten = 0;
                decimal gameSpeed = 150m;
                bool isWallHit = false;
                bool isGameOn = true;
                bool isAppleEaten = false;

                Random random = new Random();

                Console.CursorVisible = false;



                Console.SetCursorPosition(xPosition[0], yPosition[0]);
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("◇");

                //set apple on screen
                setApplePositionOnScreen(random, out appleX, out appleY);
                paintApple(appleX, appleY);

                buildWall();


                ConsoleKey command = Console.ReadKey().Key;



                do

                {
                    
                    switch (command)
                    {
                        case ConsoleKey.LeftArrow:
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(" ");
                            xPosition[0]--;
                            break;

                        case ConsoleKey.UpArrow:
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(" ");
                            yPosition[0]--;
                            break;

                        case ConsoleKey.RightArrow:
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(" ");
                            xPosition[0]++;
                            break;

                        case ConsoleKey.DownArrow:
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(" ");
                            yPosition[0]++;
                            break;


                    }

                    // Paint the snake
                    paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                    Console.SetCursorPosition(xPosition[0], yPosition[0]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("◇");



                    isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);

                    if (isWallHit)
                    {
                        isGameOn = false;
                        Console.SetCursorPosition(31, 11);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Game ove");
            ;
                        break;
                    }

                    isAppleEaten = determineIfAppleWasEaten(xPosition[0], yPosition[0], appleX, appleY);

                    // apple logic here
                    if (isAppleEaten)
                    {
                        setApplePositionOnScreen(random, out appleX, out appleY);
                        paintApple(appleX, appleY);
                        applesEaten++;
                        gameSpeed *= .925m;
                    }


                    if (Console.KeyAvailable) command = Console.ReadKey(true).Key;
                    System.Threading.Thread.Sleep(Convert.ToInt32(gameSpeed));

                } while (isGameOn);

        }

            private static void paintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
            {
                // Paint the head
                //Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("^");

                // Paint the body
                for (int i = 0; i < applesEaten; i++)
                {
                    Console.SetCursorPosition(xPositionIn[1], yPositionIn[1]);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("●");
                }

                //Erase last part of snake
                Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
                Console.WriteLine(" ");

                // Record location of each body part
                for (int i = applesEaten + 1; i > 0; i--)
                {
                    xPositionIn[i] = xPositionIn[i - 1];
                    yPositionIn[i] = yPositionIn[i - 1];
                }


                //Return the new array
                xPositionOut = xPositionIn;
                yPositionOut = yPositionIn;
            }

            private static bool DidSnakeHitWall(int xPosition, int yPosition)

            {
                if (xPosition <= 1 || xPosition >= 70 || yPosition <= 1 || yPosition >= 23) return true; return false;
            }


            private static void buildWall()
            {
                for (int i = 1; i < 41; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, i);
                    Console.Write("#");
                    Console.SetCursorPosition(70, i);
                    Console.Write("#");
                }

                for (int i = 1; i < 71; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(i, 1);
                    Console.Write("#");
                    Console.SetCursorPosition(i, 40);
                    Console.Write("#");
                }

            }

            private static void setApplePositionOnScreen(Random random, out int appleX, out int appleY)
            {
                appleX = random.Next(2, 68);
                appleY = random.Next(2, 23);
            }

            private static void paintApple(int appleX, int appleY)
            {
                Console.SetCursorPosition(appleX, appleY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(("O"));
            }
            private static bool determineIfAppleWasEaten(int xPosition, int yPosition, int appleX, int appleY)

            {

                if (xPosition == appleX && yPosition == appleY) return true; return false;

            }
      


        }
    }