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
	[Register ("MainTableViewCell")]
	partial class MainTableViewCell
	{
		[Outlet]
		UIKit.UIButton BtnQuestInfo { get; set; }

		[Outlet]
		UIKit.UILabel LblLastStage { get; set; }

		[Outlet]
		UIKit.UILabel LblQuestDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblQuestName { get; set; }

		[Outlet]
		UIKit.UITableView TblInside { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblLastStage != null) {
				LblLastStage.Dispose ();
				LblLastStage = null;
			}

			if (LblQuestDescription != null) {
				LblQuestDescription.Dispose ();
				LblQuestDescription = null;
			}

			if (LblQuestName != null) {
				LblQuestName.Dispose ();
				LblQuestName = null;
			}

			if (TblInside != null) {
				TblInside.Dispose ();
				TblInside = null;
			}

			if (BtnQuestInfo != null) {
				BtnQuestInfo.Dispose ();
				BtnQuestInfo = null;
			}
		}
	}
}
