using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public class SecondarySkillOverride : IOverrideItem
    {
        public SecondarySkill SecondarySkill { get; }
        public EnableDisableDefault EnableDisable { get; set; }

        public SecondarySkillOverride(SecondarySkill secondarySkill, EnableDisableDefault enableDisable)
        {
            EnableDisable = enableDisable;
            SecondarySkill = secondarySkill;
        }

        public SecondarySkillOverride(int secondarySkillId, EnableDisableDefault enableDisable)
            : this(SecondarySkills.Lookup[secondarySkillId], enableDisable)
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

            return $"{prefix}{SecondarySkill.Id}";
        }
    }
}
