using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

#pragma warning disable CS0649

namespace DA_Assets.UEL
{
    [CreateAssetMenu(menuName = "D.A. Assets" + "/" + "DAInspectorMini")]
    public class DAInspectorMini : SingletoneScriptableObjectInternal<DAInspectorMini>
    {
        [SerializeField] GUIStyle[] guiStyles;

        [SerializeField] VisualTreeAsset _baseUXML;
        public VisualTreeAsset BaseUXML => _baseUXML;

        internal void DrawGroup(Group group)
        {
            if (group.LabelWidth != null)
            {
                EditorGUIUtility.labelWidth = (float)group.LabelWidth;
            }

            StackFrame sf = new StackFrame(1, true);

            if (EditorGUIUtility.isProSkin)
            {
                if (group.DarkBg.ToBoolNullFalse())
                {
                    GUI.backgroundColor = Color.gray;
                }
            }
            else
            {
                GUI.backgroundColor = Color.white;
                EditorStyles.label.normal.textColor = Color.white;
            }

            if (group.GroupType == GroupType.Horizontal)
            {
                if (group.Style != GuiStyle.None)
                {
                    GUILayout.BeginHorizontal(GetStyle(group.Style), group.Options);
                }
                else
                {
                    GUILayout.BeginHorizontal(group.Options);
                }

                group.Body.Invoke();

                GUILayout.EndHorizontal();
            }
            else if (group.GroupType == GroupType.Vertical)
            {
                if (group.Style != GuiStyle.None)
                {
                    GUILayout.BeginVertical(GetStyle(group.Style), group.Options);
                }
                else
                {
                    GUILayout.BeginVertical(group.Options);
                }

                group.Body.Invoke();

                GUILayout.EndVertical();
            }
            else
            {
                Debug.Log($"Unknown group type.");
            }

            if (EditorGUIUtility.isProSkin)
            {
                if (group.DarkBg.ToBoolNullFalse())
                {
                    GUI.backgroundColor = Color.white;
                }
            }
            else
            {
                GUI.backgroundColor = Color.white;
                EditorStyles.label.normal.textColor = Color.black;
            }
        }

        public SerializedProperty SerializedPropertyField<T>(SerializedObject so, Expression<Func<T, object>> pathExpression)
        {
            string field = pathExpression.GetFieldsArray().Last();
            SerializedProperty lastProperty = so.FindProperty(field);

            so.Update();
            EditorGUILayout.PropertyField(lastProperty, true);
            so.ApplyModifiedProperties();

            return lastProperty;
        }

        internal GUIStyle GetStyle(GuiStyle customStyle)
        {
            foreach (GUIStyle style in guiStyles)
            {
                if (style.name == $"{customStyle}")
                {
                    return style;
                }
            }

            Debug.LogError($"'{customStyle}' style not found.");
            return guiStyles.FirstOrDefault(x => x.name == GuiStyle.None.ToString());
        }
    }

    internal static class DaiExtensionsInternal
    {
        public static bool ToBoolNullFalse(this bool? value)
        {
            if (value == null)
            {
                return false;
            }

            return value.Value;
        }
    }
    internal enum GroupType
    {
        Horizontal = 0,
        Vertical = 1
    }
    internal enum GuiStyle
    {
        None = 0,
        DAInspectorBackground = 806,
    }
    internal struct Group
    {
        public int InstanceId { get; set; }
        public GroupType GroupType { get; set; }
        public Action Body { get; set; }
        public GuiStyle Style { get; set; }
        public bool Flexible { get; set; }
        public AnimBool Fade { get; set; }
        public GUILayoutOption[] Options { get; set; }
        public bool Scroll { get; set; }
        public int? LabelWidth { get; set; }
        public int SplitterWidth { get; set; }
        public int SplitterStartPos { get; set; }
        public bool? DarkBg { get; set; }
    }

    internal static class DAInspectorMiniExtensions
    {
        public static string[] GetFieldsArray<T>(this Expression<Func<T, object>> pathExpression)
        {
            MemberExpression me;

            switch (pathExpression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    UnaryExpression ue = pathExpression.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;
                default:
                    me = pathExpression.Body as MemberExpression;
                    break;
            }

            List<string> fieldNames = new List<string>();

            while (me != null)
            {
                var serInfo = me.Member.GetCustomAttributes<SerializePropertyMiniAttribute>().ToArray()[0];

                if (serInfo == null)
                {
                    if (me.Member.Name.Contains("CS$") == false)
                    {
                        fieldNames.Add(me.Member.Name);
                    }
                }
                else
                {
                    fieldNames.Add(serInfo.FieldName);
                }

                me = me.Expression as MemberExpression;
            }

            fieldNames.Reverse();
            // fieldNames.RemoveAt(0);

            return fieldNames.ToArray();
        }
    }
}