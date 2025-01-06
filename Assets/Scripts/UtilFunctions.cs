using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class UtilFunctions : MonoBehaviour
{
    public static string[] GetImagePaths(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogError("Directory does not exist: " + directoryPath);
            return new string[0];
        }

        return Directory.GetFiles(directoryPath)
            .Where(file =>
                file.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase) ||
                file.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }

    public static Material CreateMaterialFromImage(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(imageBytes))
        {
            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = texture;
            return material;
        }

        Debug.LogError("Failed to load texture from: " + imagePath);
        return null;
    }

    public static Texture2D LoadImageFromPath(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogError("Image path is null or empty.");
            return null;
        }

        if (!File.Exists(imagePath))
        {
            Debug.LogError("Image file does not exist at path: " + imagePath);
            return null;
        }

        byte[] imageBytes = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(imageBytes))
        {
            return texture;
        }
        else
        {
            Debug.LogError("Failed to load texture from: " + imagePath);
            return null;
        }
    }
}
