namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class ConnectionLink
	{
		public int Value { get; set; }
		public BorderGuard? BorderGuard { get; set; }
		public ConnectionType Type { get; set; }
		public ConnectionPlacementHint PlacementHint { get; set; }
		public ConnectionRoad Roads { get; set; }
		public bool PortalRepulsion { get; set; }

		public ConnectionRestriction Restriction { get; set; }

		public ConnectionLink()
		{
			Restriction = new ConnectionRestriction();
		}
	}
}
