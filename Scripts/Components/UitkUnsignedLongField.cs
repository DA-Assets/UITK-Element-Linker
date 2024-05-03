#if UNITY_2022_1_OR_NEWER
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkUnsignedLongField : UitkLinker<UnsignedLongField> { }

    public class UnsignedLongFieldG : UnityEngine.UIElements.UnsignedLongField, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<UnsignedLongFieldG, UxmlTraits> { }

        public new class UxmlTraits : TextValueFieldTraits<ulong, UxmlUnsignedLongAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                UnsignedLongFieldG obj = ve as UnsignedLongFieldG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
#endif