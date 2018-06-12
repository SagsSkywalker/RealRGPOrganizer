using System;

using UIKit;

using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;
using System.Collections.Generic;

namespace PipboyOrganizer
{
    public partial class ViewController : UIViewController
    {
        public static UserController uc;
        public static QuestController qc;
        public static SkillController sc;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

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
            LblUserName.Text = uc.user.Username;
            LblUserLevel.Text = uc.user.UserLevel.ToString();
        }

        public List<Skill> UserTopSkills(){
            //TODO: Return a List of "Top Skills"
            return null;
        }
    }
}
