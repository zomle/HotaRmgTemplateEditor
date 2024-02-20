namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public class SecondarySkill : GameEntity
	{
		public string Name { get; }
		public DefaultAvailability DefaultAvailability { get; }

		public SecondarySkill(int id, string name, DefaultAvailability defaultAvailability = DefaultAvailability.Enabled)
			: base(id)
		{
			Name = name;
			DefaultAvailability = defaultAvailability;
		}
	}
}
