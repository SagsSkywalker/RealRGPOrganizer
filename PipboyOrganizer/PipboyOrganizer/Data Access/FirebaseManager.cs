using System;
using Firebase.Database;
using Foundation;
using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;

namespace PipboyOrganizer.DataAccess
{
    public class FirebaseManager
    {
        #region Class Variables
        //DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
        public User user = new User();
        #endregion

        public FirebaseManager()
        {
        }

        #region Read Data Methods
        /// <summary>
        /// Loads the user experience.
        /// </summary>
        /// <returns>User experience read from Firebase</returns>
        public void GetUserExperience(){
            try
            {
                DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
                DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
                userNode.GetChild("Experience").ObserveSingleEvent(DataEventType.Value, (snapshot) =>
                {
                    Console.WriteLine(snapshot.GetValue<NSNumber>().Int32Value);
                    Console.WriteLine(snapshot.GetValue<NSNumber>().Int32Value);
                    Console.WriteLine(snapshot.GetValue<NSNumber>().Int32Value);
                    //this.Form1_Load += new System.EventHandler(this.Form1_Load);
                }, (error) => {
                    Console.WriteLine(error.LocalizedDescription);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void Form1_Load(object sender, System.EventArgs e, int exp)
        {
            // Add your form load event handling code here.
            user.Experience = exp;
        }

        //Write data to a Firebase node
        //nuint experience = 190001;
        //userNode.GetChild("Experience").SetValue<NSNumber>(NSNumber.FromNUInt(experience));
    }
}
