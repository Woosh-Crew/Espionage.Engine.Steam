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
		public Item Owner { get; set; }


		//
		// Map
		//

		public void OnAttached( ref Map map ) { }

		public void OnLoad()
		{
			SteamUGC.StartPlaytimeTracking( Owner.Id );
		}

		public void OnUnload()
		{
			SteamUGC.StopPlaytimeTracking( Owner.Id );
		}
	}
}
