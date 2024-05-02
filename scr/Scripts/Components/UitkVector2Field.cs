using UnityEngine;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkVector2Field : UitkLinker<Vector2Field> { }

    public class Vector2FieldG : UnityEngine.UIElements.Vector2Field, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<Vector2FieldG, UxmlTraits> { }

        public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
            {
                name = "x"
            };

            private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
            {
                name = "y"
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                Vector2FieldG obj = (Vector2FieldG)ve;
                obj.SetValueWithoutNotify(new Vector2(m_XValue.GetValueFromBag(bag, cc), m_YValue.GetValueFromBag(bag, cc)));

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
