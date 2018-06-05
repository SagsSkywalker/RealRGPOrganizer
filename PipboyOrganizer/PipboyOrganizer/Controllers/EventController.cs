using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class EventController
    {
        public EventController(Event e, User u)
        {
            _event = e;
            user = u;
        }

        public Event _event
        {
            get;
            set;
        }

        public User user
        {
            get;
            set;
        }

        /// <summary>
        /// Creates the event and adds it to User Event List.
        /// </summary>
        void CreateEvent(){
            user.Events.Add(_event);
        }
    }
}
