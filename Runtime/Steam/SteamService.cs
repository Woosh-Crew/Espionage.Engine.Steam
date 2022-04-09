using System;
using Espionage.Engine.Logging;
using Espionage.Engine.Services;
using Steamworks;

namespace Espionage.Engine.Steam
{
	[Order( 10 )]
	internal class SteamService : Service
	{
		#if UNITY_EDITOR

		[Function, Callback( "editor.game_constructed" )]
		private static void Init()
		{
			Connect();
		}

		#endif

		public override void OnReady()
		{
			Connect();

			// Don't do anything if init failed
			if ( !SteamClient.IsValid )
			{
				return;
			}

			// Add the Steam Install to Files
			if ( !Files.Pathing.Contains( "steam" ) )
			{
				Files.Pathing.Add( "steam", SteamApps.AppInstallDir() );
			}

			// Init Logging Callbacks
			Dispatch.OnDebugCallback = ( type, str, server ) =>
			{
				if ( str == $"[{(int)type} not in sdk]" || type is CallbackType.PersonaStateChange or CallbackType.SteamAPICallCompleted )
				{
					return;
				}

				Debugging.Log.Add( new()
				{
					Message = $"{type}",
					Level = "Steam Info",
					Trace = str
				} );
			};

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

		public static void Connect()
		{
			if ( SteamClient.IsValid )
			{
				Debugging.Log.Info( "Steam already connected" );
				return;
			}

			var appId = Engine.Game is not null && Engine.Game.ClassInfo.Components.TryGet<SteamAttribute>( out var steam ) ? steam.AppId : 1614530;

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
			}
		}

		public static void Disconnect()
		{
			SteamUGC.StopPlaytimeTrackingForAllItems();
			SteamClient.Shutdown();
		}


		public override void OnShutdown()
		{
			Disconnect();
		}

		public override void OnUpdate()
		{
			// We would run callbacks
			// But Facepunch.Steamworks
			// Does that for us.
		}
	}
}
