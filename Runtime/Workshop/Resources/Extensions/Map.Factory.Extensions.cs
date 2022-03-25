using System.Linq;
using Espionage.Engine.Resources;
using Espionage.Engine.Steam.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	public static class MapFactoryExtensions
	{
		public static Map.Builder Workshop( this Map.Factory factory, Item item )
		{
			var extensions = Library.Database.GetAll<Map.File>().Select( e => e.Components.Get<FileAttribute>()?.Extension ).ToArray();
			var path = Files.Pathing.All( item.Directory, extensions ).FirstOrDefault();

			return Map.Setup.Path( path )
				.Meta( item.Title, item.Description, item.Owner.Name )
				.Origin( "Steam Workshop" )
				.With<UGC>( new( item ) );
		}
	}
}
