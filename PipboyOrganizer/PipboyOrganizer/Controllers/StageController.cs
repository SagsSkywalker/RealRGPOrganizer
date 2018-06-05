using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class StageController
    {
        public StageController()
        {
        }

        public Stage stage
        {
            get;
            set;
        }

        /// <summary>
        /// Completes the stage.
        /// </summary>
        public void CompleteStage(){
            stage.isCompleted = true;
        }
    }
}
