namespace CSE.Server.Models
{
    public class Questionnaire
    {
        public static Question[] GetMockQuestionnaire()
        {
            return new[]
            {
                new Question(
                    "How many angles are in a triangle",
                    new []
                    {
                        "3",
                        "4",
                        "5"
                    }),
                new Question(
                    "The Earth in the World is -",
                    new []
                    {
                        "Our home",
                        "Planet",
                        "Something else in the world"
                    }),
                new Question(
                    "How many people's lives in Ukraine?",
                    new []
                    {
                        "30 millions",
                        "40 millions",
                        "50 millions"
                    }),
            };
        }
    }
}