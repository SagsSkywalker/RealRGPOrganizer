// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using PipboyOrganizer.DataAccess;
using PipboyOrganizer.Models;
using UIKit;

namespace PipboyOrganizer
{
    public partial class AddQuestViewController : UIViewController,IUITableViewDelegate, IUITableViewDataSource
	{
        List<Stage> stages = new List<Stage>();
        FirebaseManager fb = new FirebaseManager();
        Quest quest;

		public AddQuestViewController (IntPtr handle) : base (handle)
		{

		}

		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("StageViewCell", indexPath) as StageViewCell;
			cell.StageDescription = stages [indexPath.Row].Description;
			return cell;
		}

		public nint RowsInSection (UITableView tableView, nint section)
		{
			return stages.Count;
		}
		private void AddStage ()
		{
			InvokeOnMainThread (() =>
			{
				Stage stage = new Stage ();
				stage.Description = TxtQuestStage.Text;
				stage.isCompleted = false;
				stages.Add (stage);

                tblStages.ReloadData();
            });
        }
		partial void BtnAddStageTouchUpInside(NSObject sender)
		{
            AddStage();
		}

        partial void BtnAddQuest(NSObject sender)
        {
            quest = new Quest();
            NSDateFormatter formatter = new NSDateFormatter();
            formatter.DateFormat = "yyyy/MM/dd hh:mm:ss";
            var fecha = formatter.ToString(DpExpiringDate.Date);
            quest.Name = TxtQuestName.Text;
            quest.Description = TxtvQuestDescription.Text;
            quest.StartDate = DateTime.Now;
            quest.ExpiringDate = DateTime.ParseExact(fecha, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            quest.RewardXP = int.Parse(TxtEXP.Text);
            quest.isCompleted = false;
            quest.Status = true;
            quest.QuestStages = stages;
            fb.AddNewQuest(quest);
            //throw new NotImplementedException();
        }
    }
}
