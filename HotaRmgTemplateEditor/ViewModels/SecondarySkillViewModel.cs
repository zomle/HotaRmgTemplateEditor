using HotaRmgTemplateEditor.Domain.HotaData;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class SecondarySkillViewModel : EnableDisableItemViewModel
	{
		public string Name { get { return BaseSecondarySkill.Name; } }
		public override DefaultAvailability DefaultAvailability { get { return BaseSecondarySkill.DefaultAvailability; } }

		public SecondarySkill BaseSecondarySkill { get; }

		public SecondarySkillViewModel(SecondarySkill baseSkill)
		{
			BaseSecondarySkill = baseSkill;
		}
	}
}
