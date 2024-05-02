using UnityEngine;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkVector3IntField : UitkLinker<Vector3IntField> { }

    public class Vector3IntFieldG : UnityEngine.UIElements.Vector3IntField, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<Vector3IntFieldG, UxmlTraits> { }

        public new class UxmlTraits : BaseField<Vector3Int>.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
            {
                name = "x"
            };

            private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
            {
                name = "y"
            };

            private UxmlIntAttributeDescription m_ZValue = new UxmlIntAttributeDescription
            {
                name = "z"
            };

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                Vector3IntFieldG obj = (Vector3IntFieldG)ve;
                obj.SetValueWithoutNotify(new Vector3Int(m_XValue.GetValueFromBag(bag, cc), m_YValue.GetValueFromBag(bag, cc), m_ZValue.GetValueFromBag(bag, cc)));

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
