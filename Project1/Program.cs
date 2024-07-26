using System;
using System.Globalization;
using TipTracker.Models;
using TipTracker.Repositories;

namespace TipTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of TipRepository

            ITipRepository tipRepository = new TipRepository();

            while (true)
            {
                ShowMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddTip(tipRepository);
                        break;
                    case "2":
                        ViewTipsByDateRange(tipRepository);
                        break;
                    case "3":
                        ViewTipsByExactDate(tipRepository);
                        break;
                    case "4":
                        ShowStatistics(tipRepository);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, 3, 4, or 5.");
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Tip Tracker Menu:");
            Console.WriteLine("1. Add Tip");
            Console.WriteLine("2. View Tips by Date Range");
            Console.WriteLine("3. View Tips by Exact Date");
            Console.WriteLine("4. View Statistics");
            Console.WriteLine("5. Exit");
        }

        private static void AddTip(ITipRepository tipRepository)
        {
            Console.Clear();
            DateTime date = GetValidDate("Enter the date:");

            decimal amount;
            while (true)
            {
                Console.Write("Amount: ");
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    break;
                }
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }

            string shift;
            while (true)
            {
                Console.Write("Shift (morning/afternoon/evening): ");
                shift = Console.ReadLine();
                if (shift == "morning" || shift == "afternoon" || shift == "evening")
                {
                    break;
                }
                Console.WriteLine("Invalid shift. Please enter 'morning', 'afternoon', or 'evening'.");
            }

            // Create a new Tip object and add it to the repository
            Tip tip = new Tip { Date = date, Amount = amount, Shift = shift };
            tipRepository.AddTip(tip);
            tipRepository.SaveChanges();
            Console.WriteLine("Tip added successfully. Press any key to return to the main menu.");
            Console.ReadKey();
        }

        private static void ViewTipsByDateRange(ITipRepository tipRepository)
        {
            Console.Clear();
            DateTime startDate = GetValidDate("Enter the start date:");
            DateTime endDate = GetValidDate("Enter the end date:");

            var tips = tipRepository.GetTips(startDate, endDate);
            foreach (var tip in tips)
            {
                Console.WriteLine($"{tip.Date.ToShortDateString()} - {tip.Shift}: ${tip.Amount}");
            }
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }

        private static void ViewTipsByExactDate(ITipRepository tipRepository)
        {
            Console.Clear();
            DateTime date = GetValidDate("Enter the date:");

            var tips = tipRepository.GetTips(date, date);
            if (!tips.Any())
            {
                Console.WriteLine("No tips found for the specified date.");
            }
            else
            {
                foreach (var tip in tips)
                {
                    Console.WriteLine($"{tip.Date.ToShortDateString()} - {tip.Shift}: ${tip.Amount}");
                }
            }
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }


        private static DateTime GetValidDate(string prompt)
        {
            int year = 0, month = 0, day = 0;

            while (year < 1000 || year > 9999)
            {
                Console.WriteLine(prompt);
                Console.Write("Year (4 digits): ");
                if (!int.TryParse(Console.ReadLine(), out year) || year < 1000 || year > 9999)
                {
                    Console.WriteLine("Invalid year. Please enter a valid 4-digit year.");
                    year = 0;
                }
            }

            while (month < 1 || month > 12)
            {
                Console.Write("Month (1-12): ");
                if (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Invalid month. Please enter a valid month (1-12).");
                    month = 0;
                }
            }

            while (day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                Console.Write("Day: ");
                if (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > DateTime.DaysInMonth(year, month))
                {
                    Console.WriteLine("Invalid day. Please enter a valid day.");
                    day = 0;
                }
            }

            return new DateTime(year, month, day);
        }

        private static DateTime GetStartOfWeek(DateTime dt)
        {
            var diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private static void ShowStatistics(ITipRepository tipRepository)
        {
            var tips = tipRepository.GetAllTips();

            if (!tips.Any())
            {
                Console.WriteLine("No tips available for statistics.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Daily Averages");
                Console.WriteLine("2. Weekly Averages");
                Console.WriteLine("3. Monthly Averages");
                Console.WriteLine("4. Highest Tip");
                Console.WriteLine("5. Lowest Tip");
                Console.WriteLine("6. Back to Main Menu");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowDailyAverages(tips);
                        break;
                    case "2":
                        ShowWeeklyAverages(tips);
                        break;
                    case "3":
                        ShowMonthlyAverages(tips);
                        break;
                    case "4":
                        ShowHighestTip(tips);
                        break;
                    case "5":
                        ShowLowestTip(tips);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
                Console.WriteLine("Press any key to return to the statistics menu.");
                Console.ReadKey();
            }
        }

        private static void ShowDailyAverages(IEnumerable<Tip> tips)
        {
            ShowAverages(tips, t => t.Date, "Daily");
        }

        private static void ShowWeeklyAverages(IEnumerable<Tip> tips)
        {
            ShowAverages(tips, t => GetStartOfWeek(t.Date), "Weekly");
        }

        private static void ShowMonthlyAverages(IEnumerable<Tip> tips)
        {
            ShowAverages(tips, t => new DateTime(t.Date.Year, t.Date.Month, 1), "Monthly");
        }

        private static void ShowAverages(IEnumerable<Tip> tips, Func<Tip, DateTime> groupByFunc, string period)
        {
            
            // Group tips by the specified grouping function and calculate the average amount for each group

            var averages = tips.GroupBy(groupByFunc)
                               .Select(g => new { Period = g.Key, Average = g.Average(t => t.Amount) });

            Console.WriteLine($"{period} Averages:");
            foreach (var avg in averages)
            {
                Console.WriteLine($"{avg.Period.ToShortDateString()}: ${avg.Average:F2}");
            }
        }

        private static void ShowHighestTip(IEnumerable<Tip> tips)
        {
            var highestTip = tips.Max(t => t.Amount);
            Console.WriteLine($"Highest Tip: ${highestTip:F2}");
        }

        private static void ShowLowestTip(IEnumerable<Tip> tips)
        {
            var lowestTip = tips.Min(t => t.Amount);
            Console.WriteLine($"Lowest Tip: ${lowestTip:F2}");
        }
    }
}
