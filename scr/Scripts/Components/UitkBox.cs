using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkBox : UitkLinker<Box> { }

    public class BoxG : UnityEngine.UIElements.Box, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<BoxG, UxmlTraits> { }

        public new class UxmlTraits : UnityEngine.UIElements.Box.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                BoxG obj = ve as BoxG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
