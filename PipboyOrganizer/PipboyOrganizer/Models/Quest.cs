using System;
using System.Collections.Generic;

namespace PipboyOrganizer.Models
{
    public class Quest
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

        public List<Stage> QuestStages
        {
            get;
            set;
        }

        public bool isCompleted
        {
            get;
            set;
        }

        public int RewardXP
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime ExpiringDate
        {
            get;
            set;
        }

        public bool Status
        {
            get;
            set;
        }

        public List<Skill> AffectedSkills
        {
            get;
            set;
        }

        public Quest()
        {
        }
    }
}
