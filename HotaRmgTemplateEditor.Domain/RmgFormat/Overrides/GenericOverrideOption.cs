namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public class GenericOverrideOption
    {
        public EnableDisableDefault EnableDisable { get; set; }
        public List<string> Tokens { get; set; }
        public GenericOverrideOption()
        {
            Tokens = [];
        }
    }
}
