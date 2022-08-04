#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ComponentsAttacher : MonoBehaviour
{

    [MenuItem("GameObject/Attach Mashes %&C")]
    static void Attach()
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
                        DestroyImmediate(c);
                        child.gameObject.AddComponent<MeshCollider>();
                    }
                }
                DisplayChildren(child);
            } else {
                Component[] components = child.GetComponents<Component>();
                foreach(Component c in components) {
                    if (c.GetType() == typeof(MeshCollider)) {
                        DestroyImmediate(c);
                        child.gameObject.AddComponent<MeshCollider>();
                    }
                }
            }
        }
    }
}
#endif