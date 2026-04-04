using System;
using System.Collections.Generic;

namespace SwordEnhanceGame
{
    class Program
    {
        // 데이터 모델 정의
        static string[] swordNames = {
            "연습용 목검", "단단한 목검", "초보 철검", "연마된 철검", "[희귀] 강철검",
            "날카로운 롱소드", "은빛 기사검", "마력 깃든 검", "광휘의 검", "[영웅] 드래곤슬레이어",
            "불꽃의 처단자", "심연의 커틀러스", "고대 용사의 유산", "파멸의 그림자", "[전설] 아스칼론",
            "천공의 집행자", "차원 분쇄기", "영혼의 수확자", "성궤의 수호검", "[신화] 창조주의 심판"
        };

        static int[] costs = { 100, 200, 400, 800, 1500, 3000, 5000, 8000, 12000, 20000, 35000, 55000, 80000, 120000, 200000, 350000, 550000, 850000, 1300000, 2500000 };
        static int[] successRates = { 100, 95, 90, 85, 80, 75, 70, 65, 60, 50, 45, 40, 35, 30, 25, 15, 10, 7, 5, 1 };

        static int currentLevel = 0;
        static int gold = 1000;
        static int protectScrolls = 0;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                ShowStatus();
                Console.WriteLine("\n[조작] E: 강화하기 | G: 상점가기 | S: 판매하기 | Q: 종료");

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.E) Enhance();
                else if (key.Key == ConsoleKey.G) GoToShop();
                else if (key.Key == ConsoleKey.S) SellSword();
                else if (key.Key == ConsoleKey.Q) break;
            }
        }

        static void ShowStatus()
        {
            Console.WriteLine("========================================");
            Console.WriteLine($" 현재 무기: +{currentLevel} {swordNames[currentLevel]}");
            Console.WriteLine($" 보유 골드: {gold:N0} G");
            Console.WriteLine($" 강화 방지권: {protectScrolls}개");
            Console.WriteLine("========================================");

            if (currentLevel < 20)
            {
                Console.WriteLine($" 다음 강화 비용: {costs[currentLevel]:N0} G");
                Console.WriteLine($" 성공 확률: {successRates[currentLevel]}%");
            }
            else
            {
                Console.WriteLine(" 최고 단계에 도달했습니다!");
            }
        }

        static void Enhance()
        {
            if (currentLevel >= 19) return;
            if (gold < costs[currentLevel])
            {
                Console.WriteLine("\n골드가 부족합니다! (아무 키나 누르세요)");
                Console.ReadKey();
                return;
            }

            gold -= costs[currentLevel];
            int roll = rand.Next(1, 101);

            Console.WriteLine("\n강화 시도 중...");
            System.Threading.Thread.Sleep(500);

            if (roll <= successRates[currentLevel])
            {
                currentLevel++;
                Console.WriteLine($"★ 강화 성공! [+{currentLevel}] {swordNames[currentLevel]}이 되었습니다!");
            }
            else
            {
                Console.WriteLine("♨ 강화 실패...");
                if (protectScrolls > 0)
                {
                    protectScrolls--;
                    Console.WriteLine("방지권을 사용하여 단계가 하락하지 않았습니다! (방지권 -1)");
                }
                else if (currentLevel > 0)
                {
                    currentLevel--;
                    Console.WriteLine("단계가 하락했습니다. (현재 +{0})", currentLevel);
                }
            }
            Console.ReadKey();
        }

        static void GoToShop()
        {
            int scrollPrice = 5000;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- [ 강화 상점 ] ---");
                Console.WriteLine($"보유 골드: {gold:N0} G");
                Console.WriteLine($"보유 방지권: {protectScrolls}개");
                Console.WriteLine("----------------------");
                Console.WriteLine($"1. 강화 방지권 구매 ({scrollPrice} G)");
                Console.WriteLine("B. 나가기");

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    if (gold >= scrollPrice)
                    {
                        gold -= scrollPrice;
                        protectScrolls++;
                        Console.WriteLine("방지권을 구매했습니다!");
                    }
                    else Console.WriteLine("골드가 부족합니다!");
                    System.Threading.Thread.Sleep(500);
                }
                else if (key.Key == ConsoleKey.B) break;
            }
        }

        static void SellSword()
        {
            // 판매가는 강화 비용의 50% * 현재 단계로 단순 계산
            int sellPrice = (costs[currentLevel] / 2) + (currentLevel * 500);
            Console.WriteLine($"\n정말로 {swordNames[currentLevel]}를 {sellPrice:N0} G에 파시겠습니까? (Y/N)");

            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                gold += sellPrice;
                currentLevel = 0;
                Console.WriteLine("판매 완료! 0단계 목검으로 초기화되었습니다.");
                Console.ReadKey();
            }
        }
    }
}
