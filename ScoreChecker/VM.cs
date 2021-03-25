// James Odeyale - Group 1

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;


namespace ScoreChecker
{
    public enum Sections
    {
        Section1,
        Section2,
        Section3
    }

    class VM : INotifyPropertyChanged
    {
        const string FILENAME_SECTION1 = "Section1.txt";
        const string FILENAME_SECTION2 = "Section2.txt";
        const string FILENAME_SECTION3 = "Section3.txt";
        const int MAX_SCORE = 100;
        const int GRADE_MIN_A_SCORE = 80;
        const int GRADE_MIN_B_SCORE = 70;
        const int GRADE_MIN_C_SCORE = 60;
        const int GRADE_MIN_D_SCORE = 55;

        int[][] allScores = new int[3][];
        int loopTrackerToTriggerCalculation = 0;

        #region Properties
        public ObservableCollection<ExamScore> ExamScoreSection1 { get; set; } = new ObservableCollection<ExamScore>();
        public ObservableCollection<ExamScore> ExamScoreSection2 { get; set; } = new ObservableCollection<ExamScore>();
        public ObservableCollection<ExamScore> ExamScoreSection3 { get; set; } = new ObservableCollection<ExamScore>();

        private int highestSection1 = 0;
        public int HighestSection1
        {
            get => highestSection1;
            set { highestSection1 = value; propertyChanged(); }
        }

        private int highestSection2 = 0;
        public int HighestSection2
        {
            get => highestSection2;
            set { highestSection2 = value; propertyChanged(); }
        }

        private int highestSection3 = 0;
        public int HighestSection3
        {
            get => highestSection3;
            set { highestSection3 = value; propertyChanged(); }
        }

        private double averageScoreSection1 = 0d;
        public double AverageScoreSection1
        {
            get => averageScoreSection1;
            set { averageScoreSection1 = value; propertyChanged(); }
        }

        private double averageScoreSection2 = 0d;
        public double AverageScoreSection2
        {
            get => averageScoreSection2;
            set { averageScoreSection2 = value; propertyChanged(); }
        }

        private double averageScoreSection3 = 0d;
        public double AverageScoreSection3
        {
            get => averageScoreSection3;
            set { averageScoreSection3 = value; propertyChanged(); }
        }

        private double averageOfAllSection = 0d;
        public double AverageOfAllSection
        {
            get => averageOfAllSection;
            set { averageOfAllSection = value; propertyChanged(); }
        }

        private string highestScoreOfAllSection = "";
        public string HighestScoreOfAllSection
        {
            get => highestScoreOfAllSection;
            set { highestScoreOfAllSection = value; propertyChanged(); }
        }

        private string lowestScoreOfAllSection = "";
        public string LowestScoreOfAllSection
        {
            get => lowestScoreOfAllSection;
            set { lowestScoreOfAllSection = value; propertyChanged(); }
        }
        #endregion

        // Read data from files
        private void ReadSectionFiles(string fileName, Sections section)
        {
            string scoresFromSection = File.ReadAllText(fileName);
            string[] linesFromSection = scoresFromSection.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            AddToSection(section, linesFromSection);
        }

        // Adding scores to listbox
        private void AddToSection(Sections section, string[] scoreLines)
        {
            int[] sectionScores = new int[scoreLines.Length];
            int i = 0;
            foreach (string scoreLine in scoreLines)
            {
                string[] props = scoreLine.Split(new char[] { '-' });
                sectionScores[i++] = int.Parse(props[1].Trim());

                ExamScore examScore = new ExamScore
                {
                    Name = props[0].Trim(),
                    Score = int.Parse(props[1].Trim()),
                    Grade = DetermineGrade(int.Parse(props[1].Trim()))
                };

                switch (section)
                {
                    case Sections.Section1:
                        ExamScoreSection1.Add(examScore);
                        allScores[0] = sectionScores;
                        break;
                    case Sections.Section2:
                        ExamScoreSection2.Add(examScore);
                        allScores[1] = sectionScores;
                        break;
                    case Sections.Section3:
                        ExamScoreSection3.Add(examScore);
                        allScores[2] = sectionScores;
                        break;
                    default:
                        break;
                }
            }
            LoopChecker();
        }

