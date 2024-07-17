using TipTracker.Models;
using System.Collections.Generic;

namespace TipTracker.Repositories
{
    public interface ITipRepository
    {
        void AddTip(Tip tip);
        IEnumerable<Tip> GetTips(DateTime startDate, DateTime endDate);
        void SaveChanges();
        void LoadTips();
    }
}
