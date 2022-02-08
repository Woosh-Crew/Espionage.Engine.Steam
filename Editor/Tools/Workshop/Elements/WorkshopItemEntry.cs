using Espionage.Engine.Editor;
using Steamworks.Ugc;
using UnityEngine.UIElements;

namespace Espionage.Engine.Steam.Editor.Elements
{
	public class WorkshopItemEntry : Element
	{
		public Item Item { get; }

		public WorkshopItemEntry( Item item )
		{
			Item = item;

			// Image
			var image = new Image();
			Add( image );

			var metaContainer = new VisualElement();
			metaContainer.style.flexDirection = FlexDirection.Column;
			Add( metaContainer );

			// Text
			metaContainer.Add( new Label( item.Title ) );
			metaContainer.Add( new Label( item.Description ) );
		}
	}
}
