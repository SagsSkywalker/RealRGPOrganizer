using System;
using System.Collections.Generic;

namespace PipboyOrganizer.Models
{
    public class User
    {
        public string Username
        {
            get;
            set;
        }

        public List<Skill> UserSkills
        {
            get;
            set;
        }

        public int UserLevel
        {
            get;
            set;
        }

        public List<Quest> ActiveQuests
        {
            get;
            set;
        }

        public List<Quest> CompletedQuests
        {
            get;
            set;
        }

        public List<Event> Events
        {
            get;
            set;
        }

        public int Experience
        {
            get;
            set;
        }

        public User()
        {
        }
    }
}
