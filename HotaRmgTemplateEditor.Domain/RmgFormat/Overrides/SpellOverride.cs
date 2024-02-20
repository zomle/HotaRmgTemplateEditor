using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public class SpellOverride : IOverrideItem
    {
        public Spell Spell { get; }
        public EnableDisableDefault EnableDisable { get; set; }

        public SpellOverride(Spell spell, EnableDisableDefault enableDisable)
        {
            EnableDisable = enableDisable;
            Spell = spell;
        }

        public SpellOverride(int spellId, EnableDisableDefault enableDisable)
            : this(Spells.Lookup[spellId], enableDisable)
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

            return $"{prefix}{Spell.Id}";
        }
    }
}
