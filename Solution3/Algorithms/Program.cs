using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Algorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "input.txt";
            string? filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            List<int> numbers;

            while (true)
            {
                try
                {
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("Файл не найден. Пожалуйста, введите полный путь к файлу (с именем файла): ");
                        filePath = Console.ReadLine();
                        continue;
                    }

                    numbers = File.ReadAllText(filePath).TrimEnd().Split().Select(int.Parse).ToList();
                    break;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файл не найден. Пожалуйста, введите полный путь к файлу (с именем файла): ");
                    filePath = Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Невозможно считать содержимое файла. Проверьте данные в файле и попробуйте снова!");
                    return;
                }
            }
            
            int N = (int)Math.Round(Math.Sqrt(numbers.Count));
            if (N * N != numbers.Count)
            {
                Console.WriteLine("Некорректные входные данные матрицы чисел!");
                Console.WriteLine("Предполагалось, что данные будут представлены в виде квадратной матрицы");   
                return;
            }

            int M;
            Console.Write($"Введите число M (где: 1 <= M <= 6 и M <= {N}): ");
            while (!int.TryParse(Console.ReadLine(), out M) || M > N || M < 1 || M > 6)
            {
                Console.WriteLine("Введено некорректное число. Пожалуйста, введите число, удовлетворяющее условиям:");
                Console.WriteLine($"1 <= M <= 6 и M <= {N}");
                Console.Write($": ");
            }


            var grid = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    grid[i, j] = numbers[i * N + j];
                    Console.Write($"{grid[i, j]} ");
                }
                Console.WriteLine();
            }


            int maxSum = int.MinValue;
            int maxMultiplication = int.MinValue;

            int directions = 8;
            var dx = new[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            var dy = new[] { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    for (int d = 0; d < directions; d++)
                    {
                        if (CanMove(x, y, dx[d], dy[d], M, N))
                        {
                            int sum = 0;
                            int multiplication = 1;
                            for (int k = 0; k < M; k++)
                            {
                                int newX = x + dx[d]*k;
                                int newY = y + dy[d]*k;
                                sum += grid[newX, newY];
                                multiplication *= grid[newX, newY];
                            }
                            maxSum = Math.Max(maxSum, sum);
                            maxMultiplication = Math.Max(maxMultiplication, multiplication);
                        }
                    }
                }
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Maximum sum: " + maxSum);
            Console.WriteLine("Maximum multiplication: " + maxMultiplication);
            Console.WriteLine("----------------------------");
        }

        static bool CanMove(int x, int y, int dx, int dy, int M, int N)
        {
            int newX = x + (M - 1) * dx;
            int newY = y + (M - 1) * dy;

            if (newX < 0 || newY < 0 || newX >= N || newY >= N) 
            {
                return false;
            }
                
            return true;
        }
    }
}
