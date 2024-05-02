using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkUnsignedIntegerField : UitkLinker<UnsignedIntegerField> { }

    public class UnsignedIntegerFieldG : UnityEngine.UIElements.UnsignedIntegerField, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<UnsignedIntegerFieldG, UxmlTraits> { }

        public new class UxmlTraits : TextValueFieldTraits<uint, UxmlUnsignedIntAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                UnsignedIntegerFieldG obj = ve as UnsignedIntegerFieldG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
