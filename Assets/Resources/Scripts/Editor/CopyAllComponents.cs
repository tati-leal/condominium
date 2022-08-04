/*
    A simple little editor extension to copy and paste all components
    Help from http://answers.unity3d.com/questions/541045/copy-all-components-from-one-character-to-another.html
    license: WTFPL (http://www.wtfpl.net/)
    author: aeroson
    advise: ChessMax
    editor: frekons
*/

#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ComponentsCopier : MonoBehaviour
{

    static List<Transform> transforms_skp = new List<Transform>(); 
    static List<Transform> transforms = new List<Transform>(); 

    [MenuItem("GameObject/Copy all components %&C")]
    static void Copy()
    {
        DisplayChildren(GameObject.Find("DW06 (1)").transform);
        DisplayChildren2(GameObject.Find("DW06").transform);

        foreach (Transform skp_transform in transforms_skp) {
            foreach (Transform broken_transform in transforms) {
                if (skp_transform.name == broken_transform.name) {
                    Component[] skp_components = skp_transform.GetComponents(typeof(Component));
                    foreach(Component skp_component in skp_components) {
                        if (skp_component.GetType() != typeof(Transform)) {
                            DestroyImmediate(broken_transform.gameObject.GetComponent(skp_component.GetType()));
                            ReplaceComponent(skp_component, broken_transform.gameObject);
                        } else {
                            ReplaceComponent(skp_component, broken_transform.gameObject);
                        }
                    }
                }
            }
        }
    }

    static void ReplaceComponent(Component skp_component, GameObject broken_transform_gameObject) {
        
        System.Type type = skp_component.GetType();

        if (type == typeof(Transform)) {
            ((Transform) broken_transform_gameObject.GetComponent(type)).localScale = ((Transform) skp_component).localScale;
            ((Transform) broken_transform_gameObject.GetComponent(type)).rotation = ((Transform) skp_component).rotation;
            return;
        }

        Component copy = broken_transform_gameObject.AddComponent(type);

        if (type == typeof(MeshFilter)) {
            ((MeshFilter) copy).sharedMesh = ((MeshFilter) skp_component).sharedMesh;
        } else if (type == typeof(Renderer)) {
            ((Renderer) copy).sharedMaterial = ((Renderer) skp_component).sharedMaterial;
        } else if (type == typeof(MeshRenderer)) {
            ((MeshRenderer) copy).sharedMaterials = ((MeshRenderer) skp_component).sharedMaterials;
            ((MeshRenderer) copy).shadowCastingMode = ((MeshRenderer) skp_component).shadowCastingMode;
            ((MeshRenderer) copy).receiveShadows = ((MeshRenderer) skp_component).receiveShadows;
            ((MeshRenderer) copy).lightProbeUsage = ((MeshRenderer) skp_component).lightProbeUsage;
            ((MeshRenderer) copy).reflectionProbeUsage = ((MeshRenderer) skp_component).reflectionProbeUsage;
            ((MeshRenderer) copy).allowOcclusionWhenDynamic = ((MeshRenderer) skp_component).allowOcclusionWhenDynamic;
        } else {

            //Copied fields can be restricted with BindingFlags
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            
            foreach (var pinfo in pinfos) 
            {
                if (pinfo.CanWrite) 
                {
                    try 
                    {
                        pinfo.SetValue(copy, pinfo.GetValue(skp_component, null), null);
                    }
                    catch
                    {
                         /*
                          * In case of NotImplementedException being thrown.
                          * For some reason specifying that exception didn't seem to catch it,
                          * so I didn't catch anything specific.
                          */
                    }
                }
            }
            
            System.Reflection.FieldInfo[] fields = type.GetFields(); 
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(skp_component));
            }

        }
    }

    static void DisplayChildren(Transform trans)
    {
        foreach(Transform child in trans) {
            //Debug.Log(child.name);
            if (child.childCount > 0) {
                transforms_skp.Add(child);
                DisplayChildren(child);
            } else {
                transforms_skp.Add(child);
            }
        }
    }

    static void DisplayChildren2(Transform trans)
    {
        foreach(Transform child in trans) {
            //Debug.Log(child.name);
            if (child.childCount > 0) {
                transforms.Add(child);
                DisplayChildren2(child);
            } else {
                transforms.Add(child);
            }
        }
    }

}
#endif