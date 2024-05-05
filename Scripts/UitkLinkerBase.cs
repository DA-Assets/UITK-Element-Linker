using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    public class UitkLinker<T> : UitkLinkerBase, IHaveElement<T> where T : VisualElement
    {
        protected T _element;
        public T Element => _element;
        public T E => _element;

        public virtual void Awake()
        {
            LinkElement();
        }

        public override void LinkElement()
        {
            string goName = this == null ? "null" : gameObject == null ? "null" : gameObject.name;
            string targetObjectStr = GetTargetObjectStr();

            if (_uiDocument == null)
            {
                throw new NullReferenceException($"Can't find UIDocument for '{targetObjectStr}'.\nGameObject name: {goName}");
            }

            VisualElement root =
#if UNITY_2021_1_OR_NEWER
                _uiDocument.rootVisualElement;
#else  
                null;
#endif

            VisualElement elem = null;

            switch (_linkingMode)
            {
                case UitkLinkingMode.Name:
                    {
                        if (_fullNamePathSearch)
                        {
                            elem = FindComponentByHierarchy(root, _names, x => x.name);
                        }
                        else
                        {
                            elem = root.Query(_name);
                        }
                    }
                    break;
                case UitkLinkingMode.Guid:
                    {
                        if (_fullGuidPathSearch)
                        {
                            elem = FindComponentByHierarchy(root, _guids, x => x.guid);
                        }
                        else
                        {
                            elem = FindGuidRecursive(root, _guid);
                        }
                    }
                    break;
            }

            if (elem == null)
            {
                Debug.LogError($"Can't find '{targetObjectStr}' element.\nGameObject name: {goName}");
                return;
            }

            if (elem is T e)
            {
                _element = e;

            }
            else
            {
                Debug.LogError($"Can't cast types: {elem.GetType()} | {typeof(T)}.\nGameObject name: {goName}");
            }

            if (_debug && _element != null)
            {
                string elementName = (string.IsNullOrEmpty(_element.name) && (_element is IHaveGuid)) ? (_element as IHaveGuid).guid : _element.name;
                Debug.Log($"Element found: {elementName}.\nGameObject name: {goName}");
            }

            OnElementLinked();
        }

        private VisualElement FindGuidRecursive(VisualElement root, string guid)
        {
            if (root == null)
                return null;

            System.Collections.Generic.IEnumerable<VisualElement> childs = root.Children();

            if (childs == null)
                return null;

            foreach (VisualElement child in childs)
            {
                if (child is IHaveGuid customElement && customElement.guid == guid)
                {
                    return child;
                }

                VisualElement found = FindGuidRecursive(child, guid);

                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private VisualElement FindComponentByHierarchy(VisualElement root, ElementIndexName[] hierarchy, Func<VisualElement, string> propertySelector, int depth = 0)
        {
            if (depth == hierarchy.Length)
                return root;

            if (root == null)
                return null;

            ElementIndexName ein = hierarchy[depth];

            VisualElement[] children = root.Children().ToArray();

            for (int i = 0; i < children.Length; i++)
            {
                VisualElement child = children[i];

                int newDepth = depth + 1;

                if (ein.Index == DEFAULT_INDEX)
                {
                    if (_debug)
                        Debug.Log($"{gameObject.name} | 1 | {ein.Name}");

                    if (propertySelector(child) == ein.Name)
                    {
                        return FindComponentByHierarchy(child, hierarchy, propertySelector, newDepth);
                    }
                }
                else if (ein.Index == i)
                {
                    if (_debug)
                        Debug.Log($"{gameObject.name} | 2 | {ein.Name}");

                    return FindComponentByHierarchy(child, hierarchy, propertySelector, newDepth);
                }
            }

            return null;
        }

        private VisualElement FindComponentByHierarchy(VisualElement root, string[] hierarchy, Func<IHaveGuid, string> propertySelector, int depth = 0)
        {
            if (depth == hierarchy.Length)
                return root;

            if (root == null)
                return null;

            string guid = hierarchy[depth];

            VisualElement[] children = root.Children().ToArray();

            for (int i = 0; i < children.Length; i++)
            {
                VisualElement child = children[i];

                int newDepth = depth + 1;

                if (child is IHaveGuid myElement)
                {
                    if (propertySelector(myElement) == guid)
                    {
                        if (_debug)
                            Debug.Log($"{gameObject.name} | 3 | {guid}");

                        return FindComponentByHierarchy(child, hierarchy, propertySelector, newDepth);
                    }
                }
            }

            return null;
        }
        private string GetTargetObjectStr()
        {
            string str = null;

            switch (_linkingMode)
            {
                case UitkLinkingMode.Name:
                    {
                        if (_fullNamePathSearch)
                        {
                            if (_names.Length > 0)
                            {
                                str = _names.Last().Name;
                            }
                        }
                        else
                        {
                            str = _name;
                        }
                    }
                    break;
                case UitkLinkingMode.Guid:
                    {
                        if (_fullGuidPathSearch)
                        {
                            if (_guids.Length > 0)
                            {
                                str = _guids.Last();
                            }
                        }
                        else
                        {
                            str = _guid;
                        }
                    }
                    break;
            }

            if (string.IsNullOrEmpty(str))
            {
                str = "null";
            }

            return str;
        }
    }

    public class SerializePropertyMiniAttribute : Attribute
    {
        public SerializePropertyMiniAttribute(string fieldName)
        {
            this.fieldName = fieldName;
        }

        private string fieldName;
        public string FieldName => fieldName;
    }

    [System.Serializable]
    public struct ElementIndexName
    {
        public int Index;
        public string Name;
    }

    public abstract class UitkLinkerBase : MonoBehaviour
    {
        public const string Product = "UITK Linker";
        public const string Publisher = "D.A. Assets";
        public const int DEFAULT_INDEX = -1;

        protected bool _fullGuidPathSearch => _guids.Length > 0;
        protected bool _fullNamePathSearch => _names.Length > 0;

        [SerializeField] protected UitkLinkingMode _linkingMode = UitkLinkingMode.Name;
        [SerializePropertyMini(nameof(_linkingMode))]
        public UitkLinkingMode LinkingMode { get => _linkingMode; set => _linkingMode = value; }

        [SerializeField] protected string _guid;
        [SerializePropertyMini(nameof(_guid))]
        public string Guid { get => _guid; set => _guid = value; }

        [SerializeField] protected string[] _guids = new string[] { };
        [SerializePropertyMini(nameof(_guids))]
        public string[] Guids { get => _guids; set => _guids = value; }

        [SerializeField] protected string _name;
        [SerializePropertyMini(nameof(_name))]
        public string Name { get => _name; set => _name = value; }

        [SerializeField] protected ElementIndexName[] _names = new ElementIndexName[] { };
        [SerializePropertyMini(nameof(_names))]
        public ElementIndexName[] Names { get => _names; set => _names = value; }


        [SerializeField]
        protected
#if UNITY_2021_1_OR_NEWER
            UIDocument 
#else
            GameObject
#endif
            _uiDocument;
        [SerializePropertyMini(nameof(_uiDocument))]
        public
#if UNITY_2021_1_OR_NEWER
            UIDocument
#else
            GameObject
#endif
            UIDocument
        { get => _uiDocument; set => _uiDocument = value; }

        [SerializeField] protected bool _debug;
        [SerializePropertyMini(nameof(_debug))]
        public bool IsDebug { get => _debug; set => _debug = value; }

        public GameObject GameObject => gameObject;

        public virtual void LinkElement()
        {

        }

        public virtual void OnElementLinked()
        {

        }
    }

    public enum UitkLinkingMode
    {
        Name,
        Guid
    }

    public interface IHaveElement<T>
    {
        T E { get; }
        T Element { get; }
    }
}