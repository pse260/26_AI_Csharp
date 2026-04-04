using System;
using System.Collections.Generic;
using System.Threading;

namespace HiddenTreasure
{
    struct Point { public int X; public int Y; }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random rand = new Random();

            // 1. 데이터 형식 설정
            string shape = "★";
            ConsoleColor themeColor = ConsoleColor.Yellow;

            int score = 0;      // [추가] 정수형 점수
            int round = 1;      // [추가] 현재 라운드
            int maxRound = 5;   // [추가] 총 라운드 수

            // 5라운드 반복문
            while (round <= maxRound)
            {
                int playerX = 10, playerY = 10;
                Point realTreasure = new Point { X = rand.Next(5, 35), Y = rand.Next(2, 12) };

                List<Point> decoys = new List<Point>();
                for (int i = 0; i < 15; i++)
                {
                    decoys.Add(new Point { X = rand.Next(5, 35), Y = rand.Next(2, 12) });
                }

                bool isFound = false;

                while (!isFound)
                {
                    Console.Clear();
                    // 상단 UI 출력
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"[Round: {round}/{maxRound}]  [Score: {score}]");
                    Console.WriteLine("-------------------------------------------");

                    // 2. 가짜 보물들 그리기
                    Console.ForegroundColor = themeColor;
                    foreach (var d in decoys)
                    {
                        Console.SetCursorPosition(d.X, d.Y + 2); // UI 때문에 y축 +2
                        Console.Write(shape);
                    }

                    // 3. 진짜 보물 그리기
                    Console.SetCursorPosition(realTreasure.X, realTreasure.Y + 2);
                    Console.Write(shape);

                    // 4. 플레이어 그리기
                    Console.SetCursorPosition(playerX, playerY + 2);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("P");
                    Console.ResetColor();

                    // 5. 이동 제어
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow: if (playerY > 0) playerY--; break;
                        case ConsoleKey.DownArrow: if (playerY < 15) playerY++; break;
                        case ConsoleKey.LeftArrow: if (playerX > 0) playerX--; break;
                        case ConsoleKey.RightArrow: if (playerX < 40) playerX++; break;
                    }

                    // 6. 판정: 가짜 보물을 먹었을 때 (-2점)
                    for (int i = 0; i < decoys.Count; i++)
                    {
                        if (playerX == decoys[i].X && playerY == decoys[i].Y)
                        {
                            score -= 2;
                            decoys.RemoveAt(i); // 먹은 가짜는 리스트에서 제거
                            break;
                        }
                    }

                    // 7. 판정: 진짜 보물을 먹었을 때 (+5점)
                    if (playerX == realTreasure.X && playerY == realTreasure.Y)
                    {
                        score += 5;
                        isFound = true;
                        round++;
                        Console.Clear();
                        Console.WriteLine("\n\n   🎊 진짜 보물을 찾았습니다! (+5점)");
                        Thread.Sleep(1000);
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine("       GAME OVER");
            Console.WriteLine($"     최종 점수: {score}점");
            Console.WriteLine("==============================");
            Console.ReadKey();
        }
    }
}
