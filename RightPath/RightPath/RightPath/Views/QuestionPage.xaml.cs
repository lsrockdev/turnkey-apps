using System;
using System.Linq;
using RightPath.Enums;
using RightPath.Models;
using Xamarin.Forms;

namespace RightPath.Views
{
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage(int questionIndex, Question question, Action<int> answeredAction, int questionCount)
        {
            InitializeComponent();

            //NavigationPage.SetHasBackButton(this, false);
            QuestionIndex = questionIndex;
            Question = question;
			Title = $"Question {QuestionIndex+1} of {questionCount}";

            AnsweredAction = answeredAction;          

            if (question.IsTextAnswer)
            {
    //            var textEntrySection = new TableSection();

				var questionLabel = new Label();
				questionLabel.LineBreakMode = LineBreakMode.WordWrap;
				questionLabel.Text = question.Text;

				MainStack.Children.Add(questionLabel);
				//textEntrySection.Add(new ViewCell { View = questionLabel, Height=30 });

                var entry = new Entry { Keyboard = Keyboard.Numeric };
                entry.Completed += Entry_Completed;
                entry.TextChanged += Entry_TextChanged;

				MainStack.Children.Add(entry);

    //            textEntrySection.Add(new ViewCell { View = entry });

                TextEnterButton = new Button
                {
                    Text = "Enter",
                    Command = new Command(o => CompleteEntry(entry)),
                    IsEnabled = false
                };

				MainStack.Children.Add(TextEnterButton);

    //            textEntrySection.Add(new ViewCell { View = TextEnterButton });
    //            ChoiceTable.Root = new TableRoot { textEntrySection };

                return;
            }

            if (question.Choices == null) return;
            var numberSelectionChoices = question.Choices.Where(choice => choice.Type == AnswerChoiceType.NumberSelection).ToArray();
            if (numberSelectionChoices.Any()){
                TextNumberEnterButtons = new Button[numberSelectionChoices.Length];
                var questionLabel = new Label();
                questionLabel.LineBreakMode = LineBreakMode.WordWrap;
                questionLabel.Text = question.Text;

                MainStack.Children.Add(questionLabel);

                int index = 0;
                foreach (var numberSelectionChoice in numberSelectionChoices)
                {
                    var label = new Label
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        Text = numberSelectionChoice.Text
                    };

                    var plusButton = new Button
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        //Margin = new Thickness(0, 0, 100, 0),
                        Text = "+",
                        BorderWidth = 1,
                        BorderColor = Color.FromHex("#727272"),
                        HeightRequest = 40,
                        ClassId = index.ToString()
                    };

                    plusButton.Clicked += delegate
                    {
                        numberSelectionChoice.Value++;
                        TextNumberEnterButtons[Int32.Parse(plusButton.ClassId)].Text = numberSelectionChoice.Value.ToString();
                    };


                    var minusButton = new Button
                    {
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Center,
                        Text = "-",
                        BorderWidth = 1,
                        BorderColor = Color.FromHex("#727272"),
                        HeightRequest = 40,
                        ClassId = index.ToString()
                    };

                    minusButton.Clicked += delegate
                    {
                        numberSelectionChoice.Value--;
                        if(numberSelectionChoice.Value<0){
                            numberSelectionChoice.Value = 0;
                        }
                        TextNumberEnterButtons[Int32.Parse(minusButton.ClassId)].Text = numberSelectionChoice.Value.ToString();
                    };

                    var numberButton = new Button
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        BorderWidth = 1,
                        BorderColor = Color.FromHex("#727272"),
                        //Margin = new Thickness(0, 0, 70, 0),
                        WidthRequest = 120,
                        HeightRequest = 40,
                        TextColor= Color.FromHex("#000000")
                    };

