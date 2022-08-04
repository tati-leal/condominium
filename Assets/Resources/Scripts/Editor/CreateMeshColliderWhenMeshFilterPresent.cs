#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ComponentsCreateMeshCollider : MonoBehaviour
{

    [MenuItem("GameObject/Create MeshCollider on MeshFilter %&C")]
    static void CreateMeshCollider()
    {
        DisplayChildren(GameObject.Find("SO FAR").transform);
    }

    static void DisplayChildren(Transform trans)
    {
        foreach(Transform child in trans) {
            if (child.childCount > 0) {
                Component[] components = child.GetComponents<Component>();
                foreach(Component c in components) {
                    if (c.GetType() == typeof(MeshFilter)) {
                        DestroyImmediate(child.gameObject.GetComponent<MeshCollider>());
                        MeshCollider m = child.gameObject.AddComponent<MeshCollider>();
                        m.sharedMesh = ((MeshFilter)c).sharedMesh;
                    }
                }
                DisplayChildren(child);
            } else {
                Component[] components = child.GetComponents<Component>();
                foreach(Component c in components) {
                    if (c.GetType() == typeof(MeshFilter)) {
                        DestroyImmediate(child.gameObject.GetComponent<MeshCollider>());
                        MeshCollider m = child.gameObject.AddComponent<MeshCollider>();
                        m.sharedMesh = ((MeshFilter)c).sharedMesh;
                    }
                }
            }
        }
    }
}
#endif