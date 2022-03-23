using System.Linq;
using Espionage.Engine.Resources;
using Espionage.Engine.Steam.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	[Library, Group( "Steam" )]
	internal static class WorkshopGrabber
	{
		[Function, Callback( "steam.ready" )]
		public static async void Grab()
		{
			var extensions = Library.Database.GetAll<Map.File>().Select( e => e.Components.Get<FileAttribute>()?.Extension ).ToArray();
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
					if ( !item.IsInstalled || string.IsNullOrEmpty( item.Directory ) )
					{
						continue;
					}

					var path = Files.Pathing.All( item.Directory, extensions ).FirstOrDefault();

					if ( !string.IsNullOrEmpty( path ) )
					{
						Map.Setup( path )
							.Meta( item.Title, item.Description, item.Owner.Name )
							.Origin( "Steam Workshop" )
							.With<UGC>( new( item ) )
							.Build();
					}
				}
			}
		}
	}
}
