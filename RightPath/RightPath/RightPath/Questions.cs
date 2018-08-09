using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RightPath.Enums;
using RightPath.Models;

namespace RightPath
{
    public class Questions
    {
        public Questions()
        { // Provide a count of I don't know
            QList = new List<Question>
            {
                new Question(10000, "What is the zip code of the house?", EstimateCategory.None, null, true),
                new Question(1, "What is the square footage of the house, excluding the garage?", EstimateCategory.None,
                    null, true),
                new Question(2, "Is the house:", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Single story", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("Multiple stories"), // Affects flooring premium - cut flooring premium in half
                        new AnswerChoice("Don't know", AnswerChoiceType.SingleSelection, 1) // Single story
                    }),
                new Question(210, "On the upper stories: Is the flooring mostly carpet or will it be?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(101, "Does the house have a garage?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("One car"), // Add 200 sqft to house for ROOF, SIDING, and BRICK calcs
                        new AnswerChoice("Two cars"), // Add 400 sqft
                        new AnswerChoice("Three cars or more"), // Add 600 sqft
                        new AnswerChoice("No garage"),
                        new AnswerChoice("I don't know") // Two cars
                    }),

                new Question(42, "Does the house have any odors: (mildew, musty, smoke, pets, etc.)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know") // No change
                    }),
                new Question(43,
                             "Grading how house was treated: Is there visible sheetrock damage in the house (holes, moisture, bad patchwork, cracks, paneling etc. or non-neutral paint or wallpaper)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("One to two rooms"), // Add 1
                        new AnswerChoice("Half of the house or less"), // Add 1.5
                        new AnswerChoice("Most of the house"), // Add 2
                        new AnswerChoice("No damage"),
                        new AnswerChoice("I don't know") // No change
                    }),

                new Question(44, "Grading how house was treated: Are any of these items damaged or missing (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Windows or doors", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Trim (inside or out)", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Flooring or appliances", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Counters or cabinets", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("None of the above"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(7, "Are you planning on replacing or refinishing the kitchen flooring?", EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 0.5 + 1/8*flooringPremium // Flip grade
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(201, "Are you planning on replacing or refinishing the bathroom flooring?", EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("Some"),
                        new AnswerChoice("I don't know")
                    }),
                //new Question(300, "If some, in how many bathrooms will the flooring be replaced?",
                    //EstimateCategory.Basic, null, true),
                
                new Question(202, "Are you planning on replacing or refinishing the utility room flooring?", EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(8, "Are you planning on replacing or refinishing any of the living area flooring (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Main living areas", AnswerChoiceType.MultipleSelection),
                        // Add 0.75 + 7/16*flooringPremium
                        new AnswerChoice("Hallways", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + 1/8*flooringPremium
                        new AnswerChoice("Stairs", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + 1/8*flooringPremium
                        new AnswerChoice("No, I'm keeping it as it is")
                    }),
                new Question(9, "Are you planning on replacing or refinishing any of the bedroom flooring (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Master bedroom", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + 1/8*flooringPremium
                        new AnswerChoice("Other bedrooms", AnswerChoiceType.MultipleSelection),
                        // Add 0.25 + 1/16*flooringPremium
                        new AnswerChoice("No, I'm keeping it as it is")
                    }),
                new Question(102, "How many bathrooms does the house have?", EstimateCategory.None, null, true),
                new Question(10, "Are you planning on replacing any of the bath tub or shower surrounds?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 0.75 per bathroom count
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1)
                    }),
                new Question(200, "If yes, how many tubs, showers or surrounds will be installed?",
                    EstimateCategory.None, null, true),
                new Question(11, "Are you planning on replacing any of the plumbing fixtures (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Kitchen faucets", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + (1/3)*(1/4)*fixturePremium
                        new AnswerChoice("Bathroom faucets", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + (1/3)*(1/4)*fixturePremium
                        new AnswerChoice("Toilets", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + (1/3)*(1/4)*fixturePremium
                        new AnswerChoice("Utility room/ wet bar faucets", AnswerChoiceType.MultipleSelection),
                        // Add 0.125 + (1/3)*(1/4)*fixturePremium
                        new AnswerChoice("No, I'm keeping it as it is")
                    }),
                new Question(12, "Are you planning on replacing all of the existing outlets and switches?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 1/3
                        new AnswerChoice("No"),
                        new AnswerChoice("Changing some"), // Add 0.1
                        new AnswerChoice("I don't know")
                    }),
                new Question(45, "Are you planning on replacing all of the existing light fixtures?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 1 + (2/3)*fixturePremium
                        new AnswerChoice("No"),
                        new AnswerChoice("Replacing some"), // Add 1/3 + (2/3)*(1/3)*fixturePremium
                        new AnswerChoice("I don't know")
                    }),
                new Question(13, "Are you planning on replacing any of the countertops (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Kitchen", AnswerChoiceType.MultipleSelection),
                        // $1000A + $1500A*countertopPremium
                        new AnswerChoice("Master bathroom", AnswerChoiceType.MultipleSelection),
                        // $300A + $300A*countertopPremium
                        new AnswerChoice("Other bathrooms", AnswerChoiceType.MultipleSelection),
                        // $200A each (-1) + $200A*countertopPremium each (-1)
                        new AnswerChoice("Utility room/ wet bar", AnswerChoiceType.MultipleSelection),
                        // $200A + $200A*countertopPremium
                        new AnswerChoice("No, I'm keeping it as it is")
                    }),
                new Question(14, "Are you planning on adding or replacing any cabinets (select all that apply)?",
                    EstimateCategory.Basic,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Kitchen", AnswerChoiceType.MultipleSelection),
                        // $3000A + $1000A*cabinetPremium
                        new AnswerChoice("Master bathroom", AnswerChoiceType.MultipleSelection),
                        // $500A + $200A*cabinetPremium
                        new AnswerChoice("Other bathrooms", AnswerChoiceType.MultipleSelection),
                        // $200A + $200A*cabinetPremium
                        new AnswerChoice("Utility room/ wet bar", AnswerChoiceType.MultipleSelection),
                         // $300A each (-1) + $200A*cabinetPremium each (-1)
                        new AnswerChoice("Any Other Room", AnswerChoiceType.MultipleSelection),
                        // $200A + $200A*cabinetPremium
                        new AnswerChoice("No, Just painting or keeping as-is")
                    }),
                new Question(103, "Are you planning on replacing all of the appliances (excluding refrigerator)?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // $1500A + $750A*appliancePremium
                        new AnswerChoice("No"),
                        new AnswerChoice("Replacing some"), // $750A + $375A*appliancePremium
                        new AnswerChoice("I don't know")
                    }),
                new Question(104, "Are you planning on painting the exterior of the house?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 1 
                        new AnswerChoice("No"),
                        new AnswerChoice("Touch-up only"), // Add 0.25
                        new AnswerChoice("I don't know")
                    }),
                new Question(105, "Are you planning on painting the interior of the house?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"), // Add 1.60 
                        new AnswerChoice("No"),
                        new AnswerChoice("Touch-up only"), // Add 0.4 per choice in Q43 (A=40, B=80, etc)
                        new AnswerChoice("I don't know")
                    }),
                new Question(16, "How much do you estimate the property will be worth after repairs?",
                    EstimateCategory.None, // Flip
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("< $100K"),
                        new AnswerChoice("$100K - $120K"),
                        new AnswerChoice("$120K - $140K"),
                        new AnswerChoice("$140K - $180K"),
                        new AnswerChoice("$180K - $225K"),
                        new AnswerChoice("$225K - $300K"),
                        new AnswerChoice("$300K - $400K"),
                        new AnswerChoice("$400K - $550K"),
                        new AnswerChoice("$550K - $800K")
                    }),
                new Question(17, "Is there a foundation issue in the house?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 6),
                        new AnswerChoice("Maybe")
                    }),
                new Question(18, "Are there any settlement cracks in the sheetrock?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1)
                    }),
                new Question(19, "If yes, how many?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("< 10"),
                        new AnswerChoice("10 - 20"),
                        new AnswerChoice("> 20")
                    }),
                new Question(203, "Are there any settlement cracks in the brick or foundation?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1)
                    }),
                new Question(204, "If yes, how many?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("1 - 2"),
                        new AnswerChoice("3 - 5"),
                        new AnswerChoice("> 5")
                    }),
                new Question(20, "Are there any slopes you can feel or see?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),

                new Question(500, "If yes, how many?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("No slopes"),
                        new AnswerChoice("1"),
                        new AnswerChoice("2"),
                        new AnswerChoice("3"),
                        new AnswerChoice("4 or more"),
                    }),


                new Question(21, "How many of those slopes are next to a bathroom, kitchen, or utility room?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("None” (No effect)"),
                        new AnswerChoice("1"),
                        new AnswerChoice("2"),
                        new AnswerChoice("3"),
                        new AnswerChoice("4")
                    }),
                new Question(22, "Does the house need to be re-piped (plumbing in attic and walls)?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("Water heater replacement only"), // Add 0.5
                        new AnswerChoice("Fix leaks only"), // Add 0.5
                        new AnswerChoice("I don't know")
                    }),
                new Question(23, "Does the HVAC system (A/C / Heater) need to be repaired or replaced?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No",AnswerChoiceType.SingleSelection, 4),
                        new AnswerChoice("Just needs servicing",AnswerChoiceType.SingleSelection, 4),
                        new AnswerChoice("I don't know")
                    }),
                new Question(24, "Does the house need a complete system (inside and outside units)?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes, just units", AnswerChoiceType.SingleSelection, 3),
                        new AnswerChoice("Yes, units and ductwork", AnswerChoiceType.SingleSelection, 3),
                        new AnswerChoice("Yes, house has never had central A/C before", AnswerChoiceType.SingleSelection,
                            3),
                        new AnswerChoice("No, needs partial repair/ductwork"),
                        new AnswerChoice("No, does not need unit replacement", AnswerChoiceType.SingleSelection,
                            3),
                        new AnswerChoice("I don't know")
                    }),
                new Question(25, "Does the condenser need to be replaced (outside unit)?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(26, "Does the furnace/air handler need to be replaced (inside units)?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(27, "Does the ductwork need to be replaced?", EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(28, "Are there any rough electrical issues that need repair or replacement?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 2),
                        new AnswerChoice("I don't know")
                    }),
                new Question(29, "Does the house need re-wiring?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("Some Repair(i.e aluminum wiring)"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(30, "Does the house need a new electric box?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(31, "Will you be removing or adding walls in the existing structure?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 2),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1) // No
                    }),
                new Question(110, "Select all areas where walls will be removed (if common wall is being removed  select biggest room of the two):", EstimateCategory.RaritiesRearranges,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Kitchen", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Dining Room", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Living Room", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Master Bathroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Other Bathroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Master Bedroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Other Bedroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Extra Room (study, media room, loft, etc.)",
                            AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("I'm not removing any walls")
                    }),
                new Question(301, "Select all areas where walls will be added:", EstimateCategory.RaritiesRearranges,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Kitchen", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Dining Room", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Living Room", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Master Bathroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Other Bathroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Master Bedroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Other Bedroom", AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("Extra Room (study, media room, loft, etc.)",
                            AnswerChoiceType.MultipleSelection),
                        new AnswerChoice("I'm not adding any walls")
                    }),
                new Question(33, "Are you planning on moving or adding any of the following (select all that apply)?",
                    EstimateCategory.BigTicket,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Toilet drain location", AnswerChoiceType.MultipleSelection), // $700A
                        new AnswerChoice("Sinks", AnswerChoiceType.MultipleSelection), // $450A 
                        new AnswerChoice("Shower supply lines", AnswerChoiceType.MultipleSelection), // $800
                        new AnswerChoice("Shower/ tub drain location", AnswerChoiceType.MultipleSelection),
                        // $1000 - all should be each
                        new AnswerChoice("No, I'm keeping it as it is")
                    }),
                new Question(34, "Does the roof need any work?", EstimateCategory.Roof,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Full replacement"), // 2.6*sqft of house plus garage (q101)A
                        new AnswerChoice("Some shingle repair only", AnswerChoiceType.SingleSelection, 1), // $575A
                        new AnswerChoice("Chemical roof wash", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("No work needed", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1)
                    }),
                new Question(205, "Does it also need new decking underneath the shingles?", EstimateCategory.Roof,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No"),
                        new AnswerChoice("Some"),
                        new AnswerChoice("I don't know")
                    }
                    ),
                new Question(35, "Does the siding, soffit, or fascia need replacement?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 2),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 2)
                    }),
                new Question(36, "How much of the house is covered in siding, soffit, and/or fascia?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("25% or less"),
                        new AnswerChoice("50%"),
                        new AnswerChoice("75%"),
                        new AnswerChoice("100%")
                    }),
                new Question(111, "Regarding that amount of siding, soffit or fascia- how much needs to be replaced?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Small repair"),
                        new AnswerChoice("25%"),
                        new AnswerChoice("50%"),
                        new AnswerChoice("75%"),
                        new AnswerChoice("100%"), // Add to 6 multiplier
                        new AnswerChoice("I don't know")
                    }),
                new Question(37, "Does the brick need replacement?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 3),
                        new AnswerChoice("Just some repair"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(38, "How much of the house is covered in brick?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("25%"),
                        new AnswerChoice("50%"),
                        new AnswerChoice("75%"),
                        new AnswerChoice("100%"),
                        new AnswerChoice("I don't know")
                    }),
                new Question(115, "Regarding that amount of brick, how much needs replaced?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("25%"),
                        new AnswerChoice("50%"),
                        new AnswerChoice("75%"),
                        new AnswerChoice("100%"), // Add to 6 multiplier
                        new AnswerChoice("No Replacement")
                    }),
                new Question(112, "How many sides of the house need brick repair?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("None"),
                        new AnswerChoice("1"), // $500A
                        new AnswerChoice("2"), // $1000A
                        new AnswerChoice("3"), // $1500A
                        new AnswerChoice("4 or more"), // $2000A
                        new AnswerChoice("I don't know")
                    }),
                new Question(113,
                    "Is there a good portion of sheetrock completely missing in the house or are you planning on completely removing some?",
                    EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("Yes"),
                        new AnswerChoice("No", AnswerChoiceType.SingleSelection, 1),
                        new AnswerChoice("I don't know", AnswerChoiceType.SingleSelection, 1)
                    }),
                new Question(114, "How much sheetrock will you be removing or is missing now?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("25% or less"), // Add 2.5
                        new AnswerChoice("50%"), // Add 5
                        new AnswerChoice("75%"), // Add 7.5
                        new AnswerChoice("100%"), // Add 10
                        new AnswerChoice("I don't know") // Add 5
                    }),
                new Question(302, "How much trash was left by the previous owner?", EstimateCategory.None,
                    new List<AnswerChoice>
                    {
                        new AnswerChoice("12 cu yd (pile the size of a Mini Cooper)"),
                        new AnswerChoice("20 cu yd (pile the size of a VW bus)"),
                        new AnswerChoice("40 cu yd"),
                        new AnswerChoice("Hoarder house"),
                        new AnswerChoice("Little to none"),
                        new AnswerChoice("I don't know")
                    })
            };

            foreach (var question in QList)
            {
                Debug.WriteLine($"{QList.IndexOf(question)}| {question.Id}| {question.Text}");
            }
        }

        internal Question Q(int qNumber)
        {
            return QList.FirstOrDefault(q => q.Id == qNumber);
        }

        public List<Question> QList { get; }

        public Question this[int index] => QList[index];

		public void Finish()
		{
			foreach (var q in QList)
			{
				q.CleanAnswer();
			}
		}
    }
}
