using System;
using Espionage.Engine.Services;
using Steamworks;

namespace Espionage.Engine.Steam
{
	[Order( 10 )]
	internal class SteamService : Service
	{
		public override void OnReady()
		{
			var appId = Engine.Game is not null && Engine.Game.ClassInfo.Components.TryGet<SteamAttribute>( out var steam ) ? steam.AppId : 1614530;

			try
			{
				SteamClient.Init( appId );
			}
			catch ( Exception e )
			{
				Dev.Log.Exception( e );
				Callback.Run( "steam.failed" );
			}

			if ( SteamClient.IsValid )
			{
				// Add the Steam Install to Files
				if ( !Files.Pathing.Contains( "steam" ) )
				{
					Files.Pathing.Add( "steam", SteamApps.AppInstallDir() );
				}

				Callback.Run( "steam.ready" );
			}
		}

		public override void OnShutdown()
		{
			SteamUGC.StopPlaytimeTrackingForAllItems();
			SteamClient.Shutdown();
		}


		public override void OnUpdate()
		{
			// We would run callbacks
			// But Facepunch.Steamworks
			// Does that for us.
		}
	}
}
