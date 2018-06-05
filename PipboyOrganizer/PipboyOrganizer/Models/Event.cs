using System;
using System.Collections.Generic;

namespace PipboyOrganizer.Models
{
    public class Event
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public List<DayOfWeek> AssignedDays
        {
            get;
            set;
        }

        public int RewardXP
        {
            get;
            set;
        }

        public List<Skill> SkillsAffected
        {
            get;
            set;
        }

        public bool isCompleted
        {
            get;
            set;
        }

        public bool Status
        {
            get;
            set;
        }

        public Event()
        {
        }
    }
}
