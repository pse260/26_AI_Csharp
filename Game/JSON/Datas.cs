using System.Collections.Generic;

namespace ConsoleApp
{
    public class Dialogue
    {
        public string Speaker { get; set; }
        public string Message { get; set; }
    }

    public class DialogueData
    {
        public List<Dialogue> Dialogues { get; set; }
    }
}


