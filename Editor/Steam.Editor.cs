using Espionage.Engine.Editor.Resources;
using UnityEditor;

namespace Espionage.Engine.Steam.Editor
{
	[Library]
	internal static class SteamEditor
	{
		[Function, Callback( "editor.game_constructed" )]
		private static void Init()
		{
			Steam.Connect();
		}
		
		[MenuItem( "Tools/Espionage.Engine/Testing Target/Steam" )]
		private static void ToggleAction()
		{
			Tester.Application = "steam://<executable>";
		}

		[MenuItem( "Tools/Espionage.Engine/Testing Target/Steam", true )]
		private static bool ToggleActionValidate()
		{
			UnityEditor.Menu.SetChecked( "Tools/Espionage.Engine/Testing Target/Steam", Tester.Application == "steam://<executable>" );
			return true;
		}
	}
}
