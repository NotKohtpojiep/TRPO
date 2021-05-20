namespace Quiz.Game
{
    public class QuizAnswer
    {
        public QuizAnswer(string text, bool isRight)
        {
            Text = text;
            IsRight = isRight;
        }
        public bool IsChoised { get; set; }
        public bool IsRight { get; }
        public string Text { get; set; }
    }
}
