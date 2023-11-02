using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    public static T AddComponent<T>(this Component component) where T : Component =>
        component.gameObject.AddComponent<T>();

    public static T GetOrAddComponent<T>(this Component component) where T : Component =>
        component.GetComponent<T>() ?? component.AddComponent<T>();

    public static bool HasComponent<T>(this Component component) where T : Component =>
        component.GetComponent<T>() != null;
}
