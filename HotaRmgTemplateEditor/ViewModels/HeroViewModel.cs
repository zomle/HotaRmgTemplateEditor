using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class HeroViewModel : EnableDisableItemViewModel
	{
		public string TownName { get { return BaseHero.Town.ToString(); } }
		public string NameAndSpecialty { get { return $"{BaseHero.Name} ({BaseHero.Specialty})"; } }
		public override DefaultAvailability DefaultAvailability { get { return BaseHero.DefaultAvailability; } }

		public Hero BaseHero { get; }

		public HeroViewModel(Hero baseHero)
		{
			BaseHero = baseHero;   
		}
	}
}
