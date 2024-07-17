using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TipTracker.Models;

namespace TipTracker.Repositories
{
    public class TipRepository : ITipRepository
    {
        private List<Tip> tips = new List<Tip>();
        private string filePath = "tips.json";

        public void AddTip(Tip tip)
        {
            tips.Add(tip);
        }

        public IEnumerable<Tip> GetTips(DateTime startDate, DateTime endDate)
        {
            foreach (var tip in tips)
            {
                if (tip.Date >= startDate && tip.Date <= endDate)
                {
                    yield return tip;
                }
            }
        }

        public void SaveChanges()
        {
            string jsonData = JsonConvert.SerializeObject(tips, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        public void LoadTips()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                tips = JsonConvert.DeserializeObject<List<Tip>>(jsonData);
            }
        }
    }
}
