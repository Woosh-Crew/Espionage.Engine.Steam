using Espionage.Engine.Components;
using Espionage.Engine.Resources;
using Steamworks;
using Steamworks.Ugc;
using UnityEngine.SceneManagement;

namespace Espionage.Engine.Steam.Resources
{
	public class UGC : IComponent<Map>, Map.ICallbacks, IComponent<IResource>
	{
		private Item Item { get; }

		public UGC( Item item )
		{
			Item = item;
		}

		public void OnAttached( IResource item ) { }

		public void OnAttached( Map map )
		{
			// Add Meta, if its not already there.
			if ( !map.Components.Has<Meta>() )
			{
				map.Components.Add( new Meta()
				{
					Title = Item.Title,
					Description = Item.Description,
					Author = Item.Owner.Name
				} );
			}

			// Add Origin, if its not already there.
			if ( !map.Components.Has<Origin>() )
			{
				map.Components.Add( new Origin() { Name = "Steam Workshop" } );
			}

			// Add Thumbnail, if its not already there.
			if ( !map.Components.Has<Thumbnail>() )
			{
				map.Components.Add( new Thumbnail( Item.PreviewImageUrl ) );
			}
		}

		// Map

		public void OnLoad( Scene scene )
		{
			SteamUGC.StartPlaytimeTracking( Item.Id );
		}

		public void OnUnload()
		{
			SteamUGC.StopPlaytimeTracking( Item.Id );
		}
	}
}
