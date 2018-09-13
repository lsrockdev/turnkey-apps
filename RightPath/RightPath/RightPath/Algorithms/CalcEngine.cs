using System;
using System.Linq;
using RightPath.Enums;

namespace RightPath.Algorithms
{
    public class CalcEngine
    {
        private readonly Questions _questions;

        public CalcEngine(Questions questions)
        {
            _questions = questions;
        }

        private double Tonnage => Math.Max(SquareFeet / 450.0, 2.5);
        private double SquareFeet => _questions.Q(1).NumericalValue;

        public double[] GetResults(Questions questions)
        {
            var BasicRate = 0d;
            var upgradesRate = 0d;
            var fixtureUpgradesRate = 0d;

            //var flooringUpgrades = 0d;

			//10000
			var marketFactor = MarketFactorService.GetMarketFactor(questions.Q(10000).NumericalValue.ToString());

            if (SquareFeet < 1000)
            {
                BasicRate += 2;
            }
            else if (SquareFeet >= 1000 && SquareFeet <= 1500)
            {
                BasicRate += 1;
            }

            // 2 - see below
            // 101 - see below

            // 42 - odors
            if (questions.Q(42).Choices[0].IsSelected)
            {
                BasicRate += 2;
            }

            // 43 - sheetrock
            if (questions.Q(43).Choices[0].IsSelected)
            {
                BasicRate += 1;
            }
            else if (questions.Q(43).Choices[1].IsSelected)
            {
                BasicRate += 1.5;
            }
            else if (questions.Q(43).Choices[2].IsSelected)
            {
                BasicRate += 2;
            }

            // 44 - broken items
            if (!questions.Q(44).Choices[4].IsSelected && !questions.Q(44).Choices[5].IsSelected)
            {
                BasicRate +=
                    questions.Q(44).Choices
                        .Count(choice => choice.Type == AnswerChoiceType.MultipleSelection && choice.IsSelected)*0.75;
            }

            // 5 flow - [9]

            #region premiums setup

            // 16 - PREMIUMS
            double flooringPremium, fixturePremium, cabinetPremium, countertopPremium, appliancePremium;
            if (questions.Q(16).Choices[0].IsSelected)
            {
                flooringPremium = 0.0;
                fixturePremium = 0.0;
                cabinetPremium = 0.0;
                countertopPremium = 0;
                appliancePremium = 0;
            }
            else if (questions.Q(16).Choices[1].IsSelected)
            {
                flooringPremium = 0.50;
                fixturePremium = 0.0;
                cabinetPremium = 0.0;
                countertopPremium = 1;
                appliancePremium = 0;
            }
            else if (questions.Q(16).Choices[2].IsSelected)
            {
                flooringPremium = 1.0;
                fixturePremium = 0.25;
                cabinetPremium = 0.75;
                countertopPremium = 1;
                appliancePremium = 0;
            }
            else if (questions.Q(16).Choices[3].IsSelected)
            {
                flooringPremium = 1.5;
                fixturePremium = 0.25;
                cabinetPremium = 1;
                countertopPremium = 1;
                appliancePremium = 0;
            }
            else if (questions.Q(16).Choices[4].IsSelected)
            {
                flooringPremium = 1.5;
                fixturePremium = 0.5;
                cabinetPremium = 1.5;
                countertopPremium = 1.0;
                appliancePremium = 0.5;
            }
            else if (questions.Q(16).Choices[5].IsSelected)
            {
                flooringPremium = 2.5;
                fixturePremium = 0.75;
                cabinetPremium = 2.0;
                countertopPremium = 1.5;
                appliancePremium = 0.5;
            }
            else if (questions.Q(16).Choices[6].IsSelected)
            {
                flooringPremium = 3.5;
                fixturePremium = 1.0;
                cabinetPremium = 2.0;
                countertopPremium = 2.0;
                appliancePremium = 1.0;
            }
            else if (questions.Q(16).Choices[7].IsSelected)
            {
                flooringPremium = 3.5;
                fixturePremium = 1.5;
                cabinetPremium = 2.5;
                countertopPremium = 3;
                appliancePremium = 2;
            }
            else
            {
                flooringPremium = 4.5;
                fixturePremium = 1.5;
                cabinetPremium = 3.0;
                countertopPremium = 3.5;
                appliancePremium = 3.0;
            }

            #endregion premiums setup

            // 7 - wet area floor
            if (questions.Q(7).Choices[0].IsSelected)
            {
                BasicRate += 0.5;
                upgradesRate += 1/8d*flooringPremium;

                //flooringUpgrades += (0.5 + 1 / 8d * flooringPremium) * SquareFeet;
            }

            // 201
            if (questions.Q(201).Choices[0].IsSelected)
            {
                BasicRate += 0.2;
                upgradesRate += 1/16d*flooringPremium;

                //flooringUpgrades += (0.2 + 1 / 16d * flooringPremium) * SquareFeet;
 
            }
            else if (questions.Q(201).Choices[2].IsSelected)
            {
                BasicRate += 0.2;
                upgradesRate += 1 / 16d * flooringPremium;

                //flooringUpgrades += (0.2 + 1 / 16d * flooringPremium) * SquareFeet;
                //var BathroomFloorReplacementCount = questions.Q(300).NumericalValue;
                //BasicRate += 0.2/BathroomFloorReplacementCount;
                //upgradesRate += 1/16d*flooringPremium/BathroomFloorReplacementCount;
            }

            // 202
            if (questions.Q(202).Choices[0].IsSelected)
            {
                BasicRate += 0.05;
                upgradesRate += 1/48d*flooringPremium;

                //flooringUpgrades += (0.05 + 1 / 48d * flooringPremium) * SquareFeet;
            }

            // 8 - living area floor
            if (!questions.Q(8).Choices[3].IsSelected)
            {
                if (questions.Q(8).Choices[0].IsSelected)
                {
                    var mlPremium = flooringPremium;

                    if (questions.Q(210).Choices[0].IsSelected)
                    {
                        mlPremium /= 2;
                    }

                    BasicRate += 0.65;
                    upgradesRate += 7/16d*mlPremium;

                    //flooringUpgrades += (0.65 + 7 / 16d * flooringPremium) * SquareFeet;
                }

                if (questions.Q(8).Choices[1].IsSelected)
                {
                    BasicRate += 0.125;
                    upgradesRate += 1/8d*flooringPremium;

                    //flooringUpgrades += (0.125 + 1 / 8d * flooringPremium) * SquareFeet;
                }

                if (questions.Q(8).Choices[2].IsSelected)
                {
                    BasicRate += 0.125;
                    upgradesRate += 1/8d*flooringPremium;

                    //flooringUpgrades += (0.125 + 1 / 8d * flooringPremium) * SquareFeet;
                }
            }

            // 9 - bedroom floor
            if (!questions.Q(9).Choices[2].IsSelected)
            {
                if (questions.Q(9).Choices[0].IsSelected)
                {
                    BasicRate += 0.15;
                    upgradesRate += 1/8d*flooringPremium;

                    //flooringUpgrades += (0.15 + 1 / 8d * flooringPremium) * SquareFeet;
                }

                if (questions.Q(9).Choices[1].IsSelected)
                {
                    var mlPremium = flooringPremium;

                    if (questions.Q(210).Choices[0].IsSelected)
                    {
                        mlPremium /= 2;
                    }

                    BasicRate += 0.25;
                    upgradesRate += 1/16d*mlPremium;

                    //flooringUpgrades += (0.25 + 1 / 16d * flooringPremium) * SquareFeet;
                }
            }

            // 102
            var numBathrooms = questions.Q(102).NumericalValue;

            var surrounds = 0d;
            // 10 - shower
            if (questions.Q(10).Choices[0].IsSelected)
            {
                surrounds = questions.Q(200).NumericalValue * 1000;
                //flooringUpgrades += surrounds;
            }

            // 11 - plumbing fixtures
            if (!questions.Q(11).Choices[4].IsSelected)
            {
                if (questions.Q(11).Choices[0].IsSelected)
                {
                    BasicRate += 0.125;
                    fixtureUpgradesRate += 1/3d*(1/4d)*fixturePremium;
                }

                if (questions.Q(11).Choices[1].IsSelected)
                {
                    BasicRate += 0.125;
                    fixtureUpgradesRate += 1/3d*(1/4d)*fixturePremium;
                }

                if (questions.Q(11).Choices[2].IsSelected)
                {
                    BasicRate += 0.125;
                    fixtureUpgradesRate += 1/3d*(1/4d)*fixturePremium;
                }

                if (questions.Q(11).Choices[3].IsSelected)
                {
                    BasicRate += 0.125;
                    fixtureUpgradesRate += 1/3d*(1/4d)*fixturePremium;
                }
                if (questions.Q(11).Choices[4].IsSelected)
                {
                    BasicRate += 0.125;
                    fixtureUpgradesRate += 1 / 3d * (1 / 4d) * fixturePremium;
                }

            }

            // 12 - outlets
            if (questions.Q(12).Choices[0].IsSelected)
            {
                BasicRate += 0.5;
            }
            else if (questions.Q(12).Choices[2].IsSelected)
            {
                BasicRate += 0.25;
            }

            // 45 - light fixtures
            if (questions.Q(45).Choices[0].IsSelected)
            {
                BasicRate += 1;
                fixtureUpgradesRate += 2/3d*fixturePremium;
            }
            else if (questions.Q(45).Choices[2].IsSelected)
            {
                BasicRate += 0.75;
                fixtureUpgradesRate += 2/3d*(0.75)*fixturePremium;
            }

            // 13 - countertops
            var countertops = 0d;
            var countertopsUpgrade = 0d;
            if (!questions.Q(13).Choices[4].IsSelected)
            {
                if (questions.Q(13).Choices[0].IsSelected)
                {
                    countertops += 1000;
                    countertopsUpgrade += 1500*countertopPremium;
                }

                if (questions.Q(13).Choices[1].IsSelected)
                {
                    countertops += 300;
                    countertopsUpgrade += 300*countertopPremium;
                }

                if (questions.Q(13).Choices[2].IsSelected && numBathrooms > 0)
                {
                    countertops += 200*(numBathrooms - 1);
                    countertopsUpgrade += 200*countertopPremium*(numBathrooms - 1);
                }

                if (questions.Q(13).Choices[3].IsSelected)
                {
                    countertops += 200;
                    countertopsUpgrade += 200*countertopPremium;
                }
            }

            // 14 - cabinets
            var cabinets = 0d;
            var cabinetsUpgrade = 0d;
            if (!questions.Q(14).Choices[5].IsSelected)
            {
                if (questions.Q(14).Choices[0].IsSelected)
                {
                    cabinets += 3000;
                    cabinetsUpgrade += 1000*cabinetPremium;
                }

                if (questions.Q(14).Choices[1].IsSelected)
                {
                    cabinets += 500;
                    cabinetsUpgrade += 200*cabinetPremium;
                }

                if (questions.Q(14).Choices[2].IsSelected && numBathrooms > 0)
                {
                    cabinets += 300 * (numBathrooms - 1);
                    cabinetsUpgrade += 200*cabinetPremium*(numBathrooms - 1);
                }

                if (questions.Q(14).Choices[3].IsSelected)
                {
                    cabinets += 200;
                    cabinetsUpgrade += 200*cabinetPremium;
                }
                if (questions.Q(14).Choices[4].IsSelected)
                {
                    cabinets += 3000;
                    cabinetsUpgrade += 1000 * cabinetPremium;
                }
            }

            // 103 - appliances
            var appliances = 0d;
            var appliancesUpgrade = 0d;
            if (questions.Q(103).Choices[0].IsSelected)
            {
                appliances += 2000;
                appliancesUpgrade += 750*appliancePremium;
            }
            else if (questions.Q(103).Choices[2].IsSelected)
            {
                appliances += 1500;
                appliancesUpgrade += 375*appliancePremium;
            }

            // 104 - ext paint
            if (questions.Q(104).Choices[0].IsSelected)
            {
                BasicRate += 0.75;

            }
            else if (questions.Q(104).Choices[2].IsSelected)
            {
                BasicRate += 0.38;
            }
            else if (questions.Q(104).Choices[3].IsSelected)
            {
                BasicRate += 0.38;
            }

            // 105 - int paint
            if (questions.Q(105).Choices[0].IsSelected)
            {
                BasicRate += 1.75;
            }
            else if (questions.Q(105).Choices[2].IsSelected )
            {
                if (questions.Q(43).Choices[0].IsSelected)
                {
                    BasicRate += 0.65;
                }
                else if (questions.Q(43).Choices[1].IsSelected)
                {
                    BasicRate += 1.05;
                }
                else if (questions.Q(43).Choices[2].IsSelected)
                {
                    BasicRate += 1.45;
                }
                else if (questions.Q(43).Choices[3].IsSelected)
                {
                    BasicRate += 1.85;
                }
            }
            else if (questions.Q(105).Choices[3].IsSelected)
            {
                if (questions.Q(43).Choices[0].IsSelected)
                {
                    BasicRate += 0.65;
                }
                else if (questions.Q(43).Choices[1].IsSelected)
                {
                    BasicRate += 1.05;
                }
                else if (questions.Q(43).Choices[2].IsSelected)
                {
                    BasicRate += 1.45;
                }
                else if (questions.Q(43).Choices[3].IsSelected)
                {
                    BasicRate += 1.85;
                }
            }


            // 15 - can be lived in
            //if (questions.Q(15).Choices[1].IsSelected)
            //{
            //    basicRate += 1.0;
            //}

            var foundation = 0.0;
            // 17
            if (questions.Q(17).Choices[0].IsSelected || questions.Q(17).Choices[2].IsSelected)
            {
                var foundationBase = 0.0;
                // 18
                if (questions.Q(18).Choices[0].IsSelected)
                {
                    // 19
                    if (questions.Q(19).Choices[0].IsSelected)
                    {
                        foundation = foundationBase = 1500.0;
                    }
                    else if (questions.Q(19).Choices[1].IsSelected)
                    {
                        foundation = foundationBase = 2000.0;
                    }
                    else
                    {
                        foundation = foundationBase = 2500.0;
                    }
                }

                // 203
                if (questions.Q(203).Choices[0].IsSelected)
                {
                    // 204
                    if (questions.Q(204).Choices[0].IsSelected)
                    {
                        foundation += 1000;
                    }
                    else if (questions.Q(204).Choices[1].IsSelected)
                    {
                        foundation += 1500;
                    }
                    else
                    {
                        foundation += 2000;
                    }
                }

                // 20
                if (questions.Q(20).Choices[0].IsSelected)
                {
                    // 204
                    if (questions.Q(500).Choices[1].IsSelected)
                    {
                        foundation += 100;
                    }
                    else if (questions.Q(500).Choices[2].IsSelected)
                    {
                        foundation += 200;
                    }
                    else if (questions.Q(500).Choices[3].IsSelected)
                    {
                        foundation += 300;
                    }
                    else if (questions.Q(500).Choices[4].IsSelected)
                    {
                        foundation += 400;
                    }
                }


                // 20
                if (questions.Q(20).Choices[0].IsSelected)
                {
                    if (questions.Q(18).Choices[0].IsSelected)
                    {
                        foundation += foundationBase*2;
                    }
                    else
                    {
                        foundation += 1500.0;
                    }
                }

                //// 21
                if (questions.Q(21).Choices[1].IsSelected)
                {
                    foundation += 2000;
                }
                else if (questions.Q(21).Choices[2].IsSelected)
                {
                    foundation += 4000;
                }
                else if (questions.Q(21).Choices[3].IsSelected)
                {
                    foundation += 6000;
                }
                else if (questions.Q(21).Choices[4].IsSelected)
                {
                    foundation += 8000;
                }

                //// 21
                //if (questions.Q(21).Choices[0].IsSelected)
                //{
                //    if (questions.Q(18).Choices[0].IsSelected)
                //    {
                //        foundation += foundationBase*2;
                //    }
                //    else
                //    {
                //        foundation += 1500.0;
                //    }
                //}
            }

            var pipes = 0d;
            // 22 - re-piped
            if (questions.Q(22).Choices[0].IsSelected)
            {
                pipes = 3 * SquareFeet;
            }
            //else if (questions.Q(22).Choices[2].IsSelected)
            //{
            //    pipes = 1000;
            //}
            else if (questions.Q(22).Choices[2].IsSelected)
            {
                pipes = 1500;
            }

            var hvacPremium = 0.0;
            // 23
            if (questions.Q(23).Choices[0].IsSelected)
            {
                // 24
                if (questions.Q(24).Choices[0].IsSelected)
                {
                    hvacPremium = 1300.0*Tonnage;
                }
                else if (questions.Q(24).Choices[1].IsSelected)
                {
                    hvacPremium = 1600.0*Tonnage;
                }
                else if (questions.Q(24).Choices[2].IsSelected)
                {
                    hvacPremium = 1800.0*Tonnage;
                }
                else
                {
                    // 25
                    if (questions.Q(25).Choices[0].IsSelected)
                    {
                        hvacPremium += 520.0*Tonnage;
                    }

                    // 26
                    if (questions.Q(26).Choices[0].IsSelected)
                    {
                        hvacPremium += 720.0*Tonnage;
                    }

                    // 27
                    if (questions.Q(27).Choices[0].IsSelected)
                    {
                        hvacPremium += 300.0*Tonnage;
                    }
                }
            }
            else if (questions.Q(23).Choices[2].IsSelected)
            {
                hvacPremium = 200.0;
            }

            var electricalPremium = 0.0;
            // 28 - rough electrical
            if (questions.Q(28).Choices[0].IsSelected || questions.Q(28).Choices[2].IsSelected)
            {
                // 29
                if (questions.Q(29).Choices[0].IsSelected)
                {
                    electricalPremium += 3.0*SquareFeet;
                }
                else if (questions.Q(29).Choices[3].IsSelected)
                {
                    electricalPremium += 1100;
                }

                // 30
                if (questions.Q(30).Choices[0].IsSelected)
                {
                    electricalPremium += 2000.0;
                }
            }
            
            // 31 - flow

            // 110 - wall removal
            var rooms = 0d;
            if (!questions.Q(110).Choices[8].IsSelected)
            {
                if (questions.Q(110).Choices[0].IsSelected)
                {
                    rooms += 1500;
                }

                if (questions.Q(110).Choices[1].IsSelected)
                {
                    rooms += 800;
                }

                if (questions.Q(110).Choices[2].IsSelected)
                {
                    rooms += 1000;
                }

                if (questions.Q(110).Choices[3].IsSelected)
                {
                    rooms += 1000;
                }

                if (questions.Q(110).Choices[4].IsSelected)
                {
                    rooms += 500;
                }

                if (questions.Q(110).Choices[5].IsSelected)
                {
                    rooms += 1200;
                }

                if (questions.Q(110).Choices[6].IsSelected)
                {
                    rooms += 800;
                }

                if (questions.Q(110).Choices[7].IsSelected)
                {
                    rooms += 800;
                }
            }

            // 301 - wall addition
            if (!questions.Q(301).Choices[8].IsSelected)
            {
                if (questions.Q(301).Choices[0].IsSelected)
                {
                    rooms += 750;
                }

                if (questions.Q(301).Choices[1].IsSelected)
                {
                    rooms += 400;
                }

                if (questions.Q(301).Choices[2].IsSelected)
                {
                    rooms += 500;
                }

                if (questions.Q(301).Choices[3].IsSelected)
                {
                    rooms += 500;
                }

                if (questions.Q(301).Choices[4].IsSelected)
                {
                    rooms += 250;
                }

                if (questions.Q(301).Choices[5].IsSelected)
                {
                    rooms += 600;
                }

                if (questions.Q(301).Choices[6].IsSelected)
                {
                    rooms += 400;
                }

                if (questions.Q(301).Choices[7].IsSelected)
                {
                    rooms += 400;
                }
            }

            // 33
            var moves = 0d;
            if (!questions.Q(33).Choices[4].IsSelected)
            {
                moves += questions.Q(33).Choices[0].Value*700d;
                moves += questions.Q(33).Choices[1].Value * 450d;
                moves += questions.Q(33).Choices[2].Value * 800d;
                moves += questions.Q(33).Choices[3].Value * 1000d;

                //if (questions.Q(33).Choices[0].IsSelected)
                //{
                //    moves += 700d;
                //}

                //if (questions.Q(33).Choices[1].IsSelected)
                //{
                //    moves += 450d;
                //}

                //if (questions.Q(33).Choices[2].IsSelected)
                //{
                //    moves += 800d;
                //}

                //if (questions.Q(33).Choices[3].IsSelected)
                //{
                //    moves += 1000d;
                //}
            }

            // 34
            var roof = 0d;
            if (questions.Q(34).Choices[0].IsSelected)
            {
                var roofSqft = SquareFeet;

                if (questions.Q(101).Choices[0].IsSelected)
                {
                    roofSqft += 200;
                }
                //else if (questions.Q(101).Choices[1].IsSelected || questions.Q(101).Choices[4].IsSelected)
                //{
                //    roofSqft += 400;
                //}
                //else if (questions.Q(101).Choices[2].IsSelected)
                //{
                //    roofSqft += 600;
                //}

                if (questions.Q(2).Choices[0].IsSelected)
                {
                    roof = 2.9 * roofSqft;
                }
                else if (questions.Q(2).Choices[1].IsSelected)
                {
                    roof = 0.6 * 3.5 * roofSqft;
                }

                // 205
                if (questions.Q(205).Choices[0].IsSelected)
                {
                    roof += 1.3*roofSqft;
                }
                else if (questions.Q(205).Choices[2].IsSelected)
                {
                    roof += 0.75 * roofSqft;
                }
            }
            else if (questions.Q(34).Choices[1].IsSelected)
            {
                roof = 1000;
            }
            else if (questions.Q(34).Choices[2].IsSelected)
            {
                roof = 800;
            }

            else if (questions.Q(34).Choices[3].IsSelected)
            {
                roof = 1800;
            }


            var raritiesRate = 0.0;
            var exteriorTrim = 0.0;
            // 35
            if (questions.Q(35).Choices[0].IsSelected)
            {
                // 36
                double coverage;
                if (questions.Q(36).Choices[0].IsSelected)
                {
                    coverage = 0.25;
                }
                else if (questions.Q(36).Choices[0].IsSelected)
                {
                    coverage = 0.5;
                }
                else if (questions.Q(36).Choices[0].IsSelected)
                {
                    coverage = 0.75;
                }
                else
                {
                    coverage = 1;
                }

                // 111
                double repl;
                if (questions.Q(111).Choices[0].IsSelected)
                {
                    repl = 0.1;
                }
                else if (questions.Q(111).Choices[1].IsSelected)
                {
                    repl = 0.25;
                }
                else if (questions.Q(111).Choices[2].IsSelected || questions.Q(111).Choices[5].IsSelected)
                {
                    repl = 0.50;
                }
                else if (questions.Q(111).Choices[3].IsSelected)
                {
                    repl = 0.75;
                }
                else
                {
                    repl = 1;
                }

                raritiesRate += 6*coverage*repl;
                exteriorTrim = 6 * coverage * repl * SquareFeet;
            }

            var brickWork = 0.0;
            // 37
            if (questions.Q(37).Choices[0].IsSelected)
            {
                // 38
                double coverage;
                if (questions.Q(38).Choices[0].IsSelected)
                {
                    coverage = 0.25;
                }
                else if (questions.Q(38).Choices[1].IsSelected || questions.Q(38).Choices[4].IsSelected)
                {
                    coverage = 0.5;
                }
                else if (questions.Q(38).Choices[2].IsSelected)
                {
                    coverage = 0.75;
                }
                else
                {
                    coverage = 1;
                }

                // 115
                double repl = 0;
                if (questions.Q(115).Choices[0].IsSelected)
                {
                    repl = 0.25;
                }
                else if (questions.Q(115).Choices[1].IsSelected)
                {
                    repl = 0.5;
                }
                else if (questions.Q(115).Choices[2].IsSelected)
                {
                    repl = 0.75;
                }
                else if (questions.Q(115).Choices[3].IsSelected)
                {
                    repl = 1;
                }

                raritiesRate += 9 * coverage * repl;
                brickWork = 9 * coverage * repl * SquareFeet;
            }

            // 112
            var brickRepair = 0d;
            if (questions.Q(112).Choices[1].IsSelected)
            {
                brickRepair += 500;
            }
            else if (questions.Q(112).Choices[2].IsSelected || questions.Q(112).Choices[5].IsSelected)
            {
                brickRepair += 1000;
            }
            else if (questions.Q(112).Choices[3].IsSelected)
            {
                brickRepair += 1500;
            }
            else if (questions.Q(112).Choices[4].IsSelected)
            {
                brickRepair += 2000;
            }
            brickWork += brickRepair;
            // 113 - flow

            var sheetTrock = 0.0;
            // 114
            if (questions.Q(114).Choices[0].IsSelected)
            {
                raritiesRate += 2.5;
                sheetTrock = 2.5 * SquareFeet;
            }
            else if (questions.Q(114).Choices[1].IsSelected|| questions.Q(114).Choices[4].IsSelected)
            {
                raritiesRate += 5;
                sheetTrock = 5 * SquareFeet;

            }
            else if (questions.Q(114).Choices[2].IsSelected)
            {
                raritiesRate += 7.5;
                sheetTrock = 7.5 * SquareFeet;

            }
            else if (questions.Q(114).Choices[3].IsSelected)
            {
                raritiesRate += 10;
                sheetTrock = 10 * SquareFeet;

            }

            // 302
            var windows = 0d;
            if (questions.Q(302).Choices[0].IsSelected)
            {
                windows = 400;
            }
            else if (questions.Q(302).Choices[1].IsSelected)
            {
                windows = 600;
            }
            else if (questions.Q(302).Choices[2].IsSelected)
            {
                windows = 1200;
            }
            else if (questions.Q(302).Choices[3].IsSelected)
            {
                windows = 2500;
            }

            if (!questions.Q(1001).Choices[2].IsSelected)
            {
                moves += questions.Q(1001).Choices[0].Value * 450d;
                moves += questions.Q(1001).Choices[1].Value * 200d;
            }


            var landscaping = 0.0;

            if (questions.Q(1002).Choices[0].IsSelected)
            {
                landscaping += 200d;
            }
            else if (questions.Q(1002).Choices[1].IsSelected)
            {
                landscaping += 800d;
            }
            else if (questions.Q(1002).Choices[2].IsSelected)
            {
                landscaping += 2000d;
            }
            else if (questions.Q(1002).Choices[3].IsSelected)
            {
                landscaping += 4000d;
            }

            var basicResult = BasicRate * SquareFeet + countertops + cabinets + surrounds + appliances;
            //var raritiesResult = raritiesRate*SquareFeet + rooms + brickRepair + moves + trash;
            var raritiesResult = rooms + sheetTrock + exteriorTrim + brickWork + landscaping + windows;

            //var raritiesResult = moves/marketFactor;

            var upgradesResult = upgradesRate*SquareFeet +fixtureUpgradesRate*SquareFeet + cabinetsUpgrade + countertopsUpgrade + appliancesUpgrade;
            var flooringUpgrades = upgradesRate * SquareFeet;
            var appearanceUpgrades = fixtureUpgradesRate * SquareFeet + countertopsUpgrade + appliancesUpgrade;
            return new[] { basicResult * marketFactor, upgradesResult * marketFactor, roof * marketFactor, foundation * marketFactor, hvacPremium * marketFactor, electricalPremium * marketFactor, pipes * marketFactor
                , raritiesResult * marketFactor,flooringUpgrades*marketFactor,cabinetsUpgrade*marketFactor,appearanceUpgrades*marketFactor
                ,rooms* marketFactor,sheetTrock* marketFactor,exteriorTrim * marketFactor,brickWork * marketFactor,landscaping*marketFactor,windows*marketFactor };
        }
    }
}