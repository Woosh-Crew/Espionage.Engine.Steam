using Espionage.Engine.Components;
using Espionage.Engine.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	public class SteamUgcComponent : IComponent<Map>
	{
		public Item Item { get; }

		public SteamUgcComponent( Item item )
		{
			Item = item;
		}

		public void OnAttached( Map map )
		{
			map.Title = Item.Title;
			map.Description = Item.Description;
		}
	}
}
