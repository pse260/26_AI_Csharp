using System;

class Program
{
    // PaleLuna 스타일의 초록색 텍스트를 위한 ANSI 코드
    const string Green = "\u001b[32m";
    const string Reset = "\u001b[0m";

    static void Main(string[] args)
    {
        // 콘솔 제목 설정
        Console.Title = "Pale Luna Console Game";

        while (true)
        {
            Console.Clear();
            // 메인 화면 출력
            Console.WriteLine(Green + "====================");
            Console.WriteLine("      달 (Luna)     ");
            Console.WriteLine("====================" + Reset);
            Console.WriteLine("1. 시작하기");
            Console.WriteLine("2. 종료");
            Console.Write("\n선택: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                StartGame();
            }
            else if (input == "2")
            {
                Console.WriteLine("게임을 종료합니다...");
                break;
            }
        }
    }

    static void StartGame()
    {
        Console.Clear();
        Console.WriteLine(Green + "[스토리 시작]" + Reset);

        // 이어지는 내용 구성
        PrintMessage("어두운 방 안에 당신은 혼자 서 있습니다.");
        PrintMessage("창밖으로 창백한 '달'이 떠오른 것이 보입니다.");
        PrintMessage("문득 발밑에 작은 열쇠 하나가 떨어진 것을 발견했습니다...");

        Console.WriteLine("\n(계속하려면 아무 키나 누르세요)");
        Console.ReadKey();
    }

    static void PrintMessage(string message)
    {
        Console.WriteLine(Green + "> " + message + Reset);
        System.Threading.Thread.Sleep(1000); // 몰입감을 위한 1초 대기


        Console.Clear();
        Console.WriteLine(Green + "[ 어두운 밤, 차가운 달빛 아래 ]" + Reset);

        PrintMessage("방 안은 고요합니다. 벽면에는 오래된 도구들이 널브러져 있습니다.");
        PrintMessage("창밖의 '달'은 평소보다 더 창백하게 빛나고 있습니다.");

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine(Green + "\n--- 무엇을 하시겠습니까? ---");
            Console.WriteLine("1. 로프를 챙긴다");
            Console.WriteLine("2. 문을 연다");
            Console.WriteLine("3. 열쇠를 줍는다");
            Console.WriteLine("4. 삽을 챙긴다" + Reset);
            Console.Write("\n당신의 선택: ");

            string choice = Console.ReadLine();

            Console.Clear();
            switch (choice)
            {
                case "1":
                    PrintMessage("거친 감촉의 '로프'를 어깨에 멨습니다. 어딘가 내려갈 때 유용할 것 같습니다.");
                    break;
                case "2":
                    PrintMessage("문을 당겨보았지만 굳게 잠겨 있습니다. 무언가 장치가 필요한 것 같습니다.");
                    break;
                case "3":
                    PrintMessage("차가운 금속 '열쇠'를 주머니에 넣었습니다. 이 방을 나갈 단서일까요?");
                    break;
                case "4":
                    PrintMessage("낡은 '삽'을 집어 들었습니다. 흙을 파헤칠 일이 생길지도 모릅니다.");
                    break;
                default:
                    PrintMessage("알 수 없는 행동입니다. 다시 생각해보세요.");
                    continue;
            }

            PrintMessage("\n달빛이 더 밝아집니다. 다음 행동을 결정해야 합니다...");
            Console.WriteLine("(계속하려면 아무 키나 누르세요)");
            Console.ReadKey();
            Console.Clear();

            // 여기서 특정 아이템을 얻었을 때 다음 시나리오로 넘어가게 설정할 수 있습니다.
        }
    }
}