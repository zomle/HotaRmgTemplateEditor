namespace HotaRmgTemplateEditor.Domain.HotaData
{

	public class Spell : GameEntity
	{
		public string Name { get; }
		public MagicSchool School { get; }
		public DefaultAvailability DefaultAvailability { get; }

		public Spell(int id, string name, MagicSchool school, DefaultAvailability availability = DefaultAvailability.Enabled)
			: base(id)
		{
			Name = name;
			School = school;
			DefaultAvailability = availability;
		}
	}
}
