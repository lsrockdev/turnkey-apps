using System;
using RightPath.Enums;

namespace RightPath.Models
{
    public class AnswerChoice
    {
        public AnswerChoice(string text, AnswerChoiceType type = AnswerChoiceType.SingleSelection, int skipCount = 0)
        {
            Text = text;
            Type = type;
            Skip = skipCount;
            Value = 0;
        }

        public string Text { get; private set; }

        bool isSelected;
        public int Value {get;set;}

		public bool IsSelected 
		{ 
			get
			{
				return isSelected;
			}
			set
			{
				AnswerTime = DateTime.Now;
				isSelected = value;
			}
		}

        public int Skip { get; private set; }
        public AnswerChoiceType Type { get; private set; }
		public DateTime AnswerTime { get; private set;}
    }
}
