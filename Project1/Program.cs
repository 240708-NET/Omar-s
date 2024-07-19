using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TipTracker.Models;
using TipTracker.Repositories;

namespace TipTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            ITipRepository tipRepository = new TipRepository();
            tipRepository.LoadTips();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Add Tip");
                Console.WriteLine("2. View Tips by Date Range");
                Console.WriteLine("3. View Tips by Exact Date");
                Console.WriteLine("4. View Statistics");
                Console.WriteLine("5. Exit");
                string choice = Console.ReadLine();

                if (choice == "1")
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
                        else
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                        }
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
                        else
                        {
                            Console.WriteLine("Invalid shift. Please enter 'morning', 'afternoon', or 'evening'.");
                        }
                    }

                    Tip tip = new Tip { Date = date, Amount = amount, Shift = shift };
                    tipRepository.AddTip(tip);
                    tipRepository.SaveChanges();
                    Console.WriteLine("Tip added successfully. Press any key to return to the main menu.");
                    Console.ReadKey();
                }
                else if (choice == "2")
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
                else if (choice == "3")
                {
                    Console.Clear();
                    DateTime date = GetValidDate("Enter the date:");

                    var tips = tipRepository.GetTips(date, date);
                    foreach (var tip in tips)
                    {
                        Console.WriteLine($"{tip.Date.ToShortDateString()} - {tip.Shift}: ${tip.Amount}");
                    }
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey();
                }
                else if (choice == "4")
                {
                    Console.Clear();
                    ShowStatistics(tipRepository);
                }
                else if (choice == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select 1, 2, 3, 4, or 5.");
                }
            }
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

                if (choice == "1")
                {
                    var dailyAverage = tips.GroupBy(t => t.Date)
                                           .Select(g => new { Date = g.Key, Average = g.Average(t => t.Amount) });

                    Console.WriteLine("Daily Averages:");
                    foreach (var avg in dailyAverage)
                    {
                        Console.WriteLine($"{avg.Date.ToShortDateString()}: ${avg.Average:F2}");
                    }
                    Console.WriteLine("Press any key to return to the statistics menu.");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    var weeklyAverage = tips.GroupBy(t => new { Year = t.Date.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(t.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday) })
                                            .Select(g => new { Week = g.Key, Average = g.Average(t => t.Amount) });

                    Console.WriteLine("Weekly Averages:");
                    foreach (var avg in weeklyAverage)
                    {
                        Console.WriteLine($"Year {avg.Week.Year}, Week {avg.Week.Week}: ${avg.Average:F2}");
                    }
                    Console.WriteLine("Press any key to return to the statistics menu.");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    var monthlyAverage = tips.GroupBy(t => new { Year = t.Date.Year, Month = t.Date.Month })
                                             .Select(g => new { Month = g.Key, Average = g.Average(t => t.Amount) });

                    Console.WriteLine("Monthly Averages:");
                    foreach (var avg in monthlyAverage)
                    {
                        Console.WriteLine($"{avg.Month.Year}-{avg.Month.Month}: ${avg.Average:F2}");
                    }
                    Console.WriteLine("Press any key to return to the statistics menu.");
                    Console.ReadKey();
                }
                else if (choice == "4")
                {
                    var highestTip = tips.Max(t => t.Amount);
                    Console.WriteLine($"Highest Tip: ${highestTip:F2}");
                    Console.WriteLine("Press any key to return to the statistics menu.");
                    Console.ReadKey();
                }
                else if (choice == "5")
                {
                    var lowestTip = tips.Min(t => t.Amount);
                    Console.WriteLine($"Lowest Tip: ${lowestTip:F2}");
                    Console.WriteLine("Press any key to return to the statistics menu.");
                    Console.ReadKey();
                }
                else if (choice == "6")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
        }
    }
}
