using System.Diagnostics;
using System.IO;
using Espionage.Engine.Resources;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	[Library]
	internal static class WorkshopGrabber
	{
		[Function, Callback( "steam.ready" )]
		public static async void Grab()
		{
			// Grab Maps
			var items = await Query.All.WhereUserSubscribed().GetPageAsync( 1 );

			if ( !items.HasValue )
			{
				return;
			}

			foreach ( var item in items.Value.Entries )
			{
				foreach ( var path in Directory.GetFiles( item.Directory, "*.map", SearchOption.AllDirectories ) )
				{
					var map = new Map( new AssetBundleMapProvider( new FileInfo( path ) ) );
					map.Components.Add( new SteamUgcComponent( item ) );
					Map.Database.Add( map );
				}
			}
		}
	}
}
