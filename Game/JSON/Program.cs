using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 한글 깨짐 방지를 위한 인코딩 설정 (중요)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 파일 경로 설정 (실행 파일이 있는 폴더 내 dialogue.json)
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dialogue.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("JSON 파일이 존재하지 않습니다. 먼저 파일을 생성하세요.");
                return;
            }

            // JSON 읽기
            string jsonContent = File.ReadAllText(filePath, Encoding.UTF8);
            var data = JsonConvert.DeserializeObject<DialogueData>(jsonContent);

            // 출력
            Console.WriteLine("=== 대화 목록 ===");
            foreach (var d in data.Dialogues)
            {
                Console.WriteLine($"{d.Speaker}: {d.Message}");
            }
        }
    }
}
