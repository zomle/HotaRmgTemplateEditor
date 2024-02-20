using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;

namespace HotaRmgTemplateEditor.Domain.RmgFormat
{

	public class Template
	{
		public TemplatePack OwnerPack { get; }

		public string Name { get; set; }
		public MapSize MinimumSize { get; set; }
		public MapSize MaximumSize { get; set; }
		public object? ComboArtifacts { get; set; }
		public Dictionary<int, ArtifactOverride> ArtifactOverrides { get; set; }
		public Dictionary<int, SpellOverride> SpellOverrides { get; set; }
		public Dictionary<int, SecondarySkillOverride> SecondarySkillOverrides { get; set; }
		public List<GameObjectOverride> ObjectOverrides { get; set; }
		public RockBlockRadius RockBlockRadius { get; set; }
		public ZoneSparseness ZoneSparseness { get; set; }
		public bool SpecialWeeksDisabled { get; set; }
		public bool SpellResearch { get; set; }
		public bool Anarchy { get; set; }

		public List<Zone> Zones { get; set; }
		public List<Connection> Connections { get; set; }

		public Template(TemplatePack ownerPack)
		{
			OwnerPack = ownerPack;

			Name = string.Empty;

			Zones = [];
			Connections = [];
			ArtifactOverrides = Artifacts.All.ToDictionary(a => a.Id, a => new ArtifactOverride(a, EnableDisableDefault.Default));
			SpellOverrides = Spells.All.ToDictionary(s => s.Id, s => new SpellOverride(s, EnableDisableDefault.Default));
			SecondarySkillOverrides = SecondarySkills.All.ToDictionary(s => s.Id, s => new SecondarySkillOverride(s, EnableDisableDefault.Default));
			ObjectOverrides = [];

			RockBlockRadius = new RockBlockRadius();
			ZoneSparseness = new ZoneSparseness();
		}
	}
}
