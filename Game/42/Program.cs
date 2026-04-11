using System;
using static _42.Datas;

namespace PaleLunaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TextParser parser = new TextParser();
            PaleLunaEngine game = new PaleLunaEngine();

            Console.Clear();
            Visualizer.TypeWrite("PALE LUNA (C) 1985\n", 100);
            Visualizer.TypeWrite("어두운 방이다. 창백한 달빛이 창가로 들어온다.");
            Visualizer.TypeWrite("바닥에는 '삽'이 있다.");
            Visualizer.TypeWrite("여기에는 '금'이 없다.");

            while (true)
            {
                Console.Write("\n- ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                Command cmd = parser.ParseInput(input);
                game.ProcessAction(cmd);
            }
        }
    }
}
