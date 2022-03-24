using Espionage.Engine.Components;
using Steamworks;
using Espionage.Engine.Resources;
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
		public void OnAttached( Map item ) { }

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
