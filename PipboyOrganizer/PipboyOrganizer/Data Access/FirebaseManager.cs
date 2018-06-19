using System;
using System.Collections.Generic;
using Firebase.Database;
using Foundation;
using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;

namespace PipboyOrganizer.DataAccess
{
    public class FirebaseManager
    {
        #region Class Variables
        DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
        DatabaseReference userNode = Database.DefaultInstance.GetRootReference().GetChild("Users").GetChild("User");

        //public event EventHandler<TweetsFetchedEventArgs> TweetsFetched;
        //public event EventHandler<TweetsFetchedFailedEventArgs> FailedTweetsFetched;
        #endregion

        public FirebaseManager()
        {
        }

        #region Read Data Methods

        #endregion

        #region Write Data Methods
        /// <summary>
        /// Adds the new quest to User's Active quests.
        /// </summary>
        /// <param name="q">The quest you want to add</param>
        public void AddNewQuest(Quest q)
        {
            ////Quest Keys
            //object[] questCreatedKeys = { "Description", "ExpiringDate", "Name", "QuestStages", "RewardXP", "StartDate", "Status", "isCompleted" };
            ////Stage Keys
            //object[] questStage1Keys = { "Description", "StageID", "isCompleted" };
            ////Stage Values
            //object[] questStage1 = { "Kill ferals in Cambridge Police Station", "1", "false" };
            ////Stage NSDictionary creation
            //var qs1 = NSDictionary.FromObjectsAndKeys(questStage1, questStage1Keys, questStage1Keys.Length);

            //object[] questStage2Keys = { "Description", "StageID", "isCompleted" };
            //object[] questStage2 = { "Kill ferals in Sanctuary", "2", "false" };
            //var qs2 = NSDictionary.FromObjectsAndKeys(questStage2, questStage2Keys, questStage2Keys.Length);

            ////Quest Values
            //object[] questCreatedValues = { "Knight Farias asked you to kill feral ghouls", "2018/7/17 19:10:20", "Cleansing the Commonwealth", "", 500, "2018/6/17 19:10:20", "true", "false" };
            ////Quest NSDictionary creation
            //var questCreatedFinal = NSDictionary.FromObjectsAndKeys(questCreatedValues, questCreatedKeys, questCreatedKeys.Length);

            //DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            //DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
            ////Create quest node and add Quest data.
            //userNode.GetChild("ActiveQuests").GetChild("Quest01").SetValue<NSDictionary>(questCreatedFinal);
            ////Create stage node and add Stage data.
            //userNode.GetChild("ActiveQuests").GetChild("Quest01").GetChild("QuestStages").GetChild("Stage01").SetValue<NSDictionary>(qs1);
            //userNode.GetChild("ActiveQuests").GetChild("Quest01").GetChild("QuestStages").GetChild("Stage02").SetValue<NSDictionary>(qs2);

            //Quest Keys
            object[] questCreatedKeys = { "Description", "ExpiringDate", "Name", "QuestStages", "RewardXP", "StartDate", "Status", "isCompleted" };
            List<NSDictionary> stages = new List<NSDictionary>();
            foreach (Stage stage in q.QuestStages)
            {
                //Stage Keys
                object[] questStageKeys = { "Description", "StageID", "isCompleted" };
                //Stage Values
                object[] questStageValues = { stage.Description, stage.IDStage, stage.isCompleted };
                //Stage NSDictionary creation
                var qs = NSDictionary.FromObjectsAndKeys(questStageValues, questStageKeys, questStageKeys.Length);
                stages.Add(qs);
            }
            //Quest Values
            object[] questCreatedValues = { q.Description, q.ExpiringDate.ToString(), q.Name, "", q.RewardXP, q.StartDate.ToString(), q.Status, q.isCompleted };
            //Quest NSDictionary creation
            var questCreatedFinal = NSDictionary.FromObjectsAndKeys(questCreatedValues, questCreatedKeys, questCreatedKeys.Length);
            //int questNumber = int.Parse(userNode.GetChild("ActiveQuests").GetChild("Counter").obse)
            //Create quest node and add Quest data.
            userNode.GetChild("ActiveQuests").GetChild("Quest02").SetValue<NSDictionary>(questCreatedFinal);
            //Create stage node and add Stage data.
            int cont = 0;
            foreach (var stage in stages)
            {
                userNode.GetChild("ActiveQuests").GetChild("Quest02").GetChild("QuestStages").GetChild($"Stage{cont++}").SetValue<NSDictionary>(stage);
            }
        }
        #endregion

        //public class TweetsFetchedEventArgs : EventArgs
        //{
        //    public List<Status> Tweets { get; private set; }
        //    public TweetsFetchedEventArgs(List<Status> tweets)
        //    {
        //        Tweets = tweets;
        //    }

        //}
        //public class TweetsFetchedFailedEventArgs : EventArgs
        //{
        //    //propiedad que se llama ErrorMesage
        //    public String ErrorMessage { get; private set; }
        //    public TweetsFetchedFailedEventArgs(String errorMessage)
        //    {
        //        ErrorMessage = errorMessage;
        //    }
        //}
    }
}
