using System;
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
                Console.WriteLine("1. Add Tip");
                Console.WriteLine("2. View Tips");
                Console.WriteLine("3. Exit");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Date (yyyy-mm-dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.Write("Amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    Console.Write("Shift (morning/afternoon/evening): ");
                    string shift = Console.ReadLine();
                    Tip tip = new Tip { Date = date, Amount = amount, Shift = shift };
                    tipRepository.AddTip(tip);
                    tipRepository.SaveChanges();
                }
                else if (choice == "2")
                {
                    Console.Write("Start Date (yyyy-mm-dd): ");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("End Date (yyyy-mm-dd): ");
                    DateTime endDate = DateTime.Parse(Console.ReadLine());
                    var tips = tipRepository.GetTips(startDate, endDate);
                    foreach (var tip in tips)
                    {
                        Console.WriteLine($"{tip.Date.ToShortDateString()} - {tip.Shift}: ${tip.Amount}");
                    }
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }
    }
}
