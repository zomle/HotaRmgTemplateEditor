namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class Connection
	{
		public int Zone1Id { get; set; }
		public int Zone2Id { get; set; }
		public bool IsMirrorConnection { get { return Zone2Id == -1; } }

		public List<ConnectionLink> Links { get; set; }
		public Connection()
		{
			Links = [];
		}
	}
}
