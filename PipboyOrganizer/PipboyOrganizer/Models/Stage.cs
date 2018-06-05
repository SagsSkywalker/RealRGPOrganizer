using System;
namespace PipboyOrganizer.Models
{
    public class Stage
    {
        public int IDStage
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool isCompleted
        {
            get;
            set;
        }

        public Stage()
        {
        }
    }
}
