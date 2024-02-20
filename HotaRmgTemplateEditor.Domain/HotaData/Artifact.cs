namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public enum ArtifactClass
	{
		Treasure,
		Minor,
		Major,
		Relic
	}

	public class Artifact : GameEntity
	{
		public string Name { get; }
		public ArtifactClass Class { get; }
		public DefaultAvailability DefaultAvailability { get; }

		public Artifact(int id, string name, ArtifactClass artiClass, DefaultAvailability availability = DefaultAvailability.Enabled)
			: base(id)
		{
			Name = name;
			Class = artiClass;
			DefaultAvailability = availability;
		}
	}
}
