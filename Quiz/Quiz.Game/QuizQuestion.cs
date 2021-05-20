using System.Linq;

namespace Quiz.Game
{
    public enum QuizQuestionType
    {
        TruOrFalse,
        MultiChoice,
        Text
    }

    public class QuizQuestion
    {
        public QuizQuestion(string question, QuizQuestionType type, QuizAnswer[] answers)
        {
            Question = question;
            Type = type;
            Answers = answers;
        }

        public string Question { get; set; }
        public QuizQuestionType Type { get; set; }
        public QuizAnswer[] Answers { get; set; }

        public virtual bool AreAnswersCorrect()
        {
            string[] answers = Answers.Where(x => x.IsChoised).Select(x => x.Text).ToArray();
            string[] rightAnswers = Answers.Where(x => x.IsRight).Select(x => x.Text).ToArray();
            return answers.SequenceEqual(rightAnswers);
        }
    }
}
