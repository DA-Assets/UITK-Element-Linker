using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkSlider : UitkLinker<Slider> { }

    public class SliderG : UnityEngine.UIElements.Slider, IHaveGuid
    {
        public string guid { get; set; }

        private new class UxmlFactory : UxmlFactory<SliderG, UxmlTraits> { }

        private new class UxmlTraits : BaseSlider<float>.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
            {
                name = "low-value"
            };

            private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
            {
                name = "high-value",
                defaultValue = 10f
            };

            private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
            {
                name = "page-size",
                defaultValue = 0f
            };

            private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
            {
                name = "show-input-field",
                defaultValue = false
            };

            private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
            {
                name = "direction",
                defaultValue = SliderDirection.Horizontal
            };

            private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
            {
                name = "inverted",
                defaultValue = false
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                SliderG obj = (SliderG)ve;
                obj.lowValue = m_LowValue.GetValueFromBag(bag, cc);
                obj.highValue = m_HighValue.GetValueFromBag(bag, cc);
                obj.direction = m_Direction.GetValueFromBag(bag, cc);
                obj.pageSize = m_PageSize.GetValueFromBag(bag, cc);
#if UNITY_2020_1_OR_NEWER
                obj.showInputField = m_ShowInputField.GetValueFromBag(bag, cc);
#endif
#if UNITY_2021_1_OR_NEWER
                obj.inverted = m_Inverted.GetValueFromBag(bag, cc);
#endif
                base.Init(ve, bag, cc);

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
