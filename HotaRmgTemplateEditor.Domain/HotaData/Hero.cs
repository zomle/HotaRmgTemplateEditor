namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public class Hero : GameEntity
	{
		public Town Town { get; }
		public string Name { get; }
		public string Specialty { get; }

		public DefaultAvailability DefaultAvailability { get; }

		public Hero(int id, Town town, string name, string specialty, DefaultAvailability availability = DefaultAvailability.Enabled)
			: base(id)
		{
			Town = town;
			Name = name;
			Specialty = specialty;

			DefaultAvailability = availability;
		}
	}
}
