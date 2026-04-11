using System;
using System.Collections.Generic;
using static _6.Class1;

namespace DialogueSystem
{
    class hasShovel
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;

            // 매니저 클래스 인스턴스 생성
            DialogueManager manager = new DialogueManager();

            // 1. 초기 대본 설정 (List<T>)
            List<Dialogue> introScript = new List<Dialogue>
            {
                new Dialogue("모험가", "드디어 이 동굴 끝에 도착했군...", ConsoleColor.Green),
                new Dialogue("수호자", "멈춰라! 이 너머는 신성한 영역이다.", ConsoleColor.Red)
            };

            // 2. 대화 시작 (Queue<T> 활용)
            manager.ProcessDialogue(new Queue<Dialogue>(introScript));

            // 3. 분기 선택
            string choice = manager.ShowChoices("무기를 내리고 대화로 푼다.", "먼저 공격을 시도한다.");
            Queue<Dialogue> nextBranch = new Queue<Dialogue>();

            // 4. Switch/If-Else 분기 처리
            if (choice == "1")
            {
                nextBranch.Enqueue(new Dialogue("수호자", "대화의 의지가 있다면 시험을 통과해 보아라.", ConsoleColor.Red));
                nextBranch.Enqueue(new Dialogue("시스템", "평화적인 루트로 진입합니다.", ConsoleColor.Gray));
            }
            else if (choice == "2")
            {
                nextBranch.Enqueue(new Dialogue("수호자", "어리석군! 너의 영혼을 이곳에 묶어주마!", ConsoleColor.DarkRed));
                nextBranch.Enqueue(new Dialogue("시스템", "전투가 시작됩니다!", ConsoleColor.Red));
            }
            else
            {
                nextBranch.Enqueue(new Dialogue("시스템", "망설이는 사이에 수호자가 사라졌습니다.", ConsoleColor.DarkGray));
            }

            manager.ProcessDialogue(nextBranch);

            Console.SetCursorPosition(0, 22);
            Console.WriteLine("시나리오 종료. 종료하려면 키를 누르세요.");
            Console.ReadKey();
        }
    }
}

