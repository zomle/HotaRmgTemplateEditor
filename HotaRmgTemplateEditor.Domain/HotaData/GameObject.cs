using System;
using System.Diagnostics;

namespace HotaRmgTemplateEditor.Domain.HotaData
{
	[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
	public class GameObject : GameEntity
	{
		public int? DefaultValue { get; set; }
		public int DefaultFrequency { get; set; }
		public string Name { get; set; }
		public string[] Properties { get; set; }

		public GameObject(int id, string[] properties, string name, int? defaultValue, int defaultFrequency)
			: base(id)
		{
			Properties = properties;
			Name = name;
			DefaultValue = defaultValue;
			DefaultFrequency = defaultFrequency;
		}

		private string GetDebuggerDisplay()
		{
			return $"{Id} [{string.Join(" ", Properties)}] ({(DefaultValue?.ToString() ?? "(null)")}; {DefaultFrequency}) - {Name}";
		}
	}
}
