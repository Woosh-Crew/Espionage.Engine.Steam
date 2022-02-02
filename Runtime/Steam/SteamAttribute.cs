using System;
using Espionage.Engine.Components;

namespace Espionage.Engine.Steam
{
	[AttributeUsage( AttributeTargets.Class )]
	public sealed class SteamAttribute : Attribute, IComponent<Library>
	{
		public uint AppId { get; }

		public SteamAttribute( uint appId )
		{
			AppId = appId;
		}

		public void OnAttached( Library library ) { }
	}
}
