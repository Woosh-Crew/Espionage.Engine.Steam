using Espionage.Engine.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	/// <summary>
	/// Steam UGC / Workshop Wrapper
	/// </summary>
	[Library, Group( "Steam" )]
	public static class Workshop
	{
		public static void Refresh()
		{
			Grab();
		}

		public static ILoadable Download( Item item )
		{
			if ( item.IsInstalled )
			{
				Dev.Log.Warning( $"Item [{item.Title}] was already installed." );
				return null;
			}

			var download = item.DownloadAsync();
			item.Subscribe();

			return Operation.Create( download, $"Downloading Workshop Item {item.Title}" );
		}

		[Function, Callback( "steam.ready" )]
		private static async void Grab()
		{
			var query = Query.ItemsReadyToUse.WhereUserSubscribed();
			var page = 1;

			// Get all pages
			while ( true )
			{
				var items = await query.GetPageAsync( page );

				if ( !items.HasValue || items.Value.ResultCount == 0 )
				{
					Dev.Log.Info( $"Couldn't find anymore workshop pages where user subscribed. Page Count: [{page - 1}]" );
					break;
				}

				page++;

				foreach ( var item in items.Value.Entries )
				{
					Map.Setup.Workshop( item ).Build();
				}
			}
		}
	}
}
