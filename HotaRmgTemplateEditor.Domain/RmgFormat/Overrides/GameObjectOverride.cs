using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat.Handler;
using System.Diagnostics;
using System.Text;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Overrides
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameObjectOverride : IOverrideItem
    {
        public EnableDisableDefault EnableDisable { get; set; }

        public IReadOnlyList<string> ObjectReference { get; set; }
        public GameObject? GameObject { get; set; }

        public int? CustomValue { get; set; }

        public int DefaultFrequency { get; set; }
        public int? CustomFrequency { get; set; }

        public AmountRestriction MaxOnMap { get; set; }
        public int? MaxOnMapAmount { get; set; }

        public AmountRestriction MaxPerZone { get; set; }
        public int? MaxPerZoneAmount { get; set; }

        public GameObjectOverride()
        {
            ObjectReference = [];
        }

        public string GetFormula()
        {
            var sb = new StringBuilder();
            if (EnableDisable == EnableDisableDefault.Enable)
            {
                sb.Append('+');
                sb.Append(string.Join(" ", ObjectReference));

                sb.Append($" {(CustomValue == null ? "d" : CustomValue.ToString())}");
                sb.Append($" {(CustomFrequency == null ? "d" : CustomFrequency.ToString())}");

                if (MaxOnMap == AmountRestriction.Default)
                {
                    sb.Append(" d");
                }
                else if (MaxOnMap == AmountRestriction.Custom)
                {
                    sb.Append($" {MaxOnMapAmount}");
                }
                else
                {
                    sb.Append(" n");
                }

                if (MaxPerZone == AmountRestriction.Default)
                {
                    sb.Append(" d");
                }
                else if (MaxPerZone == AmountRestriction.Custom)
                {
                    sb.Append($" {MaxPerZoneAmount}");
                }
                else
                {
                    sb.Append(" n");
                }
            }
            else if (EnableDisable == EnableDisableDefault.Disable)
            {
                sb.Append('-');
                sb.Append(string.Join(" ", ObjectReference));
            }
            else
            {
                throw new NotImplementedException();
            }

            return sb.ToString();
        }

        private string GetDebuggerDisplay()
        {
            var sb = new StringBuilder();
            if (EnableDisable == EnableDisableDefault.Enable)
            {
                sb.Append('+');
                sb.Append(string.Join(" ", ObjectReference));

                sb.Append($" {(CustomValue == null ? "d" : CustomValue.ToString())}");
                sb.Append($" {(CustomFrequency == null ? "d" : CustomFrequency.ToString())}");

                if (MaxOnMap == AmountRestriction.Default)
                {
                    sb.Append(" d");
                }
                else if (MaxOnMap == AmountRestriction.Custom)
                {
                    sb.Append($" {MaxOnMapAmount}");
                }
                else
                {
                    sb.Append(" n");
                }

                if (MaxPerZone == AmountRestriction.Default)
                {
                    sb.Append(" d");
                }
                else if (MaxPerZone == AmountRestriction.Custom)
                {
                    sb.Append($" {MaxPerZoneAmount}");
                }
                else
                {
                    sb.Append(" n");
                }
            }
            else if (EnableDisable == EnableDisableDefault.Disable)
            {
                sb.Append('-');
                sb.Append(string.Join(" ", ObjectReference));
            }
            else
            {
                throw new NotImplementedException();
            }

            return sb.ToString();
        }
    }
}
