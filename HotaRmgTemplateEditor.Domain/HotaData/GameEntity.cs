namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public abstract class GameEntity
	{
		public int Id { get; }

		public GameEntity(int id)
		{
			Id = id;
		}
	}
}
