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

        public TipRepository()
        {
            _context = new AppDbContext();
            LoadTips();
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
            return _context.Tips.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
        }

        public IEnumerable<Tip> GetAllTips()
        {
            return _context.Tips.ToList();
        }

        public void SaveChanges()
        {
            // Save to JSON file
            string jsonData = JsonConvert.SerializeObject(tips, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);

            // Save changes to the database
            _context.SaveChanges();
        }

        public void LoadTips()
        {
            // Load from JSON file
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                tips = JsonConvert.DeserializeObject<List<Tip>>(jsonData) ?? new List<Tip>();

                // Ensure that the database is in sync with the JSON file
                foreach (var tip in tips)
                {
                    if (!_context.Tips.Any(t => t.Id == tip.Id))
                    {
                        _context.Tips.Add(tip);
                    }
                }
                _context.SaveChanges();
            }
            else
            {
                tips = _context.Tips.ToList();
            }
        }
    }
}
