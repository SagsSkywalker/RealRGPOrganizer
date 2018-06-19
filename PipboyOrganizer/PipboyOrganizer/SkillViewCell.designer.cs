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
	[Register ("SkillViewCell")]
	partial class SkillViewCell
	{
		[Outlet]
		UIKit.UILabel LblDescription { get; set; }

		[Outlet]
		UIKit.UILabel LblSkillLevel { get; set; }

		[Outlet]
		UIKit.UILabel LblSkillName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblSkillLevel != null) {
				LblSkillLevel.Dispose ();
				LblSkillLevel = null;
			}

			if (LblSkillName != null) {
				LblSkillName.Dispose ();
				LblSkillName = null;
			}

			if (LblDescription != null) {
				LblDescription.Dispose ();
				LblDescription = null;
			}
		}
	}
}
