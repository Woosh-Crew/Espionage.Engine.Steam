using Espionage.Engine.Components;
using Steamworks;
using Espionage.Engine.Resources;
using Steamworks.Data;

namespace Espionage.Engine.Steam.Resources
{
	public class UGC : IComponent<Map>, IComponent<IResource>
	{
		private PublishedFileId FileId { get; }
		
		public UGC( ulong fileId )
		{
			FileId = fileId;
		}

		public void OnAttached( IResource item ) { }
		
		public void OnAttached( Map item )
		{
			item.Loaded += () => SteamUGC.StartPlaytimeTracking( FileId );
			item.Unloaded += () => SteamUGC.StopPlaytimeTracking( FileId );
		}
	}
}
