using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TipTracker.Data;
using TipTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace TipTracker.Repositories
{
    public class TipRepository : ITipRepository
    {
        private readonly AppDbContext _context;
        private List<Tip> tips;
        private string filePath = "tips.json";


        // Initialize the database context
        public TipRepository()
        {
            _context = new AppDbContext();
            LoadTips();

            // Load tips from JSON file
        }

        public void AddTip(Tip tip)
        {
            // Add to the list
            tips.Add(tip);

            // Add to the database
            _context.Tips.Add(tip);
        }

        public IEnumerable<Tip> GetTips(DateTime startDate, DateTime endDate)
        {
             // Query the database context to get tips where the date is within the specified range

            return _context.Tips.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
        }



        // Retrieve all tips from the database
        public IEnumerable<Tip> GetAllTips()
        {
            return _context.Tips.ToList();
        }


        public void SaveChanges()
        {
            // Save changes to the database
            _context.SaveChanges();

            // Update the JSON file with the latest data
            SynchronizeJson();
        }

        public void LoadTips()
        {
            if (File.Exists(filePath))
            {

                // Read data from the JSON file
                string jsonData = File.ReadAllText(filePath);

                // Deserialize JSON

                tips = JsonConvert.DeserializeObject<List<Tip>>(jsonData) ?? new List<Tip>();

                foreach (var jsonTip in tips)
                {
                      // Query the database to find if a tip with the same ID as the current jsonTip exists.
                      
                    var existingTip = _context.Tips.AsNoTracking().SingleOrDefault(t => t.Id == jsonTip.Id);
                    if (existingTip != null)
                    {
                        _context.Entry(existingTip).State = EntityState.Detached;
                        _context.Tips.Update(jsonTip);
                    }
                    else
                    {
                        // Add new tip to the database
                        _context.Tips.Add(jsonTip);
                    }
                }

                _context.SaveChanges(); // Save changes to the database
                SynchronizeJson();
            }
            else
            {

                // If JSON file doesn't exist, load all tips from the database
                tips = _context.Tips.ToList();
                SaveChanges();
            }
        }


        public void SynchronizeJson()
        {
            // Reload the tips from the database to ensure they are up-to-date
            tips = _context.Tips.OrderBy(t => t.Id).ThenBy(t => t.Date).ToList();
            string jsonData = JsonConvert.SerializeObject(tips, Formatting.Indented); // Serialize tips to JSON format
            File.WriteAllText(filePath, jsonData);
        }

    }
}
