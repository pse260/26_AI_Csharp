//using System;

using _41;

namespace TextAdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // 객체 생성
            TextParser parser = new TextParser();
            GameEngine game = new GameEngine();

            // 초기 시작 화면
            Console.Clear();
            Visualizer.TypeWrite("당신은 차가운 방 안에 홀로 서 있습니다.");
            Visualizer.TypeWrite("벽에는 '거울'이 있고, 발밑에는 '삽'이 놓여 있습니다.");

            // 메인 게임 루프
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\n[정신력: {game.Sanity}] 명령 입력 > ");
                Console.ResetColor();

                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                // 1. 파싱
                Command cmd = parser.ParseInput(input);

                // 2. 로직 처리
                game.ProcessAction(cmd);
            }
        }
    }
}

