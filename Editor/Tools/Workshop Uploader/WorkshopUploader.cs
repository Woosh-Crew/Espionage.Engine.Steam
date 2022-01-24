using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Espionage.Engine.Tools.Editor;
using Espionage.Engine.Editor;

namespace Espionage.Engine.Steam.Editor.Tools
{
	[Title("Workshop Uploader"), Group("Publish" )]
	[Icon( EditorIcons.Dashboard ), HelpURL( "https://github.com/Woosh-Crew/Espionage.Engine/wiki" )]
	public class WorkshopUploader : EditorTool
	{
		[MenuItem( "Tools/Workshop Uploader _F6", false, -150 )]
		private static void ShowEditor()
		{
			GetWindow<WorkshopUploader>();
		}
	}
}
