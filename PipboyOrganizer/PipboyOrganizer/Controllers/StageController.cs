using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class StageController
    {
        public StageController(Stage s)
        {
            stage = s;
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

        /// <summary>
        /// Checks if stage is completed.
        /// </summary>
        /// <returns><c>true</c>, if completed was given, <c>false</c> if is not.</returns>
        public bool isCompleted(){
            if (stage.isCompleted)
                return true;
            return false;
        }
    }
}
