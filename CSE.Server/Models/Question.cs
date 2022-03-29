namespace CSE.Server.Models
{
    public class Question
    {
        public Question(string text, string[] answers)
        {
            Text = text;
            Answers = answers;
        }

        public string Text { get; }

        public string[] Answers { get; }
    }
}
