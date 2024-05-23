/*using System.Reflection;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkMultiColumnTreeView : UitkLinker<MultiColumnTreeView> { }

    public class MultiColumnTreeViewG : UnityEngine.UIElements.MultiColumnTreeView, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<MultiColumnTreeViewG, UxmlTraits> { }

        public new class UxmlTraits : BaseTreeView.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            private readonly UxmlBoolAttributeDescription m_SortingEnabled = new UxmlBoolAttributeDescription
            {
                name = "sorting-enabled"
            };

            private readonly UxmlObjectAttributeDescription<Columns> m_Columns = new UxmlObjectAttributeDescription<Columns>();

            private readonly UxmlObjectAttributeDescription<SortColumnDescriptions> m_SortColumnDescriptions = new UxmlObjectAttributeDescription<SortColumnDescriptions>();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                MultiColumnTreeViewG multiColumnTreeView = (MultiColumnTreeViewG)ve;
                multiColumnTreeView.sortingEnabled = m_SortingEnabled.GetValueFromBag(bag, cc);

                var sortColumnDescriptionsValue = m_SortColumnDescriptions.GetValueFromBag(bag, cc);
                var columnsValue = m_Columns.GetValueFromBag(bag, cc);

                PropertyInfo sortColumnDescriptionsProperty = multiColumnTreeView.GetType().GetProperty("sortColumnDescriptions", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo columnsProperty = multiColumnTreeView.GetType().GetProperty("columns", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                if (sortColumnDescriptionsProperty != null)
                {
                    sortColumnDescriptionsProperty.SetValue(multiColumnTreeView, sortColumnDescriptionsValue);
                }

                if (columnsProperty != null)
                {
                    columnsProperty.SetValue(multiColumnTreeView, columnsValue);
                }

                MultiColumnTreeViewG obj = ve as MultiColumnTreeViewG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}
*/