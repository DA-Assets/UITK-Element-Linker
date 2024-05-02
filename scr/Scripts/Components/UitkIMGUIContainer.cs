using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkIMGUIContainer : UitkLinker<IMGUIContainer> { }

    public class IMGUIContainerG : UnityEngine.UIElements.IMGUIContainer, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<IMGUIContainerG, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get
                {
                    yield break;
                }
            }

            public UxmlTraits()
            {
                base.focusIndex.defaultValue = 0;
                base.focusable.defaultValue = true;
            }

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                IMGUIContainerG obj = ve as IMGUIContainerG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