                    numberButton.Text = numberSelectionChoice.Value.ToString();
                    TextNumberEnterButtons[index] = numberButton;
                    var grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = new Thickness(0, 6, 8, 6),
                    };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    grid.Children.Add(label);
                    grid.Children.Add(numberButton, 1, 0);
                    grid.Children.Add(plusButton, 1, 0);
                    grid.Children.Add(minusButton, 1, 0);

                    MainStack.Children.Add(grid);
                    index++;
                }

                var switchButton = new Button { Text = "Enter", Command = new Command(o => AnsweredAction(QuestionIndex)), Margin = new Thickness(0, 0, 0, 10) };
                MainStack.Children.Add(switchButton);

                var otherChoices = question.Choices.Where(choice => choice.Type == AnswerChoiceType.SingleSelection);
                foreach (var answerChoice in otherChoices)
                {
                    var button = new Button { Text = answerChoice.Text, TextColor = Color.FromHex("#727272") };
                    button.Clicked += delegate
                    {
                        foreach (var numberSelectionChoice in numberSelectionChoices)
                        {
                            numberSelectionChoice.Value = 0;
                        }
                        AnsweredAction(QuestionIndex);
                    };
                    MainStack.Children.Add(button);
                    //    buttonSection.Add(new ViewCell { View = button });
                }

                return;
            }

            var checkboxChoices = question.Choices.Where(choice => choice.Type == AnswerChoiceType.MultipleSelection).ToArray();
            if (checkboxChoices.Any())
            {
				//var checkboxSection = new TableSection(question.Text);

				var questionLabel = new Label();
				questionLabel.LineBreakMode = LineBreakMode.WordWrap;
				questionLabel.Text = question.Text;

				MainStack.Children.Add(questionLabel);

                foreach (var checkboxChoice in checkboxChoices)
                {
					//    var switchCell = new SwitchCell {Text = checkboxChoice.Text};
					//    switchCell.OnChanged += (sender, e) => {
					//                                               checkboxChoice.IsSelected = e.Value;
					//    };
					//    checkboxSection.Add(switchCell);

					var label = new Label
					{
						HorizontalOptions = LayoutOptions.Start,
						VerticalOptions = LayoutOptions.FillAndExpand,
						Text = checkboxChoice.Text
					};

					var aSwitch = new Switch
					{
						HorizontalOptions = LayoutOptions.End,
						VerticalOptions = LayoutOptions.Center
					};
					aSwitch.Toggled += (sender, e) =>
					  {
						  checkboxChoice.IsSelected = e.Value;
					  };

					var grid = new Grid
					{
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Padding = new Thickness(0, 6, 8, 6)
					};
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

					grid.Children.Add(label);
					grid.Children.Add(aSwitch, 1, 0);

					MainStack.Children.Add(grid);
                }
				//checkboxSection.Add(new ViewCell
				//{
				//    View = new Button {Text = "Enter", Command = new Command(o => AnsweredAction(QuestionIndex))}
				//});

				var switchButton = new Button { Text = "Enter", Command = new Command(o => AnsweredAction(QuestionIndex)), Margin = new Thickness(0, 0, 0, 10) };
				MainStack.Children.Add(switchButton);

                //var buttonSection = new TableSection();
                var otherChoices= question.Choices.Where(choice => choice.Type == AnswerChoiceType.SingleSelection);
                foreach (var answerChoice in otherChoices)
                {
                    var button = new Button { Text = answerChoice.Text, TextColor = Color.FromHex("#727272") };
                    button.Clicked += delegate
                    {
                        foreach (var otherAnswerChoice in otherChoices)
                        {
                            otherAnswerChoice.IsSelected = false;
                        }

                        answerChoice.IsSelected = true;
                        AnsweredAction(QuestionIndex);
                    };
					MainStack.Children.Add(button);
                //    buttonSection.Add(new ViewCell { View = button });
                }

                //ChoiceTable.Root = new TableRoot {checkboxSection, buttonSection};
            }
            else
            {
				//var buttonSection = new TableSection(question.Text);

				var questionLabel = new Label();
				questionLabel.LineBreakMode = LineBreakMode.WordWrap;
				questionLabel.Text = question.Text;

				MainStack.Children.Add(questionLabel);

                foreach (var answerChoice in question.Choices)
                {
                    var button = new Button { Text = answerChoice.Text, TextColor = Color.FromHex("#727272") };
                    button.Clicked += delegate
                    {
                        foreach(var otherAnswerChoice in question.Choices){
                            otherAnswerChoice.IsSelected = false;
                        }
                        answerChoice.IsSelected = true;
                        AnsweredAction(QuestionIndex);
                    };
					MainStack.Children.Add(button);
                //    buttonSection.Add(new ViewCell { View = button });
                }

                //ChoiceTable.Root = new TableRoot { buttonSection };
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextEnterButton.IsEnabled = !string.IsNullOrWhiteSpace(e.NewTextValue);
        }

        private Button TextEnterButton { get; set; }

        private Button[] TextNumberEnterButtons { get; set; }

        public int QuestionIndex { get; set; }
        private Question Question { get; }

        private void Entry_Completed(object sender, EventArgs e)
        {
            CompleteEntry(sender);
        }

        private void CompleteEntry(object sender)
        {
            Question.NumericalValue = double.Parse(((Entry) sender).Text);
            AnsweredAction(QuestionIndex);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private Action<int> AnsweredAction { get; }
    }
}