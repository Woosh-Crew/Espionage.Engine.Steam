using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Espionage.Engine.Resources;
using Steamworks;
using Steamworks.Ugc;

namespace Espionage.Engine.Steam
{
	public class WorkshopComponent : Map.IComponent
	{
		public Item Item { get; }

		public WorkshopComponent( Item item )
		{
			Item = item;
		}

		//
		// Map
		//

		public void OnAttached( ref Map map )
		{
			map.Title = Item.Title;
			map.Description = Item.Description;
		}

		public void OnLoad()
		{
			SteamUGC.StartPlaytimeTracking( Item.Id );
		}

		public void OnUnload()
		{
			SteamUGC.StopPlaytimeTracking( Item.Id );
		}
	}
}
