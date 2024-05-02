using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkRadioButtonGroup : UitkLinker<RadioButtonGroup> { }

    public class RadioButtonGroupG : UnityEngine.UIElements.RadioButtonGroup, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<RadioButtonGroupG, UxmlTraits> { }

        public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
            {
                name = "choices"
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                RadioButtonGroupG obj = (RadioButtonGroupG)ve;
                obj.choices = ParseChoiceList(m_Choices.GetValueFromBag(bag, cc));

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }

        internal static List<string> ParseChoiceList(string choicesFromBag)
        {
            if (string.IsNullOrEmpty(choicesFromBag.Trim()))
                return null;

            string[] array = choicesFromBag.Split(',');

            if (array.Length != 0)
            {
                List<string> list = new List<string>();
                string[] array2 = array;

                foreach (string text in array2)
                    list.Add(text.Trim());

                return list;
            }

            return null;
        }
    }
}
