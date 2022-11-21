using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetImporter : MonoBehaviour
{

    private const string ARG_ASSET_PATH = "-path";
    private const string ASSETS_DIRECTORY = "Assets";

    /// <summary>
    /// Used to import an asset via command line invocation of Unity, like so:
    /// C:\Program Files\Unity\Hub\Editor\2019.4.5f1\Editor\Unity.exe  -projectPath . -quit -batchmode -nographics -username "$UNITY_USERNAME" -password "$UNITY_PASSWORD" -serial $SERIAL_NUMBER -executeMethod AssetImporter.Import -path "$FILE_PATH" -logFile /dev/stdout
    /// </summary>
    public static void Import()
    {
        Debug.Log("Method AssetImporter.Import called...");
        var args = System.Environment.GetCommandLineArgs();

        if (args != null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case ARG_ASSET_PATH:
                        {
                            Debug.Log("Method AssetImporter.Import has -path");
                            if (++i < args.Length)
                            {
                                var assetPath = args[i];

                                assetPath = assetPath.TrimStart('/').TrimStart('\\');
                                if (!assetPath.StartsWith(ASSETS_DIRECTORY))
                                {
                                    Debug.LogError($"Unable to import '{assetPath}' as it is not under the {ASSETS_DIRECTORY} directory");
                                }
                                else
                                {
                                    Debug.Log($"Importing '{assetPath}'...");
                                    AssetDatabase.ImportAsset(assetPath);
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}