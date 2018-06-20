// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using PipboyOrganizer.DataAccess;
using PipboyOrganizer.Models;
using UIKit;

namespace PipboyOrganizer
{
	public partial class ScheduleViewController : UIViewController ,IUICollectionViewDelegate,IUICollectionViewDataSource
	{
        #region Class Variables
        FirebaseManager fb = new FirebaseManager();
        #endregion

        public ScheduleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			cvSkills.DataSource = this;
			cvSkills.Delegate = this;
		}

		partial void AddSkill_TouchUpInside (NSObject sender)
		{
			InvokeOnMainThread ( () => {
				var alert = UIAlertController.Create ("New Skill", "Write your Skill", UIAlertControllerStyle.Alert);

				UITextField field = new UITextField ();
				UITextField field2 = new UITextField ();

				alert.AddTextField ((textField) => {
					alert.AddAction (UIAlertAction.Create ($"OK", UIAlertActionStyle.Default, delegate {
						try {

							var skillName = alert.TextFields [0].Text;
							var skillDescription = alert.TextFields [1].Text;
							Console.WriteLine (skillName);
							Console.WriteLine (skillDescription);
							Skill skill = new Skill ();
							skill.Name = skillName;
							skill.Description = skillDescription;
							skill.Level = 1;
                            fb.AddNewSkill(skill);
                            UserPersistanceClass.myUser.UserSkills.Add(skill);
                            cvSkills.ReloadData ();

						} catch (Exception ex) {
                            throw ex;
						}
					}));
					field = textField;

					field.Placeholder = "Example: Cooking";
					field.Text = "";
					field.AutocorrectionType = UITextAutocorrectionType.No;
					field.KeyboardType = UIKeyboardType.Default;
					field.ReturnKeyType = UIReturnKeyType.Done;
					field.ClearButtonMode = UITextFieldViewMode.WhileEditing;

				});
				alert.AddTextField ((UITextField obj) => {
					field2 = obj;

					field2.Placeholder = "Example: Ability to make some delicious dishes";
					field2.Text = "";
					field2.AutocorrectionType = UITextAutocorrectionType.No;
					field2.KeyboardType = UIKeyboardType.Default;
					field2.ReturnKeyType = UIReturnKeyType.Done;
					field2.ClearButtonMode = UITextFieldViewMode.WhileEditing;

				});

				alert.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
				PresentViewController (alert, true, null);
			});


		}

		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell (nameof (SkillViewCell), indexPath) as SkillViewCell;
			cell.Name = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Name;
			cell.SkillDescription = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Description;
			cell.Level = UserPersistanceClass.myUser.UserSkills [indexPath.Row].Level.ToString ();
			return cell;
		}

		public nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return UserPersistanceClass.myUser?.UserSkills == null ? 0 : UserPersistanceClass.myUser.UserSkills.Count;
		}

	}
}
