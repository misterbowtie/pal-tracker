using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private Dictionary<long, TimeEntry> _timeEntryDict = new Dictionary<long, TimeEntry>();
        
        public TimeEntry Create(TimeEntry timeEntry)
        {
            long id = _timeEntryDict.Count() + 1;
            timeEntry.Id = id;

            _timeEntryDict.Add(id, timeEntry);

            return timeEntry;
        }

        public TimeEntry Find(long id)
        {
            return _timeEntryDict.Where(t => t.Key == id).Select(t => t.Value).FirstOrDefault();
        }

        public bool Contains(long id)
        {
            return _timeEntryDict.ContainsKey(id);
        }

        public IEnumerable<TimeEntry> List()
        {
            return _timeEntryDict.Select(t => t.Value).ToList();
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;
            _timeEntryDict.Remove(id);
            _timeEntryDict.Add(id, timeEntry);

            return timeEntry;
    }

    public void Delete(long id)
        {
            _timeEntryDict.Remove(id);
        }

    }
}
