using System.IO;
using System.Linq;
using Espionage.Engine.Resources;
using Espionage.Engine.Steam.Resources;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	public static class MapFactoryExtensions
	{
		public static Map.Builder? Workshop( this Map.Factory factory, Item item )
		{
			var extensions = Library.Database.GetAll<Map.File>().Select( e => e.Components.Get<FileAttribute>()?.Extension ).ToArray();
			var path = Files.Pathing( item.Directory ).All( SearchOption.AllDirectories, extensions ).FirstOrDefault();

			if ( path.Output.IsEmpty() )
			{
				Debugging.Log.Warning( $"{item.Title} did not contain a valid map." );
				return null;
			}

			return Map.Setup.Path( path )?
				.With<UGC>( new( item ) );
		}
	}
}
