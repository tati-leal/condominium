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
    /// C:\Program Files\Unity\Hub\Editor\2019.4.5f1\Editor\Unity.exe -batchmode -quit -projPath "C:\UnityTestProject" -executeMethod AssetImporter.Import -path "Assets\TestFile.txt"
    /// </summary>
    public static void Import()
    {
        var args = System.Environment.GetCommandLineArgs();

        if (args != null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case ARG_ASSET_PATH:
                        {
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