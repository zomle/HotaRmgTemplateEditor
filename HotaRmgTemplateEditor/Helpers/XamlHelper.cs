using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace HotaRmgTemplateEditor.Helpers
{
    internal static class XamlHelper
    {
		public static T CloneXamlObject<T>(object obj)
		{
			var str = SerializeXamlObject(obj);
			var other = DeserializaXamlObject(str);
			return (T)other;
		}

		private static string SerializeXamlObject(object obj)
		{
			var sb = new StringBuilder();
			using var writer = XmlWriter.Create(sb, new XmlWriterSettings
			{
				Indent = true,
				ConformanceLevel = ConformanceLevel.Fragment,
				OmitXmlDeclaration = true,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
			});

			var mgr = new XamlDesignerSerializationManager(writer)
			{
				XamlWriterMode = XamlWriterMode.Expression
			};

			XamlWriter.Save(obj, mgr);
			return sb.ToString();
		}

		private static object DeserializaXamlObject(string xamlString)
		{
			using var stringReader = new StringReader(xamlString);
			using var xmlReader = XmlReader.Create(stringReader);
			return XamlReader.Load(xmlReader);
		}
	}
}
