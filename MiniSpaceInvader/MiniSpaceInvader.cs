using System;
using System.Threading;

namespace MiniSpaceInveder
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting Console size

            int width = 60;
            int height = 54;
            Console.SetWindowSize(width, height);

            // Remove scroll bar

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            // Remove cursor

            Console.CursorVisible = false;

            // Setting the initial values of spaceship

            char spaceShip = '^';
            int rowOfSpaceShip = Console.BufferHeight - 1;
            int colOfSpaceShip = 0;

            char spaceShipProjectile = '|';

            // Setting up the enemy generator

            Random enemyGenarator = new Random();
            int minGeneratedRow = 0;
            int maxGeneratedRow = Console.WindowHeight / 2;
            int minGeneratedCol = 0;
            int maxGeneratedCol = Console.WindowWidth;

            int playerPoints = 0;

            // Setting up the enemies

            char enemy = '*';
            int rowOfEnemy = enemyGenarator.Next(minGeneratedRow, maxGeneratedRow);
            int colOfEnemy = enemyGenarator.Next(minGeneratedCol, maxGeneratedCol);

            // The game start

            Console.SetCursorPosition(colOfSpaceShip, rowOfSpaceShip);
            Console.Write(spaceShip);

            Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
            Console.Write(enemy);

            while (true)
            {
                ConsoleKeyInfo currentPressedKey = Console.ReadKey(); // Press button

                if (currentPressedKey.Key == ConsoleKey.LeftArrow && // Press (<) for move left
                    colOfSpaceShip > 0) // Max move to left is 0
                {
                    colOfSpaceShip--;
                }
                else if (currentPressedKey.Key == ConsoleKey.RightArrow && // Press (>) for move right
                         colOfSpaceShip < Console.WindowWidth - 1) // Max move to right is max WindowsWidth - 1
                {
                    colOfSpaceShip++;
                }
                else if (currentPressedKey.Key == ConsoleKey.Spacebar) // Press Spacebar for shoot
                {
                    int rowOfProjectile = rowOfSpaceShip - 1;
                    int colOfProjectile = colOfSpaceShip;

                    while (rowOfProjectile > 0) // Shoot the enemy
                    {
                        Console.Clear();

                        Console.SetCursorPosition(colOfProjectile, rowOfProjectile);
                        Console.Write(spaceShipProjectile);

                        Console.SetCursorPosition(colOfSpaceShip, rowOfSpaceShip);
                        Console.Write(spaceShip);

                        Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                        Console.Write(enemy);

                        Thread.Sleep(20); // slow down the execution

                        if (rowOfProjectile == rowOfEnemy && colOfProjectile == colOfEnemy) // when shoot the enemy
                        {
                            playerPoints++;

                            rowOfEnemy = enemyGenarator.Next(minGeneratedRow, maxGeneratedRow);
                            colOfEnemy = enemyGenarator.Next(minGeneratedCol, maxGeneratedCol);

                            break;
                        }

                        rowOfProjectile--;
                    }

                    if (rowOfProjectile == 0) // when miss the shoot
                    {
                        string messageWhenLose = "You Lost!";
                        Console.SetCursorPosition((Console.WindowWidth - messageWhenLose.Length) / 2, Console.CursorTop);
                        Console.WriteLine(messageWhenLose);

                        Console.WriteLine();

                        string messageForViewResult = "Press [R] to Restart... or any key to view your Result";
                        Console.SetCursorPosition((Console.WindowWidth - messageForViewResult.Length) / 2, Console.CursorTop);
                        Console.WriteLine(messageForViewResult);

                        ConsoleKeyInfo pressedKey = Console.ReadKey(); // Press button

                        if (pressedKey.Key == ConsoleKey.R) // Press R to Restart
                        {
                            playerPoints = 0;

                            rowOfEnemy = enemyGenarator.Next(minGeneratedRow, maxGeneratedRow);
                            colOfEnemy = enemyGenarator.Next(minGeneratedCol, maxGeneratedCol);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                Console.Clear(); // Clear last command

                Console.SetCursorPosition(colOfSpaceShip, rowOfSpaceShip);
                Console.Write(spaceShip);

                Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                Console.Write(enemy);
            }

            Console.WriteLine();

            string messageForViewPoints = $"You are points: {playerPoints}";
            Console.SetCursorPosition((Console.WindowWidth - messageForViewPoints.Length) / 2, Console.CursorTop);
            Console.WriteLine(messageForViewPoints);

        }
    }
}