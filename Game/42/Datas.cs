using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _42
{
    internal class Datas
    {
        public struct Command
        {
            public string Verb;
            public string Target;
        }

        public static class Visualizer
        {
            public static void TypeWrite(string message, int delay = 60)
            {
                foreach (char c in message)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
            }

            public static void GlitchEffect()
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                Thread.Sleep(50);
                Console.ResetColor();
                Console.Clear();
            }
        }

        public class TextParser
        {
            public Command ParseInput(string input)
            {
                string cleanInput = input.Trim().ToLower();
                string[] words = cleanInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length >= 2)
                    return new Command { Target = words[0], Verb = words[1] }; // "금 주워" 식의 입력 대응
                else if (words.Length == 1)
                    return new Command { Verb = words[0], Target = "" };

                return new Command { Verb = "", Target = "" };
            }
        }

        public class PaleLunaEngine
        {
            public List<string> Inventory = new List<string>();
            public bool IsDigged = false;
            public bool HasGold = false;

            public void ProcessAction(Command cmd)
            {
                // Pale Luna 특유의 단답형 로직
                switch (cmd.Verb)
                {
                    case "본다":
                    case "조사":
                        if (cmd.Target == "방") Visualizer.TypeWrite("\n어두운 방이다. 창백한 달빛이 들어온다.");
                        else Visualizer.TypeWrite("\n별다른 건 없다.");
                        break;

                    case "줍는다":
                    case "주워":
                        if (cmd.Target == "금")
                        {
                            if (IsDigged && !HasGold)
                            {
                                HasGold = true;
                                Inventory.Add("금");
                                Visualizer.TypeWrite("\n금을 얻었다.");
                            }
                            else Visualizer.TypeWrite("\n금은 없다.");
                        }
                        else if (cmd.Target == "삽")
                        {
                            Inventory.Add("삽");
                            Visualizer.TypeWrite("\n삽을 얻었다.");
                        }
                        break;

                    case "판다":
                    case "파라":
                        if (Inventory.Contains("삽"))
                        {
                            Visualizer.TypeWrite("\n땅을 팠다.");
                            Visualizer.TypeWrite("금이 나왔다.");
                            IsDigged = true;
                        }
                        else Visualizer.TypeWrite("\n도구가 없다.");
                        break;

                    case "사용":
                    case "쓴다":
                        if (cmd.Target == "금")
                        {
                            if (HasGold) RunTrueEnding();
                            else Visualizer.TypeWrite("\n가진 게 없다.");
                        }
                        break;

                    case "북쪽":
                    case "동쪽":
                    case "서쪽":
                    case "남쪽":
                        Visualizer.TypeWrite("\n어둠이 앞을 가로막는다.");
                        break;

                    default:
                        Visualizer.TypeWrite("\n다시 입력하라.");
                        break;
                }
            }

            private void RunTrueEnding()
            {
                Visualizer.GlitchEffect();
                Visualizer.TypeWrite("창백한 달이 미소 짓는다.");
                Thread.Sleep(1000);
                Visualizer.TypeWrite("\n경로가 열렸다.");
                Visualizer.TypeWrite("43.3182° N, 124.5284° W"); // 실제 Pale Luna 괴담에 나오는 좌표 스타일
                Thread.Sleep(2000);
                Visualizer.TypeWrite("\n이제 파라.");
                Visualizer.TypeWrite("파라.");
                Visualizer.TypeWrite("파라.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}