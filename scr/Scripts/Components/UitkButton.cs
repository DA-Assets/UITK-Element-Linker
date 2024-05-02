using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkButton : UitkLinker<Button> 
    {
        [SerializeField] UnityEvent _onClick;
        [SerializePropertyMini(nameof(_onClick))]    
        public UnityEvent OnClick { get => _onClick; set => _onClick = value; }

        public override void OnElementLinked()
        {
            base.OnElementLinked();

            _element.RegisterCallback<ClickEvent>(evt => _onClick.Invoke());
        }
    }

    public class ButtonG : UnityEngine.UIElements.Button, IHaveGuid
    {
        public string guid { get; set; }

        public new class UxmlFactory : UxmlFactory<ButtonG, UxmlTraits> { }

        public new class UxmlTraits : TextElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_Guid = GuidGenerator.GetGuidField();

            public UxmlTraits()
            {
                base.focusable.defaultValue = true;
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                ButtonG obj = ve as ButtonG;
                GuidGenerator.GenerateGuid(m_Guid, obj, bag, cc);
            }
        }
    }
}