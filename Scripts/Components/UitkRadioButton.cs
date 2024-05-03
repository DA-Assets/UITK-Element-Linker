#if UNITY_2021_1_OR_NEWER
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkRadioButton : UitkLinker<RadioButton> { }

    public class RadioButtonG : UnityEngine.UIElements.RadioButton, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<RadioButtonG, UxmlTraits> { }

        public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
            {
                name = "text"
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((RadioButtonG)ve).text = m_Text.GetValueFromBag(bag, cc);

                RadioButtonG obj = ve as RadioButtonG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
#endif