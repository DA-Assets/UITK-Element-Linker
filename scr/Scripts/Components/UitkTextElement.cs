using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkTextElement : UitkLinker<TextElement> { }

    public class TextElementG : UnityEngine.UIElements.TextElement, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<TextElementG, UxmlTraits> { }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
            {
                name = "text"
            };

            private UxmlBoolAttributeDescription m_EnableRichText = new UxmlBoolAttributeDescription
            {
                name = "enable-rich-text",
                defaultValue = true
            };

            private UxmlBoolAttributeDescription m_ParseEscapeSequences = new UxmlBoolAttributeDescription
            {
                name = "parse-escape-sequences",
                defaultValue = false
            };

            private UxmlBoolAttributeDescription m_DisplayTooltipWhenElided = new UxmlBoolAttributeDescription
            {
                name = "display-tooltip-when-elided"
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
                TextElementG obj = (TextElementG)ve;
                obj.text = m_Text.GetValueFromBag(bag, cc);
                obj.enableRichText = m_EnableRichText.GetValueFromBag(bag, cc);
                obj.parseEscapeSequences = m_ParseEscapeSequences.GetValueFromBag(bag, cc);
                obj.displayTooltipWhenElided = m_DisplayTooltipWhenElided.GetValueFromBag(bag, cc);

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
