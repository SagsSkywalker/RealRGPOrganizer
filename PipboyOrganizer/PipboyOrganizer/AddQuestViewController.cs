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
                if (TxtQuestStage.Text != "")
                {
                    Stage stage = new Stage();
                    stage.Description = TxtQuestStage.Text;
                    stage.isCompleted = false;
                    stages.Add(stage);
                    TxtQuestStage.Text = "";
                    tblStages.ReloadData();

                }
                else
                    showMessage("Warning", "You have to write a stage to add it!", this);
                    
				
            });
        }
		partial void BtnAddStageTouchUpInside(NSObject sender)
		{
            AddStage();
		}
        void showMessage(string title, string message, UIViewController fromViewController)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            fromViewController.PresentViewController(alert, true, null);
        }

        partial void BtnAddQuest(NSObject sender)
        {
            if (TxtQuestName.Text!= "" && TxtEXP.Text != "" && TxtvQuestDescription.Text != "")
            {
                if (stages.Count > 0)
                {
                    if (int.TryParse(TxtEXP.Text, out int num))
                    {
                        quest = new Quest();
                        NSDateFormatter formatter = new NSDateFormatter();
                        formatter.DateFormat = "yyyy/MM/dd hh:mm:ss";
                        var fecha = formatter.ToString(DpExpiringDate.Date);
                        var expiringDate = DateTime.ParseExact(fecha, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
                        if (expiringDate > DateTime.Now){
                            quest.Name = TxtQuestName.Text;
                            quest.Description = TxtvQuestDescription.Text;
                            quest.StartDate = DateTime.Now;
                            quest.ExpiringDate = expiringDate;
                            quest.RewardXP = num;
                            quest.isCompleted = false;
                            quest.Status = true;
                            quest.QuestStages = stages;
                            fb.AddNewQuest(quest);
                        }
                        else
                            showMessage("Warning", "You cannot pick a past date.", this);

                    }
                    else
                        showMessage("Warning", "Experience is a numerical field, you know.", this);
                        
                        
                    
                }
                else
                    showMessage("Warning", "Add one stage at least.", this);
                
            }
            else
                showMessage("Warning", "you missed some fields buddy.", this);
                

            //throw new NotImplementedException();
        }
    }
}
