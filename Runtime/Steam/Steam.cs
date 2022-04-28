using System;
using Espionage.Engine.IO;
using Steamworks;

namespace Espionage.Engine.Steam
{
	[Order( 10 ), Editor]
	public class Steam : Module
	{
		// API

		public static void Connect()
		{
			if ( SteamClient.IsValid )
			{
				Debugging.Log.Info( "Steam already connected" );
				return;
			}

			var appId = Engine.Project is not null && Engine.Project.ClassInfo.Components.TryGet<SteamAttribute>( out var steam ) ? steam.AppId : 1614530;

			try
			{
				SteamClient.Init( appId );
			}
			catch ( Exception e )
			{
				Debugging.Log.Exception( e );
				Callback.Run( "steam.failed" );
			}

			if ( SteamClient.IsValid )
			{
				Debugging.Log.Info( $"Steam Connected [Player : {SteamClient.Name}]" );

				// Add the Steam Install to Files
				if ( !Pathing.Contains( "steam" ) )
				{
					Pathing.Add( "steam", SteamApps.AppInstallDir() );
				}
			}
		}

		public static void Disconnect()
		{
			SteamUGC.StopPlaytimeTrackingForAllItems();
			SteamClient.Shutdown();
		}

		// Service

		protected override void OnReady()
		{
			Connect();

			// Don't do anything if init failed
			if ( !SteamClient.IsValid )
			{
				return;
			}

			// Init API Call Exception Callbacks
			Dispatch.OnException = ( e ) =>
			{
				Debugging.Log.Add( new()
				{
					Message = e.Message,
					Level = "Steam Exception",
					Trace = e.StackTrace
				} );
			};

			Callback.Run( "steam.ready" );
		}

		protected override void OnShutdown()
		{
			Disconnect();
		}
	}
}
