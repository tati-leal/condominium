#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ComponentsEmptyMeshColliderRemover : MonoBehaviour
{

    [MenuItem("GameObject/Remove Empty Mesh Colliders %&C")]
    static void Remove()
    {
        DisplayChildren(GameObject.Find("SO FAR").transform);
    }

    static void DisplayChildren(Transform trans)
    {
        foreach(Transform child in trans) {
            if (child.childCount > 0) {
                Component[] components = child.GetComponents<Component>();
                foreach(Component c in components) {
                    if (c.GetType() == typeof(MeshCollider)) {
                        if (((MeshCollider)c).sharedMesh == null) {
                            DestroyImmediate(c);
                        }
                    }
                }
                DisplayChildren(child);
            } else {
                Component[] components = child.GetComponents<Component>();
                foreach(Component c in components) {
                    if (c.GetType() == typeof(MeshCollider)) {
                        if (((MeshCollider)c).sharedMesh == null) {
                            DestroyImmediate(c);
                        }
                    }
                }
            }
        }
    }
}
#endif