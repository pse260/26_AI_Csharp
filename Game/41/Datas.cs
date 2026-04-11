using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _41
{
        // 1. 명령어 데이터 구조
        public struct Command
        {
            public string Verb;
            public string Target;
        }

        // 2. 시각적 연출 및 타이핑 효과
        public static class Visualizer
        {
            public static void TypeWrite(string message, int delay = 40)
            {
                foreach (char c in message)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
            }

            public static void GlitchEffect(string message, int repeat = 3)
            {
                for (int i = 0; i < repeat; i++)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n\n   ERROR: SYSTEM CORRUPTION   \n\n");
                    Thread.Sleep(50);
                    Console.ResetColor();
                    Console.Clear();
                    Thread.Sleep(30);
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                TypeWrite($"... {message} ...");
                Console.ResetColor();
            }

            public static void RepeatingText(string message, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.Red : ConsoleColor.DarkGray;
                    Console.WriteLine(message);
                    Thread.Sleep(100 + (i * 20));
                }
                Console.ResetColor();
            }
        }

        // 3. 텍스트 파서
        public class TextParser
        {
            public Command ParseInput(string input)
            {
                string cleanInput = input.Trim().ToLower();
                string[] words = cleanInput.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length >= 2)
                {
                    // "열쇠를 줍는다" -> Target: 열쇠를, Verb: 줍는다
                    return new Command { Target = words[0], Verb = words[1] };
                }
                else if (words.Length == 1)
                {
                    return new Command { Verb = words[0], Target = "" };
                }
                return new Command { Verb = "", Target = "" };
            }
        }

        // 4. 게임 엔진 (상태 및 로직 관리)
        public class GameEngine
        {
            public List<string> Inventory = new List<string>();
            public int Sanity = 100;
            public bool IsDigged = false;
            public bool HasGold = false;
            public bool CanExit = false;

            public void ProcessAction(Command cmd)
            {
                string target = cmd.Target.Replace("를", "").Replace("을", "").Replace("로", "").Trim();

                switch (cmd.Verb)
                {
                    case "본다":
                    case "조사":
                        if (target == "거울")
                        {
                            Visualizer.TypeWrite("\n거울 속의 당신이 당신보다 늦게 움직입니다.");
                            Visualizer.GlitchEffect("이것은 네가 아니다");
                            Sanity -= 30;
                        }
                        else Visualizer.TypeWrite($"\n{target}(을)를 보았지만 별다른 건 없습니다.");
                        break;

                    case "사용":
                    case "사용한다":
                        if (target == "삽")
                        {
                            Visualizer.TypeWrite("\n땅을 파헤치자 기괴한 소리가 들립니다.");
                            IsDigged = true;
                            Sanity -= 10;
                        }
                        else Visualizer.TypeWrite("\n그것은 지금 사용할 수 없습니다.");
                        break;

                    case "확인":
                    case "보다":
                        if (IsDigged && (target == "땅" || target == "상자"))
                        {
                            Visualizer.TypeWrite("그곳에 상자가 있다.", 5);
                            IsDigged = true;
                            Sanity -= 10;
                    }
                        break;

                    case "줍는다":
                    case "열기":
                        if (IsDigged && (target == "상자" || target == "황금"))
                        {
                            Visualizer.RepeatingText("그것은 황금이 아니다.", 5);
                            HasGold = true;
                            CanExit = true;
                            Inventory.Add("붉은 덩어리");
                            Visualizer.TypeWrite("\n정체를 알 수 없는 붉은 덩어리를 챙겼습니다.");
                        }
                        else Visualizer.TypeWrite($"\n{target}(을)를 보았지만 별다른 건 없습니다.");
                        break;

                    case "눈을":
                    case "감는다":
                        if (HasGold) { RunTrueEnding(); }
                        else { Visualizer.TypeWrite("\n눈을 감자 누군가의 웃음소리가 들립니다."); }
                        break;

                    case "간다":
                        if (target == "북쪽")
                        {
                            if (CanExit)
                            {
                                Visualizer.TypeWrite("\n당신은 빛을 향해 걸어 나갑니다. [Normal Ending]");
                                Environment.Exit(0);
                            }
                            else Visualizer.TypeWrite("\n문이 굳게 잠겨 있습니다.");
                        }
                        break;

                    default:
                        Visualizer.TypeWrite("\n알 수 없는 행동입니다.");
                        break;
                }
            }

            private void RunTrueEnding()
            {
                Console.Clear();
                Visualizer.RepeatingText("시스템 권한 강제 획득...", 5);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ 실험체 #402 회수 프로세스 시작 ]\n");
                Thread.Sleep(2000);
                Console.ResetColor();
                Visualizer.TypeWrite("...당신은 수술대 위에서 눈을 뜹니다. [True Ending]");
                Environment.Exit(0);
            }
        }
}