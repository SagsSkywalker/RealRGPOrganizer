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
	[Register ("ScheduleViewController")]
	partial class ScheduleViewController
	{
		[Outlet]
		UIKit.UICollectionView cvSkills { get; set; }

		[Action ("AddSkill_TouchUpInside:")]
		partial void AddSkill_TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cvSkills != null) {
				cvSkills.Dispose ();
				cvSkills = null;
			}
		}
	}
}
