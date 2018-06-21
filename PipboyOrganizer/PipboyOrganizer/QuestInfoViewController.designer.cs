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
	[Register ("QuestInfoViewController")]
	partial class QuestInfoViewController
	{
		[Outlet]
		UIKit.UICollectionView cvSkillsAfected { get; set; }

		[Outlet]
		UIKit.UILabel LblDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblTitle { get; set; }

		[Outlet]
		UIKit.UITableView tblStages { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblStages != null) {
				tblStages.Dispose ();
				tblStages = null;
			}

			if (cvSkillsAfected != null) {
				cvSkillsAfected.Dispose ();
				cvSkillsAfected = null;
			}

			if (LblTitle != null) {
				LblTitle.Dispose ();
				LblTitle = null;
			}

			if (LblDescription != null) {
				LblDescription.Dispose ();
				LblDescription = null;
			}
		}
	}
}
