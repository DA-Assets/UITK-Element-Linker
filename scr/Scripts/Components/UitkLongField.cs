using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkLongField : UitkLinker<LongField> { }

    public class LongFieldG : UnityEngine.UIElements.LongField, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<LongFieldG, UxmlTraits> { }

        public new class UxmlTraits : TextValueFieldTraits<long, UxmlLongAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                LongFieldG obj = ve as LongFieldG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
