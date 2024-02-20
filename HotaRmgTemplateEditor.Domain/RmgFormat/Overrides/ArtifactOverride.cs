using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public class ArtifactOverride : IOverrideItem
    {
        public Artifact Artifact { get; }

        public EnableDisableDefault EnableDisable { get; set; }

        public ArtifactOverride(Artifact artifact, EnableDisableDefault enableDisable)
        {
            EnableDisable = enableDisable;
            Artifact = artifact;
        }

        public ArtifactOverride(int artifactId, EnableDisableDefault enableDisable)
            : this(Artifacts.Lookup[artifactId], enableDisable)
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

            return $"{prefix}{Artifact.Id}";
        }
    }
}
