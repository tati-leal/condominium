using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomUnityCli : MonoBehaviour
{

    static void BuildAssetBundles()
    {
        //Find prefab name and get its GUID
        string guid = AssetDatabase.FindAssets("DW06", null)[0];

        //Retrieve file path from GUID
        string asset_path = AssetDatabase.GUIDToAssetPath(guid);

        //Set bundle name into prefab
        AssetImporter.GetAtPath(asset_path).SetAssetBundleNameAndVariant("dw06", "");

        //Build iOS asset bundles
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
}
