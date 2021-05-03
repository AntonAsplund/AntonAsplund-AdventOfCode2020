using System;
using System.Collections.Generic;

namespace starter_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
			//Day1AdventOfCode day1AdventOfCode = new Day1AdventOfCode();
			//day1AdventOfCode.Run();

			//Day2AdventOfCode day2AdventOfCode = new Day2AdventOfCode();
			//day2AdventOfCode.Run();

			//Day3AdventOfCode day3AdventOfCode = new Day3AdventOfCode();
			//day3AdventOfCode.Run();

			Console.ReadKey();
        }
    }

    class Day1AdventOfCode
    {
        public Day1AdventOfCode()
        {

        }
        public void Run()
        {

            #region solution

            List<int> listOfNumbers = new List<int>();
            bool numbersLeftToRead = true;

            double resultPartOne = 0;

            while (numbersLeftToRead)
            {
                int number = 0;
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    listOfNumbers.Add(number);
                }
                else
                {
                    numbersLeftToRead = false;
                }

            }

            for (int i = 0; i < listOfNumbers.Count; i++)
            {
                for (int y = 1; y < listOfNumbers.Count; y++)
                {
                    if (listOfNumbers[i] + listOfNumbers[y] == 2020 && i != y)
                    {
                        resultPartOne = listOfNumbers[i] * listOfNumbers[y];
                    }
                }
            }

            double resultPartTwo = 0;

            for (int i = 0; i < listOfNumbers.Count; i++)
            {
                for (int y = 1; y < listOfNumbers.Count; y++)
                {
                    for (int x = 1; x < listOfNumbers.Count; x++)
                    {

                        bool notTheSameNumbers = i != y && i != x && x != y;

                        if (listOfNumbers[i] + listOfNumbers[y] + listOfNumbers[x] == 2020 && notTheSameNumbers)
                        {
                            resultPartTwo = listOfNumbers[i] * listOfNumbers[y] * listOfNumbers[x];
                        }
                    }
                }
            }

            #endregion

            #region Result

            Console.WriteLine($"Result part one: {resultPartOne}");
            Console.WriteLine($"Result part two: {resultPartTwo}");

            #endregion
        }
    }

    class Day2AdventOfCode
    {
        public void Run()
        {
            #region solution

            int numberOfValidPasswordsInPartOne = 0;
            int numberOfValidPasswordsInPartTwo = 0;

            while (true)
            {
                string lineToAudit = Console.ReadLine();

                if (String.IsNullOrEmpty(lineToAudit)) { break; }

                string[] passwordInfo = lineToAudit.Split(new char[] { ' ' }, StringSplitOptions.None);

                string[] numberOfAllowedOccurancesInterval = passwordInfo[0].Split(new char[] { '-' }, StringSplitOptions.None);
                string keyLetter = passwordInfo[1].Remove(1);
                string passwordToSearchThrough = passwordInfo[2];

                int numberOfTimesLetterIsFound = 0;

                for (int i = 0; i < passwordToSearchThrough.Length; i++)
                {
                    bool letterFound = passwordToSearchThrough[i] == keyLetter.ToCharArray()[0];

                    if (letterFound)
                    {
                        numberOfTimesLetterIsFound++;
                    }
                }

                if (numberOfTimesLetterIsFound >= int.Parse(numberOfAllowedOccurancesInterval[0]) && numberOfTimesLetterIsFound <= int.Parse(numberOfAllowedOccurancesInterval[1]))
                {
                    numberOfValidPasswordsInPartOne++;
                }

                int firstIndexToLookAt = int.Parse(numberOfAllowedOccurancesInterval[0]);
                int secondIndexToLookAt = int.Parse(numberOfAllowedOccurancesInterval[1]);

                bool keyLetterIsAtFirstIndex = passwordToSearchThrough[firstIndexToLookAt - 1] == keyLetter.ToCharArray()[0];
                bool keyLetterIsAtSecondIndex = passwordToSearchThrough[secondIndexToLookAt - 1] == keyLetter.ToCharArray()[0];

                if ((keyLetterIsAtFirstIndex || keyLetterIsAtSecondIndex) && (keyLetterIsAtFirstIndex != keyLetterIsAtSecondIndex))
                {
                    numberOfValidPasswordsInPartTwo++;
                }

            }

            #endregion

            #region Result

            Console.WriteLine($"Number of correct passwords part one: {numberOfValidPasswordsInPartOne}");

            Console.WriteLine($"Number of correct passwords part two: {numberOfValidPasswordsInPartTwo}");

            #endregion
        }
    }
    class Day3AdventOfCode
    {
        public void Run()
        {
            #region solution

            List<string> firstRow = new List<string>();
            bool numbersLeftToRead = true;

            while (numbersLeftToRead)
            {
                string row = Console.ReadLine();

                if (!String.IsNullOrEmpty(row))
                {
                    firstRow.Add(row);
                }
                else
                {
                    numbersLeftToRead = false;
                }
            }


            int treesEncounteredPartOne = CalculateSlope(3, 1, firstRow);

            List<SlopeStepOption> allOptions = new List<SlopeStepOption>();

            allOptions.Add(new SlopeStepOption(1, 1));
            allOptions.Add(new SlopeStepOption(3, 1));
            allOptions.Add(new SlopeStepOption(5, 1));
            allOptions.Add(new SlopeStepOption(7, 1));
            allOptions.Add(new SlopeStepOption(1, 2));

            double numberOfTreesInAllOptions = 1;

            foreach (var option in allOptions)
            {
                int treesEncounteredInOption = CalculateSlope(option.StepsRight, option.StepsDown, firstRow);
                numberOfTreesInAllOptions = numberOfTreesInAllOptions * treesEncounteredInOption;
            }

            #endregion

            #region Result

            Console.WriteLine($"Number of trees encountered in part one: {treesEncounteredPartOne}");


            Console.WriteLine($"Number of trees encountered in part two: {numberOfTreesInAllOptions}");



            #endregion

        }

        public int CalculateSlope(int stepsRight, int stepsDown, List<string> firstRow)
        {

            int currentNumberOfStepsHorizontally = 0;
            int numberOfStepsInOneRow = firstRow[0].Length;

            int treesEncountered = 0;

            for (int height = stepsDown; height < firstRow.Count; height += stepsDown)
            {
                currentNumberOfStepsHorizontally = currentNumberOfStepsHorizontally + stepsRight;
                if (currentNumberOfStepsHorizontally >= numberOfStepsInOneRow)
                {
                    currentNumberOfStepsHorizontally = currentNumberOfStepsHorizontally - numberOfStepsInOneRow;
                    if (height == firstRow.Count)
                    {
                        break;
                    }
                }

                string currentRow = firstRow[height];

                if (currentRow[currentNumberOfStepsHorizontally] == '#')
                {
                    treesEncountered++;
                }

            }

            return treesEncountered;
        }
    }

    class SlopeStepOption
    {
        public int StepsRight { get; set; }
        public int StepsDown { get; set; }
        public double NumberOfTreesEncountered { get; set; }

        public SlopeStepOption(int stepsRight, int stepsDown)
        {
            this.StepsRight = stepsRight;
            this.StepsDown = stepsDown;
        }

    }


}