using UnityEngine;
public static class ExplorationPlayerState
{
    private static Vector3 savedPosition;
    private static Quaternion savedRotation;

    public static bool HasSavedTransform { get; private set; }

    public static void Save(Transform playerTransform)
    {
        if (playerTransform == null)
            return;

        savedPosition = playerTransform.position;
        savedRotation = playerTransform.rotation;
        HasSavedTransform = true;
    }

    public static void Restore(Transform playerTransform)
    {
        if (!HasSavedTransform || playerTransform == null)
        {
            return;
        }

        playerTransform.SetPositionAndRotation(savedPosition, savedRotation);
        HasSavedTransform = false;
    }

    public static void Clear()
    {
        savedPosition = Vector3.zero;
        savedRotation = Quaternion.identity;
        HasSavedTransform = false;
    }
}
