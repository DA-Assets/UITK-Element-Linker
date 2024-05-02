using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkTemplateContainer : UitkLinker<TemplateContainer> { }

    public class TemplateContainerG : UnityEngine.UIElements.TemplateContainer, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<TemplateContainerG, UxmlTraits> { }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            internal const string k_TemplateAttributeName = "template";

            private UxmlStringAttributeDescription m_Template = new UxmlStringAttributeDescription
            {
                name = "template",
                use = UxmlAttributeDescription.Use.Required
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
                TemplateContainerG obj = (TemplateContainerG)ve;
                /* templateContainer.templateId = m_Template.GetValueFromBag(bag, cc);
                 VisualTreeAsset visualTreeAsset = cc.visualTreeAsset?.ResolveTemplate(templateContainer.templateId);
                 if (visualTreeAsset == null)
                 {
                     templateContainer.Add(new Label($"Unknown Template: '{templateContainer.templateId}'"));
                 }
                 else
                 {
                     List<TemplateAsset.AttributeOverride> list = (bag as TemplateAsset)?.attributeOverrides;
                     List<TemplateAsset.AttributeOverride> attributeOverrides = cc.attributeOverrides;
                     List<TemplateAsset.AttributeOverride> list2 = null;
                     if (list != null || attributeOverrides != null)
                     {
                         list2 = new List<TemplateAsset.AttributeOverride>();
                         if (attributeOverrides != null)
                         {
                             list2.AddRange(attributeOverrides);
                         }

                         if (list != null)
                         {
                             list2.AddRange(list);
                         }
                     }

                     visualTreeAsset.CloneTree(ve, cc.slotInsertionPoints, list2);
                 }

                 if (visualTreeAsset == null)
                 {
                     Debug.LogErrorFormat("Could not resolve template with name '{0}'", templateContainer.templateId);
                 }*/

                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
