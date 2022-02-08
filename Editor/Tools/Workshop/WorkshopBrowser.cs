using System.Linq;
using Espionage.Engine.Editor;
using Espionage.Engine.Steam.Editor.Elements;
using Espionage.Engine.Tools.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Espionage.Engine.Steam.Editor
{
	[Title( "Workshop Browser" ), Group( "Steam" ), Help( "Browse the Workshop" ) ]
	[Icon( EditorIcons.Dashboard ), HelpURL( "https://github.com/Woosh-Crew/Espionage.Engine.Steam" )]
	public class WorkshopBrowser : EditorTool
	{
		//
		// GUI
		//

		protected override void OnCreateGUI()
		{
			var splitView = new TwoPaneSplitView( 0, 250, TwoPaneSplitViewOrientation.Horizontal );
			splitView.Add( TreeViewGUI() );
			splitView.Add( ContentGUI() );

			rootVisualElement.Add( splitView );
		}

		private VisualElement TreeViewGUI()
		{
			var root = new VisualElement();

			root.Add( new HeaderBar( "Workshop", ClassInfo.Help, null,"Bottom" ) );

			return root;
		}

		private VisualElement ContentGUI()
		{
			var root = new VisualElement();

			var scrollView = new ScrollView();
			root.Add(scrollView);

			return root;
		}
	}
}
