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
	[Register ("QuestViewController")]
	partial class QuestViewController
	{
		[Outlet]
		UIKit.UITableView tblQuests { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblQuests != null) {
				tblQuests.Dispose ();
				tblQuests = null;
			}
		}
	}
}
