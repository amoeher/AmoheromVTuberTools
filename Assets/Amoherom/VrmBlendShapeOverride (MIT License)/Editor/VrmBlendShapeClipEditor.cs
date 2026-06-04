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

        // Shared bulk-add state
        private bool _bulkFoldout = false;
        private readonly HashSet<VrmBlendShapeBehaviour.ExpressionPreset> _selected =
            new HashSet<VrmBlendShapeBehaviour.ExpressionPreset>();
        private float _bulkValue = 1f;
        private Vector2 _scroll;

        // Search/filter
        private string _search = "";
        private bool _showSelectedOnly = false;

        private enum PresetCategory
        {
            All,
            VRMCore,
            VRMExtra,
            ARKit
        }
        private PresetCategory _category = PresetCategory.All;

        // Multi-clip action mode
        private enum MultiMode { Append, Replace, Remove }
        private MultiMode _multiMode = MultiMode.Append;

        private static readonly string[] _presetNames =
            Enum.GetNames(typeof(VrmBlendShapeBehaviour.ExpressionPreset));
        private static readonly VrmBlendShapeBehaviour.ExpressionPreset[] _presetValues =
            (VrmBlendShapeBehaviour.ExpressionPreset[])
            Enum.GetValues(typeof(VrmBlendShapeBehaviour.ExpressionPreset));

        // Boundaries from your enum ordering
        private const int VRMCoreStart = 0;
        private const int VRMCoreEnd = 18;      // neutral
        private const int VRMExtraStart = 19;   // BrowAngry
        private const int VRMExtraEnd = 61;     // HAShortLow
        private const int ARKitStart = 62;      // browInnerUp
        // ARKit end = last index

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

            bool multi = targets.Length > 1;

            if (multi)
            {
                EditorGUILayout.HelpBox($"Editing {targets.Length} clips", MessageType.Info);
            }
            else
            {
                _list.DoLayoutList();
                serializedObject.ApplyModifiedProperties();

                DrawBulkSection(multi);
                return;
            }

            DrawBulkSection(multi);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBulkSection(bool isMultiEditing)
        {
            EditorGUILayout.Space(4);

            string foldoutLabel = isMultiEditing
                ? $"Multi-Clip Edit ({targets.Length} clips)"
                : "Bulk Add Presets";

            _bulkFoldout = EditorGUILayout.Foldout(_bulkFoldout, foldoutLabel, true);
            if (!_bulkFoldout) return;

            EditorGUI.indentLevel++;

            if (isMultiEditing)
            {
                _multiMode = (MultiMode)EditorGUILayout.EnumPopup("Action", _multiMode);
                EditorGUILayout.HelpBox(
                    _multiMode == MultiMode.Append ? "Adds selected presets to every clip (keeps existing)." :
                    _multiMode == MultiMode.Replace ? "Replaces each clip list with selected presets." :
                                                      "Removes selected presets from every clip.",
                    MessageType.None);
            }

            bool needsValue = _multiMode != MultiMode.Remove;
            if (needsValue)
                _bulkValue = EditorGUILayout.Slider("Value", _bulkValue, 0f, 1f);

            // Filters
            EditorGUILayout.BeginHorizontal();
            _search = EditorGUILayout.TextField("Search", _search);
            _showSelectedOnly = EditorGUILayout.ToggleLeft("Selected only", _showSelectedOnly, GUILayout.Width(110));
            EditorGUILayout.EndHorizontal();

            _category = (PresetCategory)EditorGUILayout.EnumPopup("Category", _category);

            // Actions
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear Selected", GUILayout.Width(110)))
                _selected.Clear();

            if (GUILayout.Button("Select Visible", GUILayout.Width(110)))
            {
                for (int i = 0; i < _presetValues.Length; i++)
                {
                    if (IsVisible(i))
                        _selected.Add(_presetValues[i]);
                }
            }

            if (GUILayout.Button("Deselect Visible", GUILayout.Width(120)))
            {
                for (int i = 0; i < _presetValues.Length; i++)
                {
                    if (IsVisible(i))
                        _selected.Remove(_presetValues[i]);
                }
            }
            EditorGUILayout.EndHorizontal();

            // Grid
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(260));
            int cols = 3;
            List<int> visible = new List<int>(_presetValues.Length);
            for (int i = 0; i < _presetValues.Length; i++)
            {
                if (IsVisible(i)) visible.Add(i);
            }

            int rows = Mathf.CeilToInt(visible.Count / (float)cols);
            int cursor = 0;
            for (int r = 0; r < rows; r++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int c = 0; c < cols; c++)
                {
                    if (cursor >= visible.Count) break;
                    int idx = visible[cursor++];
                    var preset = _presetValues[idx];

                    bool was = _selected.Contains(preset);
                    bool now = EditorGUILayout.ToggleLeft(_presetNames[idx], was, GUILayout.Width(210));
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

            string btnLabel = isMultiEditing
                ? $"{_multiMode} {_selected.Count} preset(s) on {targets.Length} clip(s)"
                : $"Add {_selected.Count} preset(s)";

            if (GUILayout.Button(btnLabel))
            {
                foreach (var t in targets)
                {
                    var clip = (VrmBlendShapeClip)t;
                    ApplyAction(clip);
                    EditorUtility.SetDirty(clip);
                }

                // optional convenience
                if (!isMultiEditing && _multiMode == MultiMode.Append)
                    _selected.Clear();
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
        }

        private bool IsVisible(int idx)
        {
            if (!MatchesCategory(idx)) return false;
            if (!MatchesSearch(_presetNames[idx])) return false;

            if (_showSelectedOnly && !_selected.Contains(_presetValues[idx]))
                return false;

            return true;
        }

        private bool MatchesSearch(string name)
        {
            if (string.IsNullOrWhiteSpace(_search)) return true;
            return name.IndexOf(_search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool MatchesCategory(int idx)
        {
            switch (_category)
            {
                case PresetCategory.All:
                    return true;
                case PresetCategory.VRMCore:
                    return idx >= VRMCoreStart && idx <= VRMCoreEnd;
                case PresetCategory.VRMExtra:
                    return idx >= VRMExtraStart && idx <= VRMExtraEnd;
                case PresetCategory.ARKit:
                    return idx >= ARKitStart;
                default:
                    return true;
            }
        }

        private void ApplyAction(VrmBlendShapeClip clip)
        {
            if (_multiMode == MultiMode.Append)
            {
                foreach (var preset in _selected)
                {
                    clip.behaviour.blendShapes.Add(
                        new VrmBlendShapeBehaviour.BlendShapeEntry
                        {
                            preset = preset,
                            value = _bulkValue
                        });
                }
            }
            else if (_multiMode == MultiMode.Replace)
            {
                clip.behaviour.blendShapes.Clear();
                foreach (var preset in _selected)
                {
                    clip.behaviour.blendShapes.Add(
                        new VrmBlendShapeBehaviour.BlendShapeEntry
                        {
                            preset = preset,
                            value = _bulkValue
                        });
                }
            }
            else // Remove
            {
                clip.behaviour.blendShapes.RemoveAll(e => _selected.Contains(e.preset));
            }
        }
    }
}