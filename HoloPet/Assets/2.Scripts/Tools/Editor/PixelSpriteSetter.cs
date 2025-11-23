using UnityEngine;
using UnityEditor;

public class PixelSpriteSetter : EditorWindow
{
    private Texture2D texture;
    private Vector2 pivot = new Vector2(0.5f, 0f); // bottom center by default
    private float pixelsPerUnit = 32f;             // editable PPU
    private bool slice = true;                     // toggle for slicing
    private int gridSizeX = 32;
    private int gridSizeY = 32;

    [MenuItem("Tools/PixelSpriteSetter")]
    public static void ShowWindow()
    {
        GetWindow<PixelSpriteSetter>("PixelSpriteSetter");
    }

    void OnGUI()
    {
        GUILayout.Label("Batch Set Sprite Pivot + Import Settings", EditorStyles.boldLabel);
        texture = (Texture2D)EditorGUILayout.ObjectField("Sprite Texture", texture, typeof(Texture2D), false);
        pivot = EditorGUILayout.Vector2Field("Pivot (0¡V1)", pivot);
        pixelsPerUnit = EditorGUILayout.FloatField("Pixels Per Unit", pixelsPerUnit);

        GUILayout.Space(8);
        slice = EditorGUILayout.Toggle("Slice Sprite", slice);

        if (slice)
        {
            GUILayout.Space(4);
            GUILayout.Label("Grid Slice Settings", EditorStyles.boldLabel);
            gridSizeX = EditorGUILayout.IntField("Cell Size X (px)", gridSizeX);
            gridSizeY = EditorGUILayout.IntField("Cell Size Y (px)", gridSizeY);
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Apply to Sprite(s)"))
        {
            if (texture == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a texture.", "OK");
                return;
            }

            SetPivotAndSettings(texture, pivot, pixelsPerUnit, gridSizeX, gridSizeY, slice);
        }
    }

    private void SetPivotAndSettings(Texture2D tex, Vector2 pivot, float ppu, int gridX, int gridY, bool doSlice)
    {
        string path = AssetDatabase.GetAssetPath(tex);
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;

        if (importer == null)
        {
            EditorUtility.DisplayDialog("Error", "Invalid texture importer.", "OK");
            return;
        }

        bool changed = false;

        // --- 1?? Import base settings ---
        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = ppu;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        changed = true;

        // --- 2?? Handle slicing ---
        if (doSlice)
        {
            int cols = Mathf.Max(1, tex.width / gridX);
            int rows = Mathf.Max(1, tex.height / gridY);

            var slices = new SpriteMetaData[cols * rows];
            int index = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    var rect = new Rect(x * gridX, y * gridY, gridX, gridY);
                    slices[index] = new SpriteMetaData
                    {
                        rect = rect,
                        name = $"{tex.name}_{x}_{y}",
                        alignment = (int)SpriteAlignment.Custom,
                        pivot = pivot
                    };
                    index++;
                }
            }

            importer.spritesheet = slices;
            Debug.Log($"? {tex.name}: Sliced into {cols * rows} sprites ({gridX}x{gridY}px each)");
        }
        else
        {
            // Not slicing: just apply pivot to existing sprites (or single one)
            if (importer.spritesheet != null && importer.spritesheet.Length > 0)
            {
                var sprites = importer.spritesheet;
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].alignment = (int)SpriteAlignment.Custom;
                    sprites[i].pivot = pivot;
                }
                importer.spritesheet = sprites;
                Debug.Log($"? {tex.name}: Updated pivot for {sprites.Length} existing sprites");
            }
            else
            {
                // Create one full sprite if no sprites exist
                SpriteMetaData meta = new SpriteMetaData
                {
                    rect = new Rect(0, 0, tex.width, tex.height),
                    name = tex.name,
                    alignment = (int)SpriteAlignment.Custom,
                    pivot = pivot
                };
                importer.spritesheet = new SpriteMetaData[] { meta };
                Debug.Log($"? {tex.name}: Added single sprite with pivot {pivot}");
            }
        }

        // --- 3?? Apply changes ---
        if (changed)
        {
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
            Debug.Log($"?? Updated: Pivot={pivot}, PPU={ppu}, Slice={doSlice}");
        }
    }
}







