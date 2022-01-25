using System;
using System.Collections;
using System.Collections.Generic;
using Espionage.Engine;
using Steamworks;
using UnityEngine;

namespace Espionage.Engine.Steam
{
	internal static class SteamInitializer
	{
		[Callback( "game.ready" ), Callback( "game.not_found" )]
		private static void Initialize()
		{
			uint appId = default;

			if ( Engine.Game is not null && Engine.Game.ClassInfo.Components.TryGet<SteamAttribute>( out var steam ) )
			{
				appId = steam.AppId;
			}
			else
			{
				Debugging.Log.Warning( "No Steam component found on Game, using Espionage's AppId" );
				appId = 1614530;
			}

			try
			{
				SteamClient.Init( appId );
			}
			catch ( Exception e )
			{
				Debugging.Log.Exception( e );
			}

			if ( SteamClient.IsValid )
			{
				Callback.Run( "steam.ready" );
			}
		}
	}
}
