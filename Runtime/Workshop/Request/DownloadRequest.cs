using System;

namespace Espionage.Engine.Steam.Request
{
	public class DownloadRequest : ILoadable
	{
		public float Progress { get; }
		public string Text { get; }
		
		public void Load( Action loaded = null )
		{
		}
	}
}
