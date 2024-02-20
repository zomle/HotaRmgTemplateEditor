namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class TemplatePack
	{
		public TemplatePackFieldCount FieldCount { get; set; }
		public TemplatePackOptions Options { get; set; }

		public List<Template> Templates { get; set; }
		public TemplatePack()
		{
			FieldCount = new TemplatePackFieldCount();
			Options = new TemplatePackOptions();

			Templates = [];
		}
	}
}
