using System;
using System.Threading;

namespace ConsoleAnimation
{
    class Program
    {
        static void Main(string[] args)
        {
            // 커서 숨기기 (깔끔한 화면을 위해)
            Console.CursorVisible = false;

            int x = 10; // 사각형의 가로 위치
            int y = 0;  // 사각형의 시작 세로 위치 (위쪽)

            while (y < 20) // 20줄까지 이동
            {
                // 1. 이전 화면 지우기
                Console.Clear();

                // 2. 현재 위치(y)에 사각형 그리기
                Console.SetCursorPosition(x, y);
                Console.WriteLine("■■■■"); // 사각형 윗부분
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("■■■■"); // 사각형 아랫부분

                // 3. 다음 위치로 이동
                y++;

                // 4. 속도 조절 (100ms 대기)
                Thread.Sleep(100);
            }

            // 1. 소수점 데이터 형식(double)으로 speed 선언
            // 값이 클수록 빨라집니다 (예: 1.0 -> 보통, 2.5 -> 빠름, 5.0 -> 매우 빠름)
            double speed = 2.5;

            Console.SetCursorPosition(0, 22);
            Console.WriteLine("애니메이션 종료!");
            Console.ReadKey();


        }
    }
}
