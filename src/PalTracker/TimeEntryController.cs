using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IOperationCounter<TimeEntry> _opCounter;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository, IOperationCounter<TimeEntry> operationCounter)
        {
            _timeEntryRepository = timeEntryRepository;
            _opCounter = operationCounter;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _timeEntryRepository.Create(timeEntry);

            _opCounter.Increment(TrackedOperation.Create);

            return CreatedAtRoute("GetTimeEntry", new { id = createdTimeEntry.Id }, createdTimeEntry);
        }

        //Read
        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            _opCounter.Increment(TrackedOperation.Read);
            return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Find(id)) : NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            _opCounter.Increment(TrackedOperation.List);
            return Ok(_timeEntryRepository.List());
        }

        //Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            _opCounter.Increment(TrackedOperation.Update);
            return _timeEntryRepository.Contains(id) ? (IActionResult)Ok(_timeEntryRepository.Update(id, timeEntry)) : NotFound();
        }


        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _opCounter.Increment(TrackedOperation.Delete);

            if (!_timeEntryRepository.Contains(id))
            {
                return NotFound();
            }

            _timeEntryRepository.Delete(id);
            return NoContent();
        }
    }
}
