using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;

namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class Zone
	{
		public int Id { get; set; }
		public ZoneType Type { get; set; }
		public int Size { get; set; }

		public ZonePlacement Placement { get; set; }
		public bool ZoneRepulsion { get; set; }
		public bool AllowNonCoherentRoad { get; set; }
		public List<TownTypeHint> TownHints { get; set; }

		public ZoneRestriction Restrictions { get; set; }
		public ZonePlayerTown PlayerTowns { get; set; }
		public ZoneNeutralTown NeutralTowns { get; set; }
		public ZoneTownType AllowedTowns { get; set; }
		public ZoneMine Mines { get; set; }
		public ZoneTerrain Terrain { get; set; }
		public ZoneMonster Monsters { get; set; }
		public ZoneTreasure Treasures { get; set; }
		public List<GameObjectOverride> ObjectOverrides { get; set; }
		public object MinimumObjects { get; set; }
		public ZoneDisplayCoordinate ImageSettings { get; set; }

		public Zone()
		{
			ObjectOverrides = [];
			TownHints = [];

			Restrictions = new ZoneRestriction();
			PlayerTowns = new ZonePlayerTown();
			NeutralTowns = new ZoneNeutralTown();
			AllowedTowns = new ZoneTownType();
			Mines = new ZoneMine();
			Terrain = new ZoneTerrain();
			Monsters = new ZoneMonster();
			Treasures = new ZoneTreasure();
		}
	}
}