        // Check if all scores from all sections have been compiled before running crunching function
        private void LoopChecker()
        {
            if (loopTrackerToTriggerCalculation > 1)
            {
                CrunchData();
            }
            loopTrackerToTriggerCalculation++;
        }

        // Crunch Data to get all the highest and averages
        private void CrunchData()
        {
            double totalScoreForAllSection = 0;
            double totalScoreCountForAllSection = 0;
            int highestScoreForAllSection = 0;
            int lowestScoreForAllSection = MAX_SCORE;
            string highestValueMessage = "";
            string lowestValueMessage = "";
            int noteForHighestSection = 0;
            int noteForLowestSection = 0;

            for (int i = 0; i < allScores.Length; i++)
            {
                Sections section = (Sections)i;
                int highestGrade = 0;
                double totalGrade = 0;
                int[] scores = allScores[i];

                for (int j = 0; j < scores.Length; j++)
                {
                    if (scores[j] > highestGrade)
                    {
                        highestGrade = scores[j];

                        if (highestGrade > highestScoreForAllSection)
                        {
                            highestScoreForAllSection = highestGrade;
                            noteForHighestSection = i;
                            highestValueMessage = $"{highestScoreForAllSection} found in Section {i + 1}";
                        }
                        else if (highestGrade == highestScoreForAllSection && noteForHighestSection != i)
                        {
                            noteForHighestSection = i;
                            highestValueMessage += $" and {noteForHighestSection + 1}";
                        }
                    }

                    if (scores[j] < lowestScoreForAllSection)
                    {
                        lowestScoreForAllSection = scores[j];
                        noteForLowestSection = i;
                        lowestValueMessage = $"{lowestScoreForAllSection} found in Section {i + 1}";
                    }
                    else if (scores[j] == lowestScoreForAllSection && noteForLowestSection != i)
                    {
                        noteForLowestSection = i;
                        lowestValueMessage += $" and {noteForLowestSection + 1}";
                    }

                    totalGrade += scores[j];
                }

                double averageScoreForSection = Math.Round(totalGrade / scores.Length, 2);

                totalScoreForAllSection += totalGrade;
                totalScoreCountForAllSection += scores.Length;

                switch (section)
                {
                    case Sections.Section1:
                        HighestSection1 = highestGrade;
                        AverageScoreSection1 = averageScoreForSection;
                        break;
                    case Sections.Section2:
                        HighestSection2 = highestGrade;
                        AverageScoreSection2 = averageScoreForSection;
                        break;
                    case Sections.Section3:
                        HighestSection3 = highestGrade;
                        AverageScoreSection3 = averageScoreForSection;
                        break;
                    default:
                        break;
                }
            }

            AverageOfAllSection = Math.Round(totalScoreForAllSection / totalScoreCountForAllSection, 2);
            HighestScoreOfAllSection = highestValueMessage;
            LowestScoreOfAllSection = lowestValueMessage;
        }

        // Get the appropriate grade
        private GradeSystem DetermineGrade(int score)
        {
            if (score >= GRADE_MIN_A_SCORE) return GradeSystem.A;
            else if (score >= GRADE_MIN_B_SCORE) return GradeSystem.B;
            else if (score >= GRADE_MIN_C_SCORE) return GradeSystem.C;
            else if (score >= GRADE_MIN_D_SCORE) return GradeSystem.D;
            return GradeSystem.F;
        }

        // main function
        public void Crunch()
        {
            Clear();
            ReadSectionFiles(FILENAME_SECTION1, Sections.Section1);
            ReadSectionFiles(FILENAME_SECTION2, Sections.Section2);
            ReadSectionFiles(FILENAME_SECTION3, Sections.Section3);
        }
        public void Clear()
        {
            HighestSection1 = 0;
            HighestSection2 = 0;
            HighestSection3 = 0;
            
            AverageScoreSection1 = 0;
            AverageScoreSection2 = 0;
            AverageScoreSection3 = 0;

            AverageOfAllSection = 0;
            HighestScoreOfAllSection = "";
            LowestScoreOfAllSection = "";

            ExamScoreSection1.Clear();
            ExamScoreSection2.Clear();
            ExamScoreSection3.Clear();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
