using UnityEngine;
using UnityEditor;
using System.IO;

public class ApplyTexturesToMaterials : Editor
{
    [MenuItem("HPCOS/HPCOS Material Fix")]
    static void ApplyTextures()
    {
        string texturesPath = "Assets/HPCOS/Textures";
        string materialsPath = "Assets/HPCOS/Materials";

        // Get all texture files in the texture folder
        string[] textureFiles = Directory.GetFiles(texturesPath, "*.png"); // Assuming textures are in PNG format

        foreach (string textureFile in textureFiles)
        {
            // Get the texture name without the extension
            string textureName = Path.GetFileNameWithoutExtension(textureFile);

            // Load the texture from the file
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureFile);

            if (texture != null)
            {
                // Try to find a material with the same name in the materials folder
                string materialPath = Path.Combine(materialsPath, textureName + ".mat");
                Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

                if (material != null)
                {
                    // Apply the texture to the material
                    material.mainTexture = texture;
                    EditorUtility.SetDirty(material); // Mark material as dirty to save changes

                    Debug.Log($"Applied texture {textureName} to material {material.name}");
                }
                else
                {
                    Debug.LogWarning($"Material not found for texture: {textureName}");
                }
            }
            else
            {
                Debug.LogWarning($"Failed to load texture: {textureName}");
            }
        }

        // Save all changes
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Texture application complete.");
    }
}
