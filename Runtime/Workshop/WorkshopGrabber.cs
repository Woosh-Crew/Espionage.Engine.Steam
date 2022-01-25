using System.Diagnostics;
using System.IO;
using Espionage.Engine.Resources;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	internal static class WorkshopGrabber
	{
		[Callback( "steam.ready" )]
		public static async void Grab()
		{
			Debugging.Log.Info( "Grabbing Workshop Items" );

			// Grab Maps
			var items = await Query.All.WhereUserSubscribed().GetPageAsync( 1 );

			if ( !items.HasValue )
			{
				Debugging.Log.Info( "Doesn't have value" );
				return;
			}

			foreach ( var item in items.Value.Entries )
			{
				foreach ( var path in Directory.GetFiles( item.Directory, "*.map", SearchOption.AllDirectories ) )
				{
					var map = new Map( path );
					map.Components.Add( new WorkshopComponent( item ) );
					Map.Database.Add( map );
				}
			}
		}
	}
}
