namespace CSE.Server.Models
{
    public class Anwer
    {
        public Anwer(bool isCorrectAnswer, string anwerContent)
        {
            IsCorrectAnswer = isCorrectAnswer;
            AnwerContent = anwerContent;
        }

        public bool IsCorrectAnswer { get; set; }

        public string AnwerContent { get; set; }
    }
}
