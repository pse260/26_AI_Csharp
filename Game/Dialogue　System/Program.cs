using System;
using System.Collections.Generic;
using static Dialogue_System.Datas;

public class Program
{
    public static void Main()
    {
        // 1. 초기 시나리오 데이터 설정
        StoryData story = new StoryData();
        story.Variables["PlayerName"] = "용사";
        story.Variables["Gold"] = "100"; // 초기 골드

        // 2. 대화 데이터 생성
        // [ID 0] 상점 입장
        Dialogue shopEntry = new Dialogue
        {
            SpeakerName = "상점 주인",
            RawContent = "어서 오게, {PlayerName}! 현재 {Gold} 골드를 가지고 있군. 무얼 도와줄까?"
        };

        // [ID 1] 구매 성공
        Dialogue buySuccess = new Dialogue
        {
            SpeakerName = "상점 주인",
            RawContent = "탁월한 선택이야! 현재 남은 골드는 {Gold} 골드라네."
        };

        // [ID 2] 종료
        Dialogue shopExit = new Dialogue
        {
            SpeakerName = "상점 주인",
            RawContent = "그래, 조심히 가게나. {PlayerName}!"
        };

        // 3. 선택지 구성 (입장 대화에 추가)
        // 선택지 A: 포션 구매
        shopEntry.Choices.Add(new Choice
        {
            ChoiceText = "빨간 포션 구매 (30 골드)",
            NextDialogueId = 1,
            OnSelected = () => {
                int gold = int.Parse(story.Variables["Gold"]);
                story.Variables["Gold"] = (gold - 30).ToString();
                Console.WriteLine("\n[시스템] 30골드를 소모하여 포션을 구매했습니다.");
            }
        });

        // 선택지 B: 그냥 나가기
        shopEntry.Choices.Add(new Choice
        {
            ChoiceText = "그냥 나간다",
            NextDialogueId = 2,
            OnSelected = () => {
                Console.WriteLine("\n[시스템] 아무것도 사지 않고 나갑니다.");
            }
        });

        // 데이터 리스트 등록
        story.Dialogues.Add(shopEntry);   // Index 0
        story.Dialogues.Add(buySuccess); // Index 1
        story.Dialogues.Add(shopExit);   // Index 2

        // 4. 프로그램 실행 (루프 시뮬레이션)
        int currentId = 0;

        while (currentId < story.Dialogues.Count)
        {
            Dialogue current = story.Dialogues[currentId];

            // 대사 출력 (동적 변수 치환 적용)
            Console.WriteLine($"\n----------------------------------");
            Console.WriteLine($"[{current.SpeakerName}]: {current.GetProcessedText(story.Variables)}");

            // 선택지가 없는 경우 (엔딩) 종료
            if (current.Choices.Count == 0) break;

            // 선택지 목록 출력
            for (int i = 0; i < current.Choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {current.Choices[i].ChoiceText}");
            }

            // 사용자 입력 처리
            Console.Write("\n선택지를 고르세요 (숫자 입력): ");
            if (int.TryParse(Console.ReadLine(), out int input) && input > 0 && input <= current.Choices.Count)
            {
                Choice selected = current.Choices[input - 1];
                selected.Execute(); // 로직 실행 (골드 차감 등)
                currentId = selected.NextDialogueId; // 다음 대화로 이동
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            // 마지막 대화(퇴장)에 도달하면 루프 종료 유도
            if (currentId == 2)
            {
                Console.WriteLine($"\n[{story.Dialogues[2].SpeakerName}]: {story.Dialogues[2].GetProcessedText(story.Variables)}");
                break;
            }
        }
    }
}
