using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkRepeatButton : UitkLinker<RepeatButton> { }

    public class RepeatButtonG : UnityEngine.UIElements.RepeatButton, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<RepeatButtonG, UxmlTraits> { }

        public new class UxmlTraits : TextElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
            {
                name = "delay"
            };

            private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
            {
                name = "interval"
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                RepeatButtonG obj = (RepeatButtonG)ve;
                obj.SetAction(null, m_Delay.GetValueFromBag(bag, cc), m_Interval.GetValueFromBag(bag, cc));

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}