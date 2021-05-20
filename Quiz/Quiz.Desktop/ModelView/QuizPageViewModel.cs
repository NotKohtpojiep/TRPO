using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Quiz.Game;

namespace Quiz.Desktop.ModelView
{
    public class QuizPageViewModel
    {
    }

    public class QuizTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TrueOrFalseTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate MultiChoiceTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var question = item as QuizQuestion;
            if (question?.Type == QuizQuestionType.TruOrFalse)
                return TrueOrFalseTemplate;
            if (question?.Type == QuizQuestionType.MultiChoice)
                return MultiChoiceTemplate;
            if (question?.Type == QuizQuestionType.Text)
                return TextTemplate;

            throw new ArgumentNullException("Why item is null");
        }


    }
}
