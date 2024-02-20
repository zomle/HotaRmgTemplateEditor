using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;

namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class TemplatePackOptions
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public Dictionary<Town, TownOverride> TownSelection { get; set; }
		public Dictionary<int, HeroOverride> HeroOverrides { get; set; }
		public bool Mirror { get; set; }
		public int MaxBattleRounds { get; set; }
		public string? Tags { get; set; }
		public bool MaxBattleRoundsOverridden { get;  set; }

		public TemplatePackOptions()
		{
			HeroOverrides = [];
			TownSelection = [];
		}
	}
}
