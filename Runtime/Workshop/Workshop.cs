using System;
using System.Linq;
using Espionage.Engine.Logging;
using Espionage.Engine.Resources;
using Steamworks;
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
			Assert.IsNull( item );

			if ( item.IsInstalled )
			{
				Debugging.Log.Warning( $"Item [{item.Title}] was already installed." );
				return null;
			}

			return new ItemDownloadCallback( item );
		}

		[Function, Callback( "steam.ready" )]
		private static async void Grab()
		{
			var query = Query.ItemsReadyToUse
				.WithTag( "Maps" )
				.MatchAnyTag()
				.WhereUserSubscribed();

			var page = 1;

			// Get all pages
			while ( true )
			{
				var items = await query.GetPageAsync( page );

				if ( !items.HasValue || items.Value.ResultCount == 0 )
				{
					Debugging.Log.Info( $"Couldn't find anymore workshop pages where user subscribed. Page Count: [{page - 1}]" );
					break;
				}

				page++;

				foreach ( var item in items.Value.Entries )
				{
					Map.Setup.Workshop( item )?.Build();
				}
			}
		}

		//
		// ILoadable Callbacks
		//

		private class ItemDownloadCallback : ILoadable
		{
			private Item Item { get; }

			public ItemDownloadCallback( Item item )
			{
				Item = item;
			}

			// Loadable

			public float Progress { get; private set; }
			public string Text => $"Downloading Workshop Item {Item.Title}";

			private void Update( float value )
			{
				Progress = value;
			}

			public async void Load( Action loaded )
			{
				try
				{
					// Subscribe and Download
					await Item.Subscribe();
					await Item.DownloadAsync( Update );
				}
				catch ( Exception e )
				{
					Debugging.Log.Exception( e );
				}

				loaded.Invoke();
			}
		}

		//
		// Commands
		//

		[Function( "workshop.load" ), Terminal]
		private static async void Download_CMD( string name )
		{
			Item? item;

			if ( ulong.TryParse( name, out var value ) )
			{
				item = await SteamUGC.QueryFileAsync( value );
			}
			else
			{
				var page = await Query.ItemsReadyToUse.WhereSearchText( name ).GetPageAsync( 1 );

				if ( !page.HasValue || page.Value.ResultCount == 0 )
				{
					Debugging.Log.Warning( "No Item's could be found." );
					return;
				}

				item = page.Value.Entries.FirstOrDefault();
			}

			if ( item == null )
			{
				Debugging.Log.Warning( "Couldn't find workshop item" );
				return;
			}

			Engine.Game.Loader.Start(
				new Loader.Request( Download( item.Value ) ),
				new Loader.Request( () => Map.Setup.Workshop( item.Value )?.Build() )
			);
		}

		[Function( "workshop.search" ), Terminal]
		private static async void Search_CMD( string name )
		{
			var page = await Query.ItemsReadyToUse.WhereSearchText( name ).GetPageAsync( 1 );

			if ( !page.HasValue || page.Value.ResultCount == 0 )
			{
				Debugging.Log.Warning( "No Item's could be found." );
				return;
			}

			foreach ( var item in page.Value.Entries )
			{
				Debugging.Log.Add( new() { Message = $"{item.Title} [{item.Owner.Name}]", Trace = item.Description } );
			}
		}
	}
}
