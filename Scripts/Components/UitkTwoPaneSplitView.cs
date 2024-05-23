/*using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkTwoPaneSplitView : UitkLinker<TwoPaneSplitView> { }

    public class TwoPaneSplitViewG : UnityEngine.UIElements.TwoPaneSplitView, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<TwoPaneSplitViewG, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
            {
                name = "fixed-pane-index",
                defaultValue = 0
            };

            private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
            {
                name = "fixed-pane-initial-dimension",
                defaultValue = 100
            };

            private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
            {
                name = "orientation",
                defaultValue = TwoPaneSplitViewOrientation.Horizontal
            };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get
                {
                    yield break;
                }
            }

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                int valueFromBag = m_FixedPaneIndex.GetValueFromBag(bag, cc);
                int valueFromBag2 = m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
                TwoPaneSplitViewOrientation valueFromBag3 = m_Orientation.GetValueFromBag(bag, cc);

                TwoPaneSplitViewG obj = ve as TwoPaneSplitViewG;

                Type type = obj.GetType();

                MethodInfo methodInfo = type.GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new Type[] { typeof(int), typeof(float), typeof(TwoPaneSplitViewOrientation) }, null);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(obj, new object[] { valueFromBag, valueFromBag2, valueFromBag3 });
                }

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
*/