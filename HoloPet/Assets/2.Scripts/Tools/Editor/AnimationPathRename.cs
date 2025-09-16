using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationPathRename : EditorWindow
{
    string oldName = "Body";          // Original child name in animation
    string newName = "Body_New";      // New child name
    string newParent = "NewParent";   // Optional new parent to prepend

    [MenuItem("Tools/Animation Path Rewriter")]
    static void OpenWindow()
    {
        GetWindow<AnimationPathRename>("Animation Path Rewriter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Update Child Names and Hierarchy in Animation Clips", EditorStyles.boldLabel);

        oldName = EditorGUILayout.TextField("Old Child Name", oldName);
        newName = EditorGUILayout.TextField("New Child Name", newName);
        newParent = EditorGUILayout.TextField("New Parent (optional)", newParent);

        if (GUILayout.Button("Rewrite Selected Animation Clips"))
        {
            RewriteSelectedClips();
        }
    }

    void RewriteSelectedClips()
    {
        var clips = Selection.GetFiltered<AnimationClip>(SelectionMode.Assets);

        foreach (var clip in clips)
        {
            bool modified = false;

            var bindings = AnimationUtility.GetCurveBindings(clip);
            foreach (var binding in bindings)
            {
                if (binding.path.Contains(oldName))
                {
                    string updatedPath = binding.path.Replace(oldName, newName);

                    if (!string.IsNullOrEmpty(newParent))
                    {
                        updatedPath = newParent + "/" + updatedPath;
                    }

                    var curve = AnimationUtility.GetEditorCurve(clip, binding);
                    var newBinding = binding;
                    newBinding.path = updatedPath;

                    AnimationUtility.SetEditorCurve(clip, binding, null);
                    AnimationUtility.SetEditorCurve(clip, newBinding, curve);

                    modified = true;
                }
            }

            if (modified)
            {
                Debug.Log($"Updated animation clip: {clip.name}");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
