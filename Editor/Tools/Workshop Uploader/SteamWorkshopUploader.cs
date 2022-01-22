using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Espionage.Engine.Tools.Editor;
using Espionage.Engine.Editor;
using Tool = Espionage.Engine.Tools.Editor.Tool;

namespace Espionage.Engine.Steam.Editor.Tools
{
    [Library( "tool.workshop_uploader", Title = "Workshop Uploader", Help = "Publish content to the Steam Workshop", Group = "Publish" ), Icon( EditorIcons.Dashboard ), HelpURL( "https://github.com/Woosh-Crew/Espionage.Engine/wiki" )]
    public class SteamWorkshopUploader : Tool
    {
        [MenuItem( "Tools/Workshop Uploader _F6", false, -150 )]
        private static void ShowEditor( )
        {
            var wind = GetWindow<SteamWorkshopUploader>( );
        }
    }
}