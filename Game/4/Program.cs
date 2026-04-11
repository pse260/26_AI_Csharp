using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PaleLunaGame
{
    public class ParsedCommand
    {
        public string Verb { get; set; }
        public string Subject { get; set; }
    }

    public static class TextParser
    {
        public static ParsedCommand Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            // Pale Luna는 매우 단순한 단어 분할을 사용합니다.
            string[] parts = input.ToLower().Trim().Split(' ');
            var command = new ParsedCommand();

            if (parts.Length >= 2)
            {
                command.Verb = parts[parts.Length - 1]; // "금 지도를 줍는다" -> 줍는다
                command.Subject = string.Join(" ", parts.Take(parts.Length - 1))
                                    .Replace("를", "").Replace("을", "");
            }
            else
            {
                command.Verb = parts[0];
                command.Subject = "";
            }
            return command;
        }
    }

    public static class Visualizer
    {
        public static void TypeWrite(string message, int speed = 100) // Pale Luna는 아주 느리고 기괴하게 출력됩니다.
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.WriteLine();
        }

        public static void DescribeLocation(string location)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            switch (location)
            {
                case "어두운 방":
                    TypeWrite("\n어두운 방이다. 창문으로 창백한 달빛이 들어온다.");
                    TypeWrite("바닥에는 금 지도와 주머니 칼이 있다.");
                    break;
                case "숲":
                    TypeWrite("\n컴컴한 숲이다. 여기에는 금 지도가 없다.");
                    break;
                case "구덩이":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    TypeWrite("\nPale Luna가 미소 짓는다.");
                    break;
            }
            Console.ResetColor();
        }
    }

    public class Player
    {
        public List<string> Inventory { get; private set; } = new List<string>();
        public string CurrentLocation { get; set; } = "어두운 방";
        public bool IsDigged { get; set; } = false;
        public bool HasGold { get; set; } = false;

        public void UseItem(string item)
        {
            if (item == "주머니 칼" && CurrentLocation == "숲")
            {
                Visualizer.TypeWrite("\n...칼로 지면을 파헤쳤다.");
                Visualizer.TypeWrite("구덩이가 나타났다.");
                IsDigged = true;
            }
            else if (item == "금 지도" && IsDigged)
            {
                TriggerTrueEnding();
            }
            else
            {
                Visualizer.TypeWrite("\n여기서는 사용할 수 없다.");
            }
        }

        private void TriggerTrueEnding()
        {
            // Pale Luna의 악명 높은 진 엔딩 연출
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Visualizer.TypeWrite("축하한다.", 200);
            Visualizer.TypeWrite("현실의 좌표: 40.242, -121.444", 300); // 괴담 속 실제 좌표
            Visualizer.TypeWrite("구덩이를 더 깊이 파라.", 500);

            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}
