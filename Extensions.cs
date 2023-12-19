using UnityEngine;

internal static class Extensions
{
    public static T TryGetComponentInParent<T>(this Collider collider) where T : Component
    {
        Transform currentTransform = collider.transform;

        while (currentTransform != null)
        {
            T component = currentTransform.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            currentTransform = currentTransform.parent;
        }

        return null;
    }

    public static bool TryGetComponentInParent<T>(this Collider collider, out T component) where T : Component
    {
        component = null;
        Transform currentTransform = collider.transform;

        while (currentTransform != null)
        {
            component = currentTransform.GetComponent<T>();
            if (component != null)
            {
                return true;
            }

            currentTransform = currentTransform.parent;
        }

        return false;
    }
}