using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DA_Assets.UEL
{
    [CustomEditor(typeof(UitkLinker<>), true), CanEditMultipleObjects]
    public class UitkLinkerEditor : Editor
    {
        private DAInspectorMini gui => DAInspectorMini.Instance;

        UitkLinkerBase monoBeh;

        Type targetType = null;

        void OnEnable()
        {
            targetType = target.GetType();
            monoBeh = (UitkLinkerBase)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            IMGUIContainer imguiContainer = new IMGUIContainer(() =>
            {
                OnInspectorLegacy(root);
            });

            imguiContainer.style.display = DisplayStyle.Flex;
            root.Add(imguiContainer);

            return root;
        }


        public void OnInspectorLegacy(VisualElement root)
        {
            serializedObject.Update();

            gui.DrawGroup(new Group
            {
                GroupType = GroupType.Vertical,
                Style = GuiStyle.DAInspectorBackground,
                DarkBg = true,
                Body = () =>
                {
                    gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.UIDocument);
                    gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.LinkingMode);

                    switch (monoBeh.LinkingMode)
                    {
                        case UitkLinkingMode.Name:
                            {
                                gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.Name);
                            }
                            break;
                        case UitkLinkingMode.IndexNames:
                            {
                                gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.Names);
                            }
                            break;
                        case UitkLinkingMode.Guid:
                            {
                                gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.Guid);
                            }
                            break;
                        case UitkLinkingMode.Guids:
                            {
                                gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.Guids);
                            }
                            break;
                    }

#if UNITY_2020_1_OR_NEWER
                    if (targetType == typeof(UitkButton))
                    {
                        gui.SerializedPropertyField<UitkButton>(serializedObject, x => x.OnClick);;
                    }
#endif

                    if (monoBeh.IsDebug)
                    {
                        GUILayout.Space(15);
                        base.OnInspectorGUI();
                    }
                    else
                    {
                        gui.SerializedPropertyField<UitkLinkerBase>(serializedObject, x => x.IsDebug);
                    }
                }
            });

            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomPropertyDrawer(typeof(ElementIndexName))]
    public class ElementIndexNameDrawer : PropertyDrawer
    {
        static readonly ElementIndexName def = default;

        const int padding = 2;
        const float spacing = 10;
        const float indexFieldWidth = 30;
        const float labelWidth = 50;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUIUtility.labelWidth = labelWidth;

            string indexName = nameof(def.Index);
            string nameName = nameof(def.Name);

            Rect indexRect = new Rect(position.x, position.y, indexFieldWidth + labelWidth, position.height);
            EditorGUI.PropertyField(indexRect, property.FindPropertyRelative(indexName), new GUIContent(indexName));

            float nameFieldWidth = position.width - indexFieldWidth - labelWidth - padding - spacing;

            Rect nameRect = new Rect(position.x + indexFieldWidth + labelWidth + padding + spacing, position.y, nameFieldWidth, position.height);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative(nameName), new GUIContent(nameName));

            EditorGUIUtility.labelWidth = 0;
            EditorGUI.indentLevel = indent;

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }
    }

}

