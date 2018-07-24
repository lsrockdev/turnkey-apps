using System.Linq;
using RightPath;

namespace DebugConsole
{
    using System;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var questions = new Questions().QList;

            foreach (var grp in questions.GroupBy(q => q.Category))
            {
                Console.WriteLine($"{grp.Key} {grp.Count()}");
                foreach (var question in grp)
                {
                    Console.WriteLine($"--{question.Id}");
                }
            }


            //Console.WriteLine(QuestionBank.GetNextQuestionNumber());
            //Console.WriteLine(QuestionBank.GetNextQuestionNumber(1));
            //Console.WriteLine(QuestionBank.GetNextQuestionNumber(2));
            //Console.WriteLine(QuestionBank.GetNextQuestionNumber(2,3));

            //foreach (var deferredMaint in (DeferredMaintenance[])Enum.GetValues(typeof(DeferredMaintenance)))
            //{
            //    foreach (
            //        var prevOccupantTreat in
            //            (PreviousOccupantTreatment[])Enum.GetValues(typeof(PreviousOccupantTreatment)))
            //    {
            //        var rating1 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, false);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, false, => rating: {rating1}, {CalcEngine.GetRatePerSqFt(rating1, TotalSquareFootage.Small):C}/sq ft");
            //        var rating2 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, true);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, true, => rating: {rating2}, {CalcEngine.GetRatePerSqFt(rating2, TotalSquareFootage.Small):C}/sq ft");

            //        var rating3 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, false);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, false, => rating: {rating3}, {CalcEngine.GetRatePerSqFt(rating3, TotalSquareFootage.Medium):C}/sq ft");
            //        var rating4 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, true);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, true, => rating: {rating4}, {CalcEngine.GetRatePerSqFt(rating4, TotalSquareFootage.Medium):C}/sq ft");

            //        var rating5 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, false);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, false, => rating: {rating5}, {CalcEngine.GetRatePerSqFt(rating5, TotalSquareFootage.Large):C}/sq ft");
            //        var rating6 = CalcEngine.GetRating(deferredMaint, prevOccupantTreat, true);
            //        Console.WriteLine(
            //            $"{deferredMaint}, {prevOccupantTreat}, true, => rating: {rating6}, {CalcEngine.GetRatePerSqFt(rating6, TotalSquareFootage.Large):C}/sq ft");
            //        Console.WriteLine();
            //    }
            //}

            Console.ReadKey();
        }
    }
}
