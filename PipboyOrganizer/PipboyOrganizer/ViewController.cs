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
            // Perform any additional setup after loading the view, typically from a nib.
            LoadUserData();
            AddNewQuest();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void Initialize(){
            //Load User from Firebase
            //In the meantime we create new user
            User user = new User()
            {
                Username = "Skywalker",
                UserLevel = 1,
                Experience = 0,
                UserSkills = new List<Skill>(){
                    new Skill(){
                        Name = "Lightsaber Combat",
                        Description = "The proper ways to use a lightsaber",
                        Level = 1
                    },
                    new Skill(){
                        Name = "Droid Repair",
                        Description = "Bleeep",
                        Level = 1
                    }
                }
            };
            uc = new UserController(user);
            qc = new QuestController();
            sc = new SkillController();
        }

        public void LoadUserData(){
            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");

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
                cvSkills.DataSource = skills as IUICollectionViewDataSource;
            }, (error) =>
            {
                Console.WriteLine(error.LocalizedDescription);
            });

        }

        public void AddNewQuest(){
            Quest quest = new Quest()
            {
                Name = "Cleansing the Commonwealth",
                Description = "Knight Farias asked you to kill feral ghouls",
                isCompleted = false,
                RewardXP = 500,
                StartDate = new DateTime(2018, 6, 17, 19, 10, 20),
                ExpiringDate = new DateTime(2018, 7, 17, 19, 10, 20),
                Status = true,
                QuestStages = new List<Stage>(){
                    new Stage(){
                        IDStage = 1,
                        Description = "Kill ferals in Cambridge Police Station",
                        isCompleted = false
                    },
                    new Stage(){
                        IDStage = 2,
                        Description = "Kill ferals in Sanctuary",
                        isCompleted = false
                    }
                }
            };

            //object[] questCreated = { "Quest02" };

            //object[] questCreatedKeys = { "Description", "ExpiringDate", "Name", "QuestStages", "RewardXP", "StartDate", "Status", "isCompleted" };

            //object[] questStage1Keys = { "Description", "StageID", "isCompleted" };
            //object[] questStage1 = { "Kill ferals in Cambridge Police Station", "1", "false" };
            //var qs1 = NSDictionary.FromObjectsAndKeys(questStage1, questStage1Keys, questStage1Keys.Length);

            //object[] questStage2Keys = { "Description", "StageID", "isCompleted" };
            //object[] questStage2 = { "Kill ferals in Sanctuary", "2", "false" };
            //var qs2 = NSDictionary.FromObjectsAndKeys(questStage2, questStage2Keys, questStage2Keys.Length);

            //object[] questStagesValues = { qs1, qs2 };

            //object[] questCreatedValues = { "Knight Farias asked you to kill feral ghouls", "2018/7/17 19:10:20", "Cleansing the Commonwealth", questStagesValues, "500", "2018/6/17 19:10:20", "true", "false" };

            //object[] questValues = { questCreatedValues };

            //var questCreatedFinal = NSDictionary.FromObjectsAndKeys(questValues, questCreatedKeys);

            object[] keys = { "Test" };
            object[] values = { "I dont know anymore" };
            var data = NSDictionary.FromObjectsAndKeys(values, keys, keys.Length);

            DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();
            DatabaseReference userNode = rootNode.GetChild("Users").GetChild("User");
            userNode.GetChild("ActiveQuests").SetValue<NSDictionary>(data);
        }
    }
}
