﻿using System;
using System.Collections.Generic;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class QuestController
    {
        public QuestController()
        {
        }

        public Quest quest
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
        /// Gets the active quests.
        /// </summary>
        /// <returns>The active quests.</returns>
        /// <param name="u">User whom to retrieve quests</param>
        public List<Quest> GetActiveQuests(User u){
            return u.ActiveQuests;
        }

        /// <summary>
        /// Gets the completed quests.
        /// </summary>
        /// <returns>The completed quests.</returns>
        /// <param name="u">User whom to retrieve quests</param>
        public List<Quest> GetCompletedQuests(User u){
            return u.CompletedQuests;
        }

        /// <summary>
        /// Completes the quest, gives User the reward xp, adds points to Skills and adds it to User Completed Quests.
        /// </summary>
        public void CompleteQuest()
        {
            int stages = quest.QuestStages.Count;
            int counter = 0;
            foreach (Stage stg in quest.QuestStages)
            {
                if(stg.isCompleted)
                    counter++;
            }
            if (counter == stages)
            {
                quest.isCompleted = true;
                user.CompletedQuests.Add(quest);
                //user.Experience += 
            }
        }

        /// <summary>
        /// Checks if Quest is expired
        /// </summary>
        /// <returns><c>true</c>, If expired was given, <c>false</c> If there is still time to complete quest.</returns>
        public bool isExpired(){
            if(quest.ExpiringDate.CompareTo(DateTime.Now) <= 0){
                quest.Status = false;
                user.CompletedQuests.Add(quest);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates the quest and adds it to User Active Quests.
        /// </summary>
        public void CreateQuest(){
            user.ActiveQuests.Add(quest);
        }
    }
}
