using UnityEditor;
using UnityEngine;

public class ImportSettingsHelper : AssetPostprocessor
{
    const float GLOBAL_SCALE = 0.2f;

    void OnPreprocessModel()
    {
        var modelImporter = assetImporter as ModelImporter;
        if (modelImporter != null)
        {
            modelImporter.globalScale = GLOBAL_SCALE;
            modelImporter.isReadable = false;
        }
    }
}
