using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkTreeView : UitkLinker<TreeView> { }

    public class TreeViewG : UnityEngine.UIElements.TreeView, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<TreeViewG, UxmlTraits> { }

        public new class UxmlTraits : BaseTreeView.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public override void Init(UnityEngine.UIElements.VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                TreeViewG obj = ve as TreeViewG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}