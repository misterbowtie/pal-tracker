using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _tec;

        public MySqlTimeEntryRepository(TimeEntryContext timeEntryContext)
        {
            _tec = timeEntryContext;
        }



        public bool Contains(long id)
        {
            return _tec.TimeEntryRecords.AsNoTracking().Any(t => t.Id == id);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var timeEntryRec = timeEntry.ToRecord();

            _tec.TimeEntryRecords.Add(timeEntryRec);
            _tec.SaveChanges();

            return Find(timeEntryRec.Id.Value);
        }

        public void Delete(long id)
        {
            var timeEnt = FindRecord(id);

            _tec.TimeEntryRecords.Remove(timeEnt);
            _tec.SaveChanges();
            
        }

        public TimeEntry Find(long id)
        {
            return FindRecord(id).ToEntity();
        }

        public IEnumerable<TimeEntry> List()
        {
            return _tec.TimeEntryRecords.AsNoTracking().Select(t => t.ToEntity()).ToList();
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var timeEnt = timeEntry.ToRecord();
            timeEnt.Id = id;

            _tec.TimeEntryRecords.Update(timeEnt);
            _tec.SaveChanges();

            return timeEnt.ToEntity();
        }

        private TimeEntryRecord FindRecord(long id) =>
            _tec.TimeEntryRecords.AsNoTracking().Single(t => t.Id == id);
     }
}
