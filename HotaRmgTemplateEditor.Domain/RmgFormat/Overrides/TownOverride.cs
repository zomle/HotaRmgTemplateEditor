using HotaRmgTemplateEditor.Domain.HotaData;
using System.Diagnostics;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    public interface IOverrideItem
    {
        string GetFormula();
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class TownOverride : IOverrideItem
    {
        public Town Town { get; }
        public bool IsAllowed { get; set; }

        public TownOverride(Town town, bool allowed)
        {
            Town = town;
            IsAllowed = allowed;
        }

        public string GetFormula()
        {
            return $"{(IsAllowed ? '+' : '-')}{(int)Town}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{(IsAllowed ? "enabled" : "disabled")} - {Town}";
        }
    }
}
