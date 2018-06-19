// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PipboyOrganizer
{
	[Register ("AddQuestViewController")]
	partial class AddQuestViewController
	{
		[Outlet]
		UIKit.UIButton BtnAddStage { get; set; }

		[Outlet]
		UIKit.UIButton BtnCreateQuest { get; set; }

		[Outlet]
		UIKit.UIButton BtnCreateQuestTouchUpInside { get; set; }

		[Outlet]
		UIKit.UIDatePicker DpExpiringDate { get; set; }

		[Outlet]
		UIKit.UITableView tblStages { get; set; }

		[Outlet]
		UIKit.UITextField TxtEXP { get; set; }

		[Outlet]
		UIKit.UITextField TxtQuestName { get; set; }

		[Outlet]
		UIKit.UITextField TxtQuestStage { get; set; }

		[Outlet]
		UIKit.UITextView TxtvQuestDescription { get; set; }

		[Action ("BtnAddQuest:")]
		partial void BtnAddQuest (Foundation.NSObject sender);

		[Action ("BtnAddStageTouchUpInside:")]
		partial void BtnAddStageTouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnAddStage != null) {
				BtnAddStage.Dispose ();
				BtnAddStage = null;
			}

			if (BtnCreateQuest != null) {
				BtnCreateQuest.Dispose ();
				BtnCreateQuest = null;
			}

			if (BtnCreateQuestTouchUpInside != null) {
				BtnCreateQuestTouchUpInside.Dispose ();
				BtnCreateQuestTouchUpInside = null;
			}

			if (DpExpiringDate != null) {
				DpExpiringDate.Dispose ();
				DpExpiringDate = null;
			}

			if (tblStages != null) {
				tblStages.Dispose ();
				tblStages = null;
			}

			if (TxtEXP != null) {
				TxtEXP.Dispose ();
				TxtEXP = null;
			}

			if (TxtQuestName != null) {
				TxtQuestName.Dispose ();
				TxtQuestName = null;
			}

			if (TxtQuestStage != null) {
				TxtQuestStage.Dispose ();
				TxtQuestStage = null;
			}

			if (TxtvQuestDescription != null) {
				TxtvQuestDescription.Dispose ();
				TxtvQuestDescription = null;
			}
		}
	}
}
