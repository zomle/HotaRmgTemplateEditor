namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public class Creature : GameEntity
	{
		public string Name { get; }
		public Town Town { get; }

		public Creature(int id, string name, Town town)
			: base(id)
		{
			Name = name;
			Town = town;
		}
	}
}
