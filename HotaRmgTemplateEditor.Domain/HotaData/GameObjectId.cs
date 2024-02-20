using System.Diagnostics.CodeAnalysis;

namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public record GameObjectId : IEqualityComparer<GameObjectId>
	{
		public readonly IReadOnlyList<object> Ids;

		public readonly IReadOnlyList<string> IdsAsString;
		public GameObjectId(int categoryId, params object[] subIds)
		{
			Ids = new List<object>() { categoryId };
			IdsAsString = new List<string>() { categoryId.ToString() };

			foreach (var subId in subIds)
			{
				((List<object>)Ids).Add(subId);
				((List<string>)IdsAsString).Add(subId.ToString());
			}
		}

		public bool Equals(GameObjectId? x, GameObjectId? y)
		{

			if (ReferenceEquals(x, y))
			{
				return true;
			}

			if (x is null || y is null)
			{
				return false;
			}

			if (x.Ids.Count != y.Ids.Count)
			{
				return false;
			}

			return x.Ids.SequenceEqual(y.Ids);
		}

		public int GetHashCode([DisallowNull] GameObjectId obj)
		{
			var result = 0;
			foreach (var item in Ids)
			{
				result = HashCode.Combine(item, result);
			}
			return result;
		}
	}
}
