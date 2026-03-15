using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Amoherom
{
    [CustomEditor(typeof(VrmBlendShapeClip)), CanEditMultipleObjects]
    public class VrmBlendShapeClipEditor : Editor
    {
        // Single-clip list
        private ReorderableList _list;
        private SerializedProperty _blendShapesProp;

        // Shared bulk-add state (works for both single and multi-clip)
        private bool _bulkFoldout = false;
        private readonly HashSet<VrmBlendShapeBehaviour.ExpressionPreset> _selected =
            new HashSet<VrmBlendShapeBehaviour.ExpressionPreset>();
        private float _bulkValue = 1f;
        private Vector2 _scroll;

        // Multi-clip action mode
        private enum MultiMode { Append, Replace, Remove }
        private MultiMode _multiMode = MultiMode.Append;

        private static readonly string[] _presetNames =
            Enum.GetNames(typeof(VrmBlendShapeBehaviour.ExpressionPreset));
        private static readonly VrmBlendShapeBehaviour.ExpressionPreset[] _presetValues =
            (VrmBlendShapeBehaviour.ExpressionPreset[])
            Enum.GetValues(typeof(VrmBlendShapeBehaviour.ExpressionPreset));

        private void OnEnable()
        {
            _blendShapesProp = serializedObject.FindProperty("behaviour.blendShapes");
            BuildList();
        }

        private void BuildList()
        {
            _list = new ReorderableList(serializedObject, _blendShapesProp, true, true, true, true)
            {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Blend Shapes"),
                elementHeight = EditorGUIUtility.singleLineHeight * 2 + 8,
                drawElementCallback = (rect, index, active, focused) =>
                {
                    var element = _blendShapesProp.GetArrayElementAtIndex(index);
                    var presetProp = element.FindPropertyRelative("preset");
                    var valueProp = element.FindPropertyRelative("value");

                    var presetRect = new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight);
                    var sliderRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 6, rect.width, EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(presetRect, presetProp, GUIContent.none);
                    valueProp.floatValue = EditorGUI.Slider(sliderRect, valueProp.floatValue, -1f, 1f);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            bool isMulti = targets.Length > 1;

            if (isMulti)
            {
                EditorGUILayout.HelpBox(
                    $"Editing {targets.Length} clips",
                    MessageType.Info);
            }
            else
            {
                _list.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
                return;
            }

            EditorGUILayout.Space(4);

            // ---- Multi-clip preset picker ----
            string foldoutLabel = $"Multi-Clip Edit ({targets.Length} clips)";
            _bulkFoldout = EditorGUILayout.Foldout(_bulkFoldout, foldoutLabel, true);
            if (_bulkFoldout)
            {
                EditorGUI.indentLevel++;

                if (isMulti)
                {
                    _multiMode = (MultiMode)EditorGUILayout.EnumPopup("Action", _multiMode);
                    EditorGUILayout.HelpBox(
                        _multiMode == MultiMode.Append ? "Adds the selected presets to every clip (keeps existing ones)." :
                        _multiMode == MultiMode.Replace ? "Replaces each clip's entire list with the selected presets." :
                                                          "Removes the selected presets from every clip (others stay).",
                        MessageType.None);
                }

                bool needsValue = _multiMode != MultiMode.Remove;
                if (needsValue)
                    _bulkValue = EditorGUILayout.Slider("Value", _bulkValue, 0f, 1f);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Clear", GUILayout.Width(60)))
                    _selected.Clear();
                EditorGUILayout.EndHorizontal();

                _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(200));
                int cols = 3;
                int rows = Mathf.CeilToInt(_presetValues.Length / (float)cols);
                for (int r = 0; r < rows; r++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int c = 0; c < cols; c++)
                    {
                        int idx = r * cols + c;
                        if (idx >= _presetValues.Length) break;
                        var preset = _presetValues[idx];
                        bool was = _selected.Contains(preset);
                        bool now = EditorGUILayout.ToggleLeft(_presetNames[idx], was, GUILayout.Width(170));
                        if (now != was)
                        {
                            if (now) _selected.Add(preset);
                            else _selected.Remove(preset);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();

                EditorGUI.BeginDisabledGroup(_selected.Count == 0);

                string btnLabel = isMulti
                    ? $"{_multiMode} {_selected.Count} preset(s) on {targets.Length} clips"
                    : $"Add {_selected.Count} preset(s)";

                if (GUILayout.Button(btnLabel))
                {
                    foreach (var t in targets)
                    {
                        var clip = (VrmBlendShapeClip)t;
                        ApplyMultiAction(clip);
                        EditorUtility.SetDirty(clip);
                    }
                    if (!isMulti) _selected.Clear();
                }

                EditorGUI.EndDisabledGroup();
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void ApplyMultiAction(VrmBlendShapeClip clip)
        {
            if (_multiMode == MultiMode.Append)
            {
                foreach (var preset in _selected)
                    clip.behaviour.blendShapes.Add(
                        new VrmBlendShapeBehaviour.BlendShapeEntry { preset = preset, value = _bulkValue });
            }
            else if (_multiMode == MultiMode.Replace)
            {
                clip.behaviour.blendShapes.Clear();
                foreach (var preset in _selected)
                    clip.behaviour.blendShapes.Add(
                        new VrmBlendShapeBehaviour.BlendShapeEntry { preset = preset, value = _bulkValue });
            }
            else // Remove
            {
                clip.behaviour.blendShapes.RemoveAll(e => _selected.Contains(e.preset));
            }
        }

        private bool isMulti => targets.Length > 1;
    }
}