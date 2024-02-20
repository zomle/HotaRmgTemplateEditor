using System.Diagnostics;

namespace HotaRmgTemplateEditor.Domain.RmgFormat
{

	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class ZoneDisplayCoordinate
	{
		public float X { get; set; }
		public float Y { get; set; }

		public ZoneDisplayCoordinate(float x, float y)
		{
			X = x;
			Y = y;
		}

		private string GetDebuggerDisplay()
		{
			return $"({X}; {Y})";
		}
	}
}
