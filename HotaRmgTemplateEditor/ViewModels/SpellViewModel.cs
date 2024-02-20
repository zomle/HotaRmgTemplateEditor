using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class SpellViewModel : EnableDisableItemViewModel
	{
		public string School { get { return BaseSpell.School.ToString(); } }
		public string Name { get { return BaseSpell.Name; } }
		public override DefaultAvailability DefaultAvailability { get { return BaseSpell.DefaultAvailability; } }

		public Spell BaseSpell { get; }

		public SpellViewModel(Spell baseSpell)
		{
			BaseSpell = baseSpell;
		}
	}
}
