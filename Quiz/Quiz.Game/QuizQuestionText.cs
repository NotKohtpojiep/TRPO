using System.Linq;

namespace Quiz.Game
{
    public class QuizQuestionText : QuizQuestion
    {
        public QuizQuestionText(string question, string rightAnswer) : base(question, QuizQuestionType.Text, new[] { new QuizAnswer("", false) })
        {
            RightAnswer = rightAnswer;
        }

        public string RightAnswer { get; set; }
        public override bool AreAnswersCorrect()
        {
            return base.Answers.First().Text.ToLower() == RightAnswer.ToLower();
        }
    }
}
