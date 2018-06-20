using System;
using System.Collections.Generic;
using Firebase.Database;
using Foundation;
using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;

using nuint = global::System.UInt32;

namespace PipboyOrganizer.DataAccess
{
    public class FirebaseManager
    {
        #region Class Variables
        DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
        DatabaseReference userNode = Database.DefaultInstance.GetRootReference().GetChild("Users").GetChild("User");

        static readonly Lazy<FirebaseManager> lazy = new Lazy<FirebaseManager>(() => new FirebaseManager());
        public static FirebaseManager SharedInstance { get => lazy.Value; }

        public event EventHandler<QuestDataLoadedEvent> QuestDataLoaded;
        public event EventHandler<UserDataLoadedEvent> UserDataLoaded;
        public event EventHandler<SkillsDataLoadedEvent> SkillsDataLoaded;
        public event EventHandler<QuestsDataLoadedEvent> QuestsDataLoaded;
        public event EventHandler<StageDataLoadedEvent> StageDataLoaded;
        public event EventHandler<StagesDataLoadedEvent> StagesDataLoaded;
        #endregion

        public FirebaseManager()
        {
        }

        #region Read Data Methods
        public void LoadUserData()
        {
            userNode.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                User userResponse = new User();
                //Read whole User data and save in data variable.
                var data = snapshot.GetValue<NSDictionary>();
                userResponse.UserLevel = int.Parse(data.ValueForKey(new NSString("UserLevel")).ToString());
                userResponse.Username = data.ValueForKey(new NSString("Username")).ToString();
                userResponse.Experience = int.Parse(data.ValueForKey(new NSString("Experience")).ToString());
                var e = new UserDataLoadedEvent(userResponse);
                UserDataLoaded.Invoke(this, e);
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });
        }

        public void LoadUserQuests(){
            userNode.GetChild("ActiveQuests").ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                List<Quest> questsResponse = new List<Quest>();
                var data = snapshot.GetValue<NSDictionary>().Values;
                foreach (var quest in data)
                {
                    Quest qt = new Quest();
                    qt.Name = quest.ValueForKey(new NSString("Name")).ToString();
                    qt.Description = quest.ValueForKey(new NSString("Description")).ToString();
                    qt.StartDate = DateTime.Parse(quest.ValueForKey(new NSString("StartDate")).ToString());
                    qt.ExpiringDate = DateTime.Parse(quest.ValueForKey(new NSString("ExpiringDate")).ToString());
                    qt.isCompleted = (quest.ValueForKey(new NSString("isCompleted")).ToString() == "true");
                    qt.Status = (quest.ValueForKey(new NSString("Status")).ToString() == "true");
                    qt.RewardXP = int.Parse(quest.ValueForKey(new NSString("RewardXP")).ToString());
                    var data2 = quest.ValueForKey(new NSString("QuestStages"));
                    List<Stage> stagesResponse = new List<Stage>();
                    //for (nuint i = 0; i < data2.Count; i++)
                    //{
                    //    Stage st = new Stage();
                    //    st.Description = data2.GetItem<NSDictionary> (i)["Description"].ToString();
                    //    st.isCompleted = (data2.GetItem<NSDictionary>(i)["isCompleted"].ToString()=="true");
                    //    st.IDStage = int.Parse(data2.GetItem<NSDictionary>(i)["StageID"].ToString());
                    //    stagesResponse.Add(st);
                    //}
                    qt.QuestStages = stagesResponse;
                }
                var e = new QuestsDataLoadedEvent(questsResponse);
                QuestsDataLoaded.Invoke(this, e);
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });
        }

        /// <summary>
        /// Loads the user skills.
        /// </summary>
        public void LoadUserSkills(){
            userNode.GetChild("UserSkills").ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                List<Skill> skillsResponse = new List<Skill>();
                var data = snapshot.GetValue<NSDictionary>().Values;
                foreach (var skill in data)
                {
                    Skill sk = new Skill();
                    sk.Name = skill.ValueForKey(new NSString("Name")).ToString();
                    sk.Description = skill.ValueForKey(new NSString("Description")).ToString();
                    sk.Level = int.Parse(skill.ValueForKey(new NSString("Level")).ToString());
                    //sk.Level = int.Parse(skill.)
                    skillsResponse.Add(sk);
                }
                var e = new SkillsDataLoadedEvent(skillsResponse);
                SkillsDataLoaded.Invoke(this, e);
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });
        }
        #endregion

        #region Write Data Methods
        /// <summary>
        /// Adds the new quest to User's Active quests.
        /// </summary>
        /// <param name="q">The quest you want to add</param>
        public void AddNewQuest(Quest q)
        {

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

        /// <summary>
        /// Adds the new skill to User skills
        /// </summary>
        /// <param name="s">The skill you want to add</param>
        public void AddNewSkill(Skill s)
        {
            //Skill keys
            object[] skillKeys = { "Description", "Level", "Name" };
            //Skill values
            object[] skillValues = { s.Description, s.Level, s.Name };
            //Skill NSDictionary creation
            var sk = NSDictionary.FromObjectsAndKeys(skillValues, skillKeys, skillKeys.Length);

            DatabaseReference skillsNode = userNode.GetChild("UserSkills");
            //TODO: Implement counter node
            skillsNode.GetChild("Skill04").SetValue<NSDictionary>(sk);
        }
        #endregion

        #region Update Data Methods
        /// <summary>
        /// Updates the user data.
        /// </summary>
        /// <param name="u">User object to replace data</param>
        public void UpdateUserData(User u)
        {
            userNode.GetChild("Username").SetValue<NSString>(new NSString(u.Username));
            userNode.GetChild("Experience").SetValue<NSNumber>(new NSNumber(u.Experience));
            userNode.GetChild("UserLevel").SetValue<NSNumber>(new NSNumber(u.UserLevel));
        }
        #endregion
    }

    public class QuestDataLoadedEvent : EventArgs
    {
        public Quest quest { get; private set; }
        public QuestDataLoadedEvent(Quest _quest)
        {
            quest = _quest;
        }
    }

    public class StageDataLoadedEvent : EventArgs
    {
        public Stage stage { get; private set; }
        public StageDataLoadedEvent(Stage _stage)
        {
            stage = _stage;
        }
    }

    public class UserDataLoadedEvent : EventArgs{
        public User user { get; private set; }
        public UserDataLoadedEvent(User _user){
            user = _user;
        }
    }

    public class SkillsDataLoadedEvent : EventArgs{
        public List<Skill> skills { get; private set; }
        public SkillsDataLoadedEvent(List<Skill> _skills){
            skills = _skills;
        }
    }

    public class StagesDataLoadedEvent : EventArgs
    {
        public List<Stage> stages { get; private set; }
        public StagesDataLoadedEvent(List<Stage> _stages)
        {
            stages = _stages;
        }
    }

    public class QuestsDataLoadedEvent : EventArgs
    {
        public List<Quest> quests { get; private set; }
        public QuestsDataLoadedEvent(List<Quest> _quests)
        {
            quests = _quests;
        }
    }
}
