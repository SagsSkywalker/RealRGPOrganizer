﻿// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PipboyOrganizer
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UICollectionView cvSkills { get; set; }

		[Outlet]
		UIKit.UIImageView ImgUser { get; set; }

		[Outlet]
		UIKit.UILabel LblUserLevel { get; set; }

		[Outlet]
		UIKit.UILabel LblUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImgUser != null) {
				ImgUser.Dispose ();
				ImgUser = null;
			}

			if (LblUserLevel != null) {
				LblUserLevel.Dispose ();
				LblUserLevel = null;
			}

			if (LblUserName != null) {
				LblUserName.Dispose ();
				LblUserName = null;
			}

			if (cvSkills != null) {
				cvSkills.Dispose ();
				cvSkills = null;
			}
		}
	}
}
