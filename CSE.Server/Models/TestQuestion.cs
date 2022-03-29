using System.Collections.Generic;

namespace CSE.Server.Models
{
    public class TestQuestion
    {
        public TestQuestion(string question, List<Anwer> answers)
        {
            Question = question;
            Answers = answers;
        }

        public string Question { get; }
        public List<Anwer> Answers { get; }
    }
}
