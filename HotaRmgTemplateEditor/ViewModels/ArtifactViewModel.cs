using HotaRmgTemplateEditor.Domain.HotaData;


namespace HotaRmgTemplateEditor.ViewModels
{
	public class ArtifactViewModel : EnableDisableItemViewModel
	{
		public string ArtiClass { get { return BaseArtifact.Class.ToString(); } }
		public string Name { get { return BaseArtifact.Name; } }
		public override DefaultAvailability DefaultAvailability { get { return BaseArtifact.DefaultAvailability; } }
		public Artifact BaseArtifact { get; }

		public ArtifactViewModel(Artifact baseArtifact)
		{
			BaseArtifact = baseArtifact;
		}
	}
}
