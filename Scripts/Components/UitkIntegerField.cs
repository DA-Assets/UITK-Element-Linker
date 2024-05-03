#if UNITY_2022_1_OR_NEWER
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkIntegerField : UitkLinker<IntegerField> { }

    public class IntegerFieldG : UnityEngine.UIElements.IntegerField, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<IntegerFieldG, UxmlTraits> { }

        public new class UxmlTraits : TextValueFieldTraits<int, UxmlIntAttributeDescription>
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                IntegerFieldG obj = ve as IntegerFieldG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
#endif