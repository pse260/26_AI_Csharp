using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue_System
{
    internal class Datas
    {
        // 1. 대화의 최소 단위 클래스
        public class Dialogue
        {
            public string SpeakerName { get; set; }      // 현재 말하는 사람의 이름
            public string RawContent { get; set; }       // 동적 변수가 포함된 원본 대화 내용 (예: "안녕, {PlayerName}!")
            public List<Choice> Choices { get; set; } = new List<Choice>(); // 이 대화 후 나타날 선택지 묶음

            // 동적 텍스트를 처리하여 최종 문자열 반환
            public string GetProcessedText(Dictionary<string, string> gameData)
            {
                string processed = RawContent;
                foreach (var data in gameData)
                {
                    processed = processed.Replace($"{{{data.Key}}}", data.Value);
                }
                return processed;
            }
        }

        // 2. 선택지 클래스
        public class Choice
        {
            public string ChoiceText { get; set; }      // 버튼 등에 표시될 텍스트
            public int NextDialogueId { get; set; }     // 선택 시 이동할 다음 대화의 ID (인덱스)
            public Action OnSelected { get; set; }      // 선택 시 실행될 특별한 로직 (호감도 상승 등)

            public void Execute()
            {
                OnSelected?.Invoke();
            }
        }

        // 3. 전체 시나리오를 관리하는 데이터 구조
        public class StoryData
        {
            public List<Dialogue> Dialogues { get; set; } = new List<Dialogue>();
            public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
        }

    }
}

