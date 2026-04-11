using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6
{
    internal class Class1
    {

        // 대화 데이터 모델
        public class Dialogue
        {
            public string Talker { get; set; }
            public string Message { get; set; }
            public ConsoleColor Color { get; set; }

            public Dialogue(string talker, string message, ConsoleColor color = ConsoleColor.White)
            {
                Talker = talker;
                Message = message;
                Color = color;
            }
        }

        // 대화 시스템 기능 클래스
        public class DialogueManager
        {
            private const int BoxWidth = 60;
            private const int BoxHeight = 8;
            private const int StartX = 5;
            private const int StartY = 12;

            // 테두리 그리기
            public void DrawBox(int x, int y, int width, int height)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y);
                Console.Write("┌" + new string('─', width - 2) + "┐");
                for (int i = 1; i < height - 1; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.Write("│" + new string(' ', width - 2) + "│");
                }
                Console.SetCursorPosition(x, y + height - 1);
                Console.Write("└" + new string('─', width - 2) + "┘");
                Console.ResetColor();
            }

            // 큐에 담긴 대화 순차 처리
            public void ProcessDialogue(Queue<Dialogue> queue)
            {
                while (queue.Count > 0)
                {
                    DrawBox(StartX, StartY, BoxWidth, BoxHeight);
                    Dialogue current = queue.Dequeue();

                    // 화자 출력
                    Console.SetCursorPosition(StartX + 2, StartY + 2);
                    Console.ForegroundColor = current.Color;
                    Console.Write($"[{current.Talker}]");
                    Console.ResetColor();

                    // 메시지 타이핑 출력
                    Console.SetCursorPosition(StartX + 2, StartY + 4);
                    foreach (char c in current.Message)
                    {
                        Console.Write(c);
                        Thread.Sleep(30);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" ▼");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }

            // 선택지 UI 출력
            public string ShowChoices(string c1, string c2)
            {
                DrawBox(StartX, 4, BoxWidth, 6);
                Console.SetCursorPosition(StartX + 2, 5);
                Console.WriteLine($"1. {c1}");
                Console.SetCursorPosition(StartX + 2, 6);
                Console.WriteLine($"2. {c2}");
                Console.SetCursorPosition(StartX + 2, 8);
                Console.Write("선택 입력: ");
                return Console.ReadLine();
            }
        }
    }

}
