using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public class HeroOverride : IOverrideItem
    {
        public Hero Hero { get; }
        public EnableDisableDefault EnableDisable { get; set; }

        public HeroOverride(Hero hero, EnableDisableDefault enableDisable)
        {
            EnableDisable = enableDisable;
            Hero = hero;
        }

        public HeroOverride(int heroId, EnableDisableDefault enableDisable)
            : this(Heroes.Lookup[heroId], enableDisable)
        {
        }

        public string GetFormula()
        {
            var prefix = EnableDisable switch
            {
                EnableDisableDefault.Default => throw new NotImplementedException(),
                EnableDisableDefault.Enable => '+',
                EnableDisableDefault.Disable => '-',
                _ => throw new InvalidOperationException(),
            };

            return $"{prefix}{Hero.Id}";
        }
    }
}
