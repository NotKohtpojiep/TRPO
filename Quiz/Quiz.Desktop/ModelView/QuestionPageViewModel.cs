using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using Quiz.Desktop.Model;
using Quiz.Game;

namespace Quiz.Desktop.ModelView
{
    public class QuestionPageViewModel : BindableBase
    {
        public QuestionPageViewModel()
        {
            Init();

            ButtonCLOCKS = new RelayCommand(_ => MainButtonClick("Количество правильных ответов: " + QuizCollection.Count(x => x.AreAnswersCorrect())));
            ButtonNextQuestion = new RelayCommand(_ => NextQuestion());
            ButtonPreviousQuestion = new RelayCommand(_ => PreviousQuestion());
        }

        private void Init()
        {
            string question = "Какие из перечисленных паттернов проектирования ограничивают платформенные зависимости?";
            QuizAnswer[] answers =
            {
                new("Flyweight",false), 
                new("Adapter", false), 
                new("Abstract Factory", true), 
                new("Bridge", true), 
                new("Interpreter", false)
            };
            QuizQuestionType questionType = QuizQuestionType.MultiChoice;

            string question2 = "Выберите все верные утверждение про отношения паттернов Фасад (Facade) и Адаптер (Adapter)";
            QuizAnswer[] answers2 =
            {
                new("Фасад задает новый интерфейс, а адаптер повторно использует старый", true),
                new("Фасад можно сделать адаптером, так как обычно нужен только один объект-фасад", false),
                new("Фасад оборачивает подсистему, а адаптер оборачивает только один класс", true),
                new("Адаптер позволяет двум существующим интерфейсам работать сообща, вместо задания нового как адаптер", true)
            };
            QuizQuestionType questionType2 = QuizQuestionType.MultiChoice;

            string question3 = "Когда следует использовать паттерн \"приспособленец\"?";
            QuizAnswer[] answers3 =
            {
                new("Когда большинство состояний объектов могут быть сохранены на диске или рассчитаны во время исполнения", true),
                new("Когда нужно сократить затраты при работе с большим количеством мелких объектов", true),
                new("Когда объект может иметь несколько представлений", false),
                new("Когда нужно изменить реализацию без изменения абстракции", false)
            };
            QuizQuestionType questionType3 = QuizQuestionType.MultiChoice;

            string question4 = "Что является преимуществом использования паттернов проектирования?";
            QuizAnswer[] answers4 =
            {
                new("Они упрощают разработку и поддержку пользовательских интерфейсов", false),
                new("Они предоставляют проверенные техники решения задач", true),
                new("Они предоставляют механизмы для тестирования модулей системы", false),
                new("Они уменьшают количество проектной документации", false),
                new("Они снижают затраты на разработку, так как они уже реализованы и их можно использовать без именений", false)
            };
            QuizQuestionType questionType4 = QuizQuestionType.TruOrFalse;


            QuizQuestion[] questions = {
                new QuizQuestion(question, questionType, answers),
                new QuizQuestion(question2, questionType2, answers2),
                new QuizQuestion(question3, questionType3, answers3),
                new QuizQuestion(question4, questionType4, answers4),
                new QuizQuestionText("Close your eyes...", "Oyasomiyoo")
            };
            QuizCollection = new ObservableCollection<QuizQuestion>(questions);
            CurrentQuestion = QuizCollection.First();
        }

        private int _currentIndex = 0;

        public ObservableCollection<QuizQuestion> QuizCollection { get; set; }
        public RelayCommand ButtonCLOCKS { get; set; }
        public RelayCommand ButtonNextQuestion { get; set; }
        public RelayCommand ButtonPreviousQuestion { get; set; }

        public QuizQuestion CurrentQuestion { get; set; }

        private void MainButtonClick(object sender)
        {
            MessageBox.Show(sender.ToString());
        }

        private void NextQuestion()
        {
            if (_currentIndex + 1 >= QuizCollection.Count)
            {
                MessageBox.Show("Достигнут конец массива");
                return;
            }
            ChangeQuestion(_currentIndex, _currentIndex + 1);
            _currentIndex++;
        }

        private void PreviousQuestion()
        {
            if (_currentIndex - 1 < 0)
            {
                MessageBox.Show("Достигнуто начало массива");
                return;
            }
            ChangeQuestion(_currentIndex, _currentIndex - 1);
            _currentIndex--;
        }

        private void ChangeQuestion(int currentIndex, int newIndex)
        {
            QuizCollection[currentIndex] = CurrentQuestion;
            CurrentQuestion = QuizCollection[newIndex];
        }
    }
}