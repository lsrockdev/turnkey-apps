using System.Collections.Generic;
using System.Linq;
using RightPath.Enums;

namespace RightPath.Models
{
	public class Question
    {
        public Question(int id, string text, EstimateCategory category, IEnumerable<AnswerChoice> choices, bool isTextAnswer = false)
        {
            Id = id;
            Text = text;
            Category = category;
            Choices = (List<AnswerChoice>) choices;
            IsTextAnswer = isTextAnswer;
        }

        public int Id { get; private set; }
        public string Text { get; private set; }
        public EstimateCategory Category { get; set; }
        public List<AnswerChoice> Choices { get; }
        public bool IsTextAnswer { get; }
        public double NumericalValue { get; set; }

        public void Reset()
        {
            if (IsTextAnswer)
            {
                NumericalValue = 0;
            }
            else
            {
                foreach (var answerChoice in Choices)
                {
                    answerChoice.IsSelected = false;
                }
            }
        }

		public void CleanAnswer()
		{
			if (Choices == null) return;

			foreach (var choice in Choices.Where(c => c.Type == AnswerChoiceType.SingleSelection && c.IsSelected).OrderByDescending(c => c.AnswerTime).Skip(1))
			{
				choice.IsSelected = false;
			}
		}
    }
}
