using UnityEngine;
using UnityEditor;
using System.IO;

public class AnimationPathInserter : EditorWindow
{
    string parentToInsert = "HumanBody"; // Parent to insert
    string insertAfter = "BodyParts";    // Path segment after which to insert
    bool createBackup = true;            // Whether to backup clips

    [MenuItem("Tools/Animation Path Inserter")]
    static void OpenWindow()
    {
        GetWindow<AnimationPathInserter>("Safe Parent Inserter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Insert a Parent in AnimationClip Paths ", EditorStyles.boldLabel);

        insertAfter = EditorGUILayout.TextField("Insert After Path Segment", insertAfter);
        parentToInsert = EditorGUILayout.TextField("Parent to Insert", parentToInsert);
        createBackup = EditorGUILayout.Toggle("Create Backup", createBackup);

        if (GUILayout.Button("Update Selected Animation Clips"))
        {
            UpdateSelectedClips();
        }
    }

    void UpdateSelectedClips()
    {
        var clips = Selection.GetFiltered<AnimationClip>(SelectionMode.Assets);

        foreach (var clip in clips)
        {
            bool modified = false;

            // ----- Transform / float / vector curves -----
            var curveBindings = AnimationUtility.GetCurveBindings(clip);
            foreach (var binding in curveBindings)
            {
                string newPath = InsertParentIfNeeded(binding.path);
                if (newPath != binding.path)
                {
                    var curve = AnimationUtility.GetEditorCurve(clip, binding);
                    var newBinding = binding;
                    newBinding.path = newPath;

                    AnimationUtility.SetEditorCurve(clip, binding, null);
                    AnimationUtility.SetEditorCurve(clip, newBinding, curve);
                    modified = true;
                }
            }

            // ----- ObjectReference curves (SpriteRenderer.sprite) -----
            var objBindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            foreach (var binding in objBindings)
            {
                string newPath = InsertParentIfNeeded(binding.path);
                if (newPath != binding.path)
                {
                    var keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);
                    var newBinding = binding;
                    newBinding.path = newPath;

                    AnimationUtility.SetObjectReferenceCurve(clip, binding, null);
                    AnimationUtility.SetObjectReferenceCurve(clip, newBinding, keyframes);
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

    string InsertParentIfNeeded(string path)
    {
        var segments = path.Split('/');

        for (int i = 0; i < segments.Length; i++)
        {
            if (segments[i] == insertAfter)
            {
                // Already has parent inserted? Skip
                if (i + 1 < segments.Length && segments[i + 1] == parentToInsert)
                    return path;

                // Insert parent
                return string.Join("/", segments, 0, i + 1) + "/" + parentToInsert + "/" + string.Join("/", segments, i + 1, segments.Length - (i + 1));
            }
        }

        return path;
    }
}
