using System;

using UIKit;
using Foundation;

using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;
using System.Collections.Generic;
using Firebase.Database;
using PipboyOrganizer.DataAccess;

namespace PipboyOrganizer
{
    public partial class ViewController : UIViewController
    {
        public static UserController uc;
        public static QuestController qc;
        public static SkillController sc;

        public static FirebaseManager db = new FirebaseManager();

        public User myUser;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UpdateUserData();
            // Perform any additional setup after loading the view, typically from a nib.
            LoadUserData();
            AddNewQuest();
            //LoadQuestData();
            AddNewSkill();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void LoadUserData(){
            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
            NSDictionary refe;
            myUser = new User();
            userNode.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                //Read whole User data and save in data variable.
                var data = snapshot.GetValue<NSDictionary>();
                //Read userlevel and assign value to label
                LblUserLevel.Text = data.ValueForKey(new NSString("UserLevel")).ToString();
                //Username
                LblUserName.Text = data.ValueForKey(new NSString("Username")).ToString();
                //User Skills
                var skills = data.ValueForKey(new NSString("UserSkills"));
                refe = data;
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });

        }

        public void AddNewQuest(){
            //Quest Keys
            object[] questCreatedKeys = { "Description", "ExpiringDate", "Name", "QuestStages", "RewardXP", "StartDate", "Status", "isCompleted" };
            //Stage Keys
            object[] questStage1Keys = { "Description", "StageID", "isCompleted" };
            //Stage Values
            object[] questStage1 = { "Kill ferals in Cambridge Police Station", "1", "false" };
            //Stage NSDictionary creation
            var qs1 = NSDictionary.FromObjectsAndKeys(questStage1, questStage1Keys, questStage1Keys.Length);

            object[] questStage2Keys = { "Description", "StageID", "isCompleted" };
            object[] questStage2 = { "Kill ferals in Sanctuary", "2", "false" };
            var qs2 = NSDictionary.FromObjectsAndKeys(questStage2, questStage2Keys, questStage2Keys.Length);

            //Quest Values
            object[] questCreatedValues = { "Knight Farias asked you to kill feral ghouls", "2018/7/17 19:10:20", "Cleansing the Commonwealth", "", 500, "2018/6/17 19:10:20", "true", "false" };
            //Quest NSDictionary creation
            var questCreatedFinal = NSDictionary.FromObjectsAndKeys(questCreatedValues, questCreatedKeys, questCreatedKeys.Length);

            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
            //Create quest node and add Quest data.
            userNode.GetChild("ActiveQuests").GetChild("Quest01").SetValue<NSDictionary>(questCreatedFinal);
            //Create stage node and add Stage data.
            userNode.GetChild("ActiveQuests").GetChild("Quest01").GetChild("QuestStages").GetChild("Stage01").SetValue<NSDictionary>(qs1);
            userNode.GetChild("ActiveQuests").GetChild("Quest01").GetChild("QuestStages").GetChild("Stage02").SetValue<NSDictionary>(qs2);
        }

        public void AddNewSkill(){
            //Skill keys
            object[] skillKeys = { "Description", "Level", "Name" };
            //Skill values
            object[] skillValues = { "Ability to repair artifacts", 45, "Repair" };
            //Skill NSDictionary creation
            var sk = NSDictionary.FromObjectsAndKeys(skillValues, skillKeys, skillKeys.Length);

            DatabaseReference skillsNode = Database.DefaultInstance.GetRootReference().GetChild("Users").GetChild("User").GetChild("UserSkills");
            skillsNode.GetChild("Skill03").SetValue<NSDictionary>(sk);
        }

        public void UpdateUserData(){
            //We need a User as a parameter to update data in Firebase
            User user = new User()
            {
                Username = "SagsSkywalker",
                Experience = 200000,
                UserLevel = 20
            };
            DatabaseReference userNode = Database.DefaultInstance.GetRootReference().GetChild("Users").GetChild("User");
            userNode.GetChild("Username").SetValue<NSString>(new NSString(user.Username));
            userNode.GetChild("Experience").SetValue<NSNumber>(new NSNumber(user.Experience));
            userNode.GetChild("UserLevel").SetValue<NSNumber>(new NSNumber(user.UserLevel));
        }

        public void LoadQuestData(){
            //We need a parameter for the QuestID for this is Quest01
            string questNodeParam = "Quest01";
            //Prepare Firebase access
            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
            DatabaseReference questNode = userNode.GetChild("ActiveQuests").GetChild(questNodeParam);
            //Prepare stages node using QuestID parameter
            DatabaseReference stagesNode = questNode.GetChild("QuestStages");

            stagesNode.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                //Create stages list
                List<Stage> stages = new List<Stage>();
                var stagesData = snapshot.GetValue<NSDictionary>().Values;
                int c = 0;
                foreach (var stage in stagesData)
                {
                    Stage tStage = new Stage();
                    tStage.IDStage = c++;
                    tStage.Description = stage.ValueForKey(new NSString("Description")).ToString();
                    tStage.isCompleted = (stage.ValueForKey(new NSString("isCompleted")).ToString() == "true");
                    stages.Add(tStage);
                }
                //Create Quest
                Quest quest = new Quest();
                questNode.ObserveSingleEvent(DataEventType.Value, (snapshot2) =>
                {
                    var questData = snapshot2.GetValue<NSDictionary>().Values;
                    foreach (var node in questData)
                    {
                        quest.Name = node.ValueForKey(new NSString("Name")).ToString();
                        quest.Description = node.ValueForKey(new NSString("Description")).ToString();
                        quest.RewardXP = int.Parse(node.ValueForKey(new NSString("RewardXP")).ToString());
                        quest.StartDate = DateTime.Parse(node.ValueForKey(new NSString("StartDate")).ToString());
                        quest.ExpiringDate = DateTime.Parse(node.ValueForKey(new NSString("ExpiringDate")).ToString());
                        quest.isCompleted = (node.ValueForKey(new NSString("isCompleted")).ToString() == "true");
                        quest.Status = (node.ValueForKey(new NSString("Status")).ToString() == "true");
                    }
                    quest.QuestStages = stages;

                    //Do whatever with this quest.
                    //TODO: Display on table.
                    Console.WriteLine(quest);

                }, (err) =>
                {
                    Console.WriteLine(err.LocalizedDescription);
                });
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });
        }


        public void LoadAllQuests(){
            //Prepare Firebase access
            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");


        }
    }
}
