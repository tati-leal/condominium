using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomUnityCli : MonoBehaviour
{

    static void BuildAssetBundles()
    {

        string arg = GetArg("-executeMethod");
        Debug.Log(arg);
        //Find prefab name and get its GUID
        string guid = AssetDatabase.FindAssets(arg, null)[0];

        //Retrieve file path from GUID
        string asset_path = AssetDatabase.GUIDToAssetPath(guid);

        //Define its coordinates
        //GameObject building = AssetDatabase.LoadAssetAtPath(asset_path, typeof(GameObject)) as GameObject;
        //building.transform.localPosition = new Vector3(0, 0, 0);

        //Set bundle name into prefab
        AssetImporter.GetAtPath(asset_path).SetAssetBundleNameAndVariant(arg, "");

        //Build iOS asset bundles
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    // Helper function for getting the command line arguments
    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 2)
            {
                return args[i + 2];
            }
        }
        return null;
    }
}
