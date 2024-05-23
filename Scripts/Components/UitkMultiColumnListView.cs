/*using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkMultiColumnListView : UitkLinker<MultiColumnListView> { }

    public class MultiColumnListViewG : UnityEngine.UIElements.MultiColumnListView, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<MultiColumnListViewG, UxmlTraits> { }

        public new class UxmlTraits : BaseListView.UxmlTraits
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
                MultiColumnListViewG multiColumnListView = (MultiColumnListViewG)ve;
                multiColumnListView.sortingEnabled = m_SortingEnabled.GetValueFromBag(bag, cc);

                var sortColumnDescriptionsValue = m_SortColumnDescriptions.GetValueFromBag(bag, cc);
                var columnsValue = m_Columns.GetValueFromBag(bag, cc);

                PropertyInfo sortColumnDescriptionsProperty = multiColumnListView.GetType().GetProperty("sortColumnDescriptions", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo columnsProperty = multiColumnListView.GetType().GetProperty("columns", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                if (sortColumnDescriptionsProperty != null)
                {
                    sortColumnDescriptionsProperty.SetValue(multiColumnListView, sortColumnDescriptionsValue);
                }

                if (columnsProperty != null)
                {
                    columnsProperty.SetValue(multiColumnListView, columnsValue);
                }

                MultiColumnListViewG obj = ve as MultiColumnListViewG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }

    internal class UxmlObjectAttributeDescription<T> where T : new()
    {
        public T defaultValue { get; set; }

        public virtual T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
        {
            if (cc.visualTreeAsset != null)
            {
                MethodInfo getUxmlObjectsMethod = cc.visualTreeAsset.GetType().GetMethod("GetUxmlObjects", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(IBinding), typeof(CreationContext) }, null);

                if (getUxmlObjectsMethod != null)
                {
                    MethodInfo genericMethod = getUxmlObjectsMethod.MakeGenericMethod(typeof(T));

                    object result = genericMethod.Invoke(cc.visualTreeAsset, new object[] { bag, cc });

                    if (result != null)
                    {
                        List<T> list = (List<T>)result;

                        if (list != null)
                        {
                            using List<T>.Enumerator enumerator = list.GetEnumerator();
                            if (enumerator.MoveNext())
                            {
                                return enumerator.Current;
                            }
                        }
                    }
                }
            }

            return defaultValue;
        }
    }

}
*/