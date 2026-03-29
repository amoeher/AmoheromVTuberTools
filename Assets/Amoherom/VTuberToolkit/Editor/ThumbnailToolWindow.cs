using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Common;
using TMPro;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CaptureConfiguration
{
    public string name = "New Config";
    public List<string> selectedCameraNames = new List<string>();
    public ThumbnailToolWindow.AspectRatio aspectRatio = ThumbnailToolWindow.AspectRatio.SixteenByNine;
    public int baseWidth = 1920;
    public ThumbnailToolWindow.ImageFormat format = ThumbnailToolWindow.ImageFormat.PNG;
    public int jpgQuality = 90;
    public bool useCustomSize = false;
    
}

[System.Serializable]
public class ConfigurationList
{
    public List<CaptureConfiguration> items = new List<CaptureConfiguration>();
}

public class ThumbnailToolWindow : EditorWindow
{
    string THUMBNAIL_FOLDER_NAME = "Thumbnails";
    private const string PREFS_KEY = "ThumbnailToolConfigurations";
    // Configuration management
    private List<CaptureConfiguration> configurations = new List<CaptureConfiguration>();
    private int selectedConfigIndex = -1;
    private Vector2 configScrollPosition;
    private Vector2 cameraScrollPosition;
    private bool showConfigEditor = false;
    
    // Aspect ratio settings
    public enum AspectRatio 
    { 
        SixteenByNine,      // 16:9 (Horizontal)
        NineBySixteen,      // 9:16 (Vertical)
        OneByOne,           // 1:1 (Square)
        FourByThree,        // 4:3 (Standard)
        ThreeByFour         // 3:4 (Vertical Standard)
    }
    
    // Format settings
    public enum ImageFormat { PNG, JPG }
    
    // Aspect ratio display names
    private static readonly string[] aspectRatioNames = new string[]
    {
        "16:9 (Horizontal)",
        "9:16 (Vertical)",
        "1:1 (Square)",
        "4:3 (Standard Horizontal)",
        "3:4 (Standard Vertical)"
    };

    [MenuItem("Amoherom/VTuberThmbnail")]
    public static void ShowWindow()
    {
        GetWindow<ThumbnailToolWindow>("VTuber Thumbnail");
    }

    void OnGUI()
    {
        GUILayout.Label("Amoherom VTuber Thumbnail", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        // Configuration List Section
        GUILayout.Label("Capture Configurations", EditorStyles.boldLabel);
        
        if (configurations.Count == 0)
        {
            EditorGUILayout.HelpBox("No configurations created. Add a configuration to get started!", MessageType.Info);
        }
        else
        {
            // Display configuration list
            configScrollPosition = EditorGUILayout.BeginScrollView(configScrollPosition, GUILayout.Height(Mathf.Min(configurations.Count * 25 + 10, 120)));
            
            for (int i = 0; i < configurations.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                string aspectRatioDisplay = aspectRatioNames[(int)configurations[i].aspectRatio].Split(' ')[0]; // Get just "16:9" part
                string configLabel = $"{configurations[i].name} - {aspectRatioDisplay} ({configurations[i].selectedCameraNames.Count} cam)";
                
                if (GUILayout.Toggle(selectedConfigIndex == i, configLabel, "Button"))
                {
                    selectedConfigIndex = i;
                    showConfigEditor = true;
                }
                
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    if (EditorUtility.DisplayDialog("Delete Configuration", 
                        $"Are you sure you want to delete '{configurations[i].name}'?", 
                        "Delete", "Cancel"))
                    {
                        configurations.RemoveAt(i);
                        if (selectedConfigIndex >= configurations.Count)
                        {
                            selectedConfigIndex = configurations.Count - 1;
                        }
                    }
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndScrollView();
        }
        
        // Add/Remove buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Configuration"))
        {
            configurations.Add(new CaptureConfiguration() { name = $"Config {configurations.Count + 1}" });
            selectedConfigIndex = configurations.Count - 1;
            showConfigEditor = true;
        }
        
        if (configurations.Count > 0 && GUILayout.Button("Duplicate Selected") && selectedConfigIndex >= 0)
        {
            var newConfig = new CaptureConfiguration();
            var selected = configurations[selectedConfigIndex];
            newConfig.name = selected.name + " Copy";
            newConfig.selectedCameraNames = new List<string>(selected.selectedCameraNames);
            newConfig.aspectRatio = selected.aspectRatio;
            newConfig.baseWidth = selected.baseWidth;
            newConfig.format = selected.format;
            newConfig.jpgQuality = selected.jpgQuality;
            newConfig.useCustomSize = selected.useCustomSize;
            configurations.Add(newConfig);
            selectedConfigIndex = configurations.Count - 1;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Configuration Editor
        if (showConfigEditor && selectedConfigIndex >= 0 && selectedConfigIndex < configurations.Count)
        {
            DrawConfigurationEditor(configurations[selectedConfigIndex]);
        }

        EditorGUILayout.Space();

        // Capture All Button
        GUI.enabled = configurations.Count > 0 && configurations.Any(c => c.selectedCameraNames.Count > 0);
        if (GUILayout.Button("Capture All Configurations", GUILayout.Height(40)))
        {
            CaptureAllConfigurations();
        }
        GUI.enabled = true;
        
        if (configurations.Count > 0)
        {
            int totalCaptures = configurations.Sum(c => c.selectedCameraNames.Count);
            EditorGUILayout.LabelField($"Total captures: {totalCaptures} screenshot(s)", EditorStyles.centeredGreyMiniLabel);
        }
    }

    private void DrawConfigurationEditor(CaptureConfiguration config)
    {
        EditorGUILayout.BeginVertical("box");
        
        GUILayout.Label("Edit Configuration", EditorStyles.boldLabel);
        
        // Configuration Name
        config.name = EditorGUILayout.TextField("Configuration Name", config.name);
        
        EditorGUILayout.Space();
        
        // Camera Selection
        GUILayout.Label("Camera Selection", EditorStyles.boldLabel);
        Camera[] cameras = Camera.allCameras;
        
        if (cameras.Length > 0)
        {
            cameraScrollPosition = EditorGUILayout.BeginScrollView(cameraScrollPosition, GUILayout.Height(Mathf.Min(cameras.Length * 20 + 10, 100)));
            
            foreach (Camera cam in cameras)
            {
                bool isSelected = config.selectedCameraNames.Contains(cam.name);
                bool newSelected = EditorGUILayout.Toggle(cam.name, isSelected);
                
                if (newSelected != isSelected)
                {
                    if (newSelected)
                    {
                        config.selectedCameraNames.Add(cam.name);
                    }
                    else
                    {
                        config.selectedCameraNames.Remove(cam.name);
                    }
                }
            }
            
            EditorGUILayout.EndScrollView();
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select All"))
            {
                config.selectedCameraNames.Clear();
                foreach (Camera cam in cameras)
                {
                    config.selectedCameraNames.Add(cam.name);
                }
            }
            if (GUILayout.Button("Deselect All"))
            {
                config.selectedCameraNames.Clear();
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("No cameras found in the scene!", MessageType.Warning);
        }

        EditorGUILayout.Space();
        
        // Resolution Settings
        GUILayout.Label("Resolution Settings", EditorStyles.boldLabel);
        
        int aspectRatioIndex = (int)config.aspectRatio;
        aspectRatioIndex = EditorGUILayout.Popup("Aspect Ratio", aspectRatioIndex, aspectRatioNames);
        config.aspectRatio = (AspectRatio)aspectRatioIndex;
        
        string aspectRatioInfo = GetAspectRatioInfo(config.aspectRatio);
        EditorGUILayout.LabelField("", aspectRatioInfo, EditorStyles.miniLabel);
        
        config.useCustomSize = EditorGUILayout.Toggle("Custom Size", config.useCustomSize);
        
        if (config.useCustomSize)
        {
            config.baseWidth = EditorGUILayout.IntField("Width", config.baseWidth);
        }
        else
        {
            int newBaseWidth = EditorGUILayout.IntSlider("Quality", config.baseWidth, 720, 3840);
            if (newBaseWidth != config.baseWidth)
            {
                if (newBaseWidth < 1280) config.baseWidth = 720;
                else if (newBaseWidth < 1920) config.baseWidth = 1280;
                else if (newBaseWidth < 2560) config.baseWidth = 1920;
                else if (newBaseWidth < 3840) config.baseWidth = 2560;
                else config.baseWidth = 3840;
            }
        }
        
        var dimensions = GetDimensions(config.aspectRatio, config.baseWidth);
        EditorGUILayout.LabelField("Output Size", $"{dimensions.width} x {dimensions.height}");

        EditorGUILayout.Space();
        
        // Format Settings
        GUILayout.Label("Format Settings", EditorStyles.boldLabel);
        config.format = (ImageFormat)EditorGUILayout.EnumPopup("Image Format", config.format);
        
        if (config.format == ImageFormat.JPG)
        {
            config.jpgQuality = EditorGUILayout.IntSlider("JPG Quality", config.jpgQuality, 1, 100);
        }
        
        EditorGUILayout.EndVertical();
    }

    private string GetAspectRatioInfo(AspectRatio ratio)
    {
        switch (ratio)
        {
            case AspectRatio.SixteenByNine:
                return "YouTube, TV, Monitors";
            case AspectRatio.NineBySixteen:
                return "TikTok, Instagram Stories, Shorts";
            case AspectRatio.OneByOne:
                return "Instagram Post, Profile Pictures";
            case AspectRatio.FourByThree:
                return "Classic TV, Presentations";
            case AspectRatio.ThreeByFour:
                return "Pinterest, Portrait";
            default:
                return "";
        }
    }

    private (int width, int height) GetDimensions(AspectRatio ratio, int baseWidth)
    {
        switch (ratio)
        {
            case AspectRatio.SixteenByNine:
                return (baseWidth, baseWidth * 9 / 16);
            case AspectRatio.NineBySixteen:
                return (baseWidth * 9 / 16, baseWidth);
            case AspectRatio.OneByOne:
                return (baseWidth, baseWidth);
            case AspectRatio.FourByThree:
                return (baseWidth, baseWidth * 3 / 4);
            case AspectRatio.ThreeByFour:
                return (baseWidth * 3 / 4, baseWidth);
            default:
                return (baseWidth, baseWidth * 9 / 16);
        }
    }

    private void CaptureAllConfigurations()
    {
        string directory = $"Assets/{THUMBNAIL_FOLDER_NAME}";
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
            Debug.Log("Thumbnails folder created");
        }

        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        int totalSuccessCount = 0;
        int totalAttempts = 0;
        
        Camera[] cameras = Camera.allCameras;

        foreach (var config in configurations)
        {
            if (config.selectedCameraNames.Count == 0)
                continue;

            var dimensions = GetDimensions(config.aspectRatio, config.baseWidth);
            
            foreach (string cameraName in config.selectedCameraNames)
            {
                totalAttempts++;
                
                // Find camera by name
                Camera cam = cameras.FirstOrDefault(c => c.name == cameraName);
                if (cam == null)
                {
                    Debug.LogWarning($"Camera '{cameraName}' not found in scene. Skipping.");
                    continue;
                }

                try
                {
                    // Create render texture
                    RenderTexture rt = new RenderTexture(dimensions.width, dimensions.height, 24);
                    RenderTexture previousRT = cam.targetTexture;
                    
                    cam.targetTexture = rt;
                    cam.Render();

                    // Read pixels from render texture
                    RenderTexture.active = rt;
                    Texture2D screenshot = new Texture2D(dimensions.width, dimensions.height, TextureFormat.RGB24, false);
                    screenshot.ReadPixels(new Rect(0, 0, dimensions.width, dimensions.height), 0, 0);
                    screenshot.Apply();

                    // Cleanup
                    cam.targetTexture = previousRT;
                    RenderTexture.active = null;
                    DestroyImmediate(rt);

                    // Encode and save
                    byte[] bytes;
                    string extension;
                    
                    if (config.format == ImageFormat.PNG)
                    {
                        bytes = screenshot.EncodeToPNG();
                        extension = "png";
                    }
                    else // JPG
                    {
                        bytes = screenshot.EncodeToJPG(config.jpgQuality);
                        extension = "jpg";
                    }

                    // Sanitize config name for filename
                    string safeConfigName = string.Join("_", config.name.Split(System.IO.Path.GetInvalidFileNameChars()));
                    string fileName = $"{directory}/{timestamp}_{safeConfigName}_{cam.name}.{extension}";
                    System.IO.File.WriteAllBytes(fileName, bytes);
                    
                    DestroyImmediate(screenshot);
                    
                    Debug.Log($"[{config.name}] Thumbnail saved: {fileName}");
                    totalSuccessCount++;
                }
                catch (Exception e)
                {
                    Debug.LogError($"[{config.name}] Failed to capture from camera {cam.name}: {e.Message}");
                }
            }
        }
        
        // Refresh asset database to show the new files
        AssetDatabase.Refresh();
        
        if (totalSuccessCount > 0)
        {
            EditorUtility.DisplayDialog("Success", 
                $"{totalSuccessCount} of {totalAttempts} thumbnail(s) saved successfully!\n\nCheck the {THUMBNAIL_FOLDER_NAME} folder.", 
                "OK");
        }
        else
        {
            EditorUtility.DisplayDialog("Error", 
                "No thumbnails were captured. Please check your configuration and camera selections.", 
                "OK");
        }
    }

    private void CreateCamera()
    {
        GameObject cam = new GameObject("ThumbnailCamera");
        Camera camera = cam.AddComponent<Camera>();
        camera.transform.position = new Vector3(0, 1.4f, -2);
        camera.transform.LookAt(Vector3.zero);
    }


    private void OnEnable()
    {
        if (EditorPrefs.HasKey(PREFS_KEY))
        {
            string json = EditorPrefs.GetString(PREFS_KEY);
            var wrapper = JsonUtility.FromJson<ConfigurationList>(json);
            if (wrapper != null)
                configurations = wrapper.items;
        }
    }

    private void OnDisable()
    {
        var wrapper = new ConfigurationList { items = configurations };
        EditorPrefs.SetString(PREFS_KEY, JsonUtility.ToJson(wrapper));
    }
}