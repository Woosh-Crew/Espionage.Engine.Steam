using Espionage.Engine.Components;
using Steamworks;
using Espionage.Engine.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam.Resources
{
	public class UGC : IComponent<Map>, IComponent<IResource>
	{
		private Item Item { get; }

		public UGC( Item item )
		{
			Item = item;
		}

		public void OnAttached( IResource item ) { }

		public void OnAttached( Map item )
		{
			item.Loaded += () => SteamUGC.StartPlaytimeTracking( Item.Id );
			item.Unloaded += () => SteamUGC.StopPlaytimeTracking( Item.Id );
		}
	}
}
