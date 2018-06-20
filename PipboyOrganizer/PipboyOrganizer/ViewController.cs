using System;

using UIKit;
using Foundation;
using PipboyOrganizer.Controllers;
using PipboyOrganizer.Models;
using System.Collections.Generic;
using Firebase.Database;
using PipboyOrganizer.DataAccess;

namespace PipboyOrganizer {
	public partial class ViewController : UIViewController, IUICollectionViewDelegate, IUICollectionViewDataSource {
		public static UserController uc;
		public static QuestController qc;
		public static SkillController sc;

		public static FirebaseManager db = new FirebaseManager ();

		//public User myUser;

		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitializeComponents ();
			// Perform any additional setup after loading the view, typically from a nib.
			//db.LoadUserData();

			//Console.WriteLine(myUser);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public void InitializeComponents ()
		{
			cvSkills.DataSource = this;
			cvSkills.Delegate = this;
			//myUser = new User ();
			FirebaseManager.SharedInstance.UserDataLoaded += FirebaseManager_UserDataLoaded;
			FirebaseManager.SharedInstance.SkillsDataLoaded += FirebaseManager_SkillsDataLoaded;
			FirebaseManager.SharedInstance.QuestsDataLoaded += FirebaseManager_QuestsDataLoaded;
			//FirebaseManager.SharedInstance.CQuestsDataLoaded += FirebaseManager_CQuestsDataLoaded;
			FirebaseManager.SharedInstance.LoadUserData ();
		}

		//private void FirebaseManager_CQuestsDataLoaded(object sender, CQuestsDataLoadedEvent e)
		//{
		//    myUser.CompletedQuests = e.quests;
		//}

		private void FirebaseManager_QuestsDataLoaded (object sender, QuestsDataLoadedEvent e)
		{
			UserPersistanceClass.myUser.ActiveQuests = e.quests;
		}

		private void FirebaseManager_SkillsDataLoaded (object sender, SkillsDataLoadedEvent e)
		{
			UserPersistanceClass.myUser.UserSkills = e.skills;
			cvSkills.ReloadData ();
		}

		void FirebaseManager_UserDataLoaded (object sender, UserDataLoadedEvent e)
		{
			//myUser = new User();
			UserPersistanceClass.myUser = e.user;
			LblUserName.Text = UserPersistanceClass.myUser.Username;
			LblUserLevel.Text = UserPersistanceClass.myUser.UserLevel.ToString ();
			FirebaseManager.SharedInstance.LoadUserSkills ();
			FirebaseManager.SharedInstance.LoadUserQuests ();
			//FirebaseManager.SharedInstance.LoadUserCompletedQuests();
		}

		public void LoadQuestData ()
		{
			//We need a parameter for the QuestID for this is Quest01
			string questNodeParam = "Quest01";
			//Prepare Firebase access
			DatabaseReference rootNode = Database.DefaultInstance.GetRootReference ();
			DatabaseReference userNode = rootNode.GetChild ("Users").GetChild ("User");
			DatabaseReference questNode = userNode.GetChild ("ActiveQuests").GetChild (questNodeParam);
			//Prepare stages node using QuestID parameter
			DatabaseReference stagesNode = questNode.GetChild ("QuestStages");

			stagesNode.ObserveSingleEvent (DataEventType.Value, (snapshot) =>
			{
				//Create stages list
				List<Stage> stages = new List<Stage> ();
				var stagesData = snapshot.GetValue<NSDictionary> ().Values;
				int c = 0;
				foreach (var stage in stagesData) {
					Stage tStage = new Stage ();
					tStage.IDStage = c++;
					tStage.Description = stage.ValueForKey (new NSString ("Description")).ToString ();
					tStage.isCompleted = (stage.ValueForKey (new NSString ("isCompleted")).ToString () == "true");
					stages.Add (tStage);
				}
				//Create Quest
				Quest quest = new Quest ();
				questNode.ObserveSingleEvent (DataEventType.Value, (snapshot2) =>
				{
					var questData = snapshot2.GetValue<NSDictionary> ().Values;
					foreach (var node in questData) {
						quest.Name = node.ValueForKey (new NSString ("Name")).ToString ();
						quest.Description = node.ValueForKey (new NSString ("Description")).ToString ();
						quest.RewardXP = int.Parse (node.ValueForKey (new NSString ("RewardXP")).ToString ());
						quest.StartDate = DateTime.Parse (node.ValueForKey (new NSString ("StartDate")).ToString ());
						quest.ExpiringDate = DateTime.Parse (node.ValueForKey (new NSString ("ExpiringDate")).ToString ());
						quest.isCompleted = (node.ValueForKey (new NSString ("isCompleted")).ToString () == "true");
						quest.Status = (node.ValueForKey (new NSString ("Status")).ToString () == "true");
					}
					quest.QuestStages = stages;

					//Do whatever with this quest.
					//TODO: Display on table.
					Console.WriteLine (quest);

				}, (err) =>
				{
					Console.WriteLine (err.LocalizedDescription);
				});
			}, (error) =>
			{
				Console.WriteLine (error.LocalizedDescription);
			});
		}


		public void LoadAllQuests ()
		{
			//Prepare Firebase access
			DatabaseReference rootNode = Database.DefaultInstance.GetRootReference ();
			DatabaseReference userNode = rootNode.GetChild ("Users").GetChild ("User");


		}

		[Export ("numberOfSectionsInCollectionView:")]
		public nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return UserPersistanceClass.myUser?.UserSkills == null ? 0 : 3;
			//return 3;
		}

		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell (nameof (SkillViewCell), indexPath) as SkillViewCell;
			cell.Name = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Name;
			cell.SkillDescription = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Description;
			cell.Level = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Level.ToString ();
			return cell;
		}
	}
}
