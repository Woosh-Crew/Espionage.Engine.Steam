using System.Diagnostics;
using System.IO;
using Espionage.Engine.Resources;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	[Library, Group( "Steam" )]
	internal static class WorkshopGrabber
	{
		[Function, Callback( "steam.ready" )]
		public static void Grab() { }
	}
}
