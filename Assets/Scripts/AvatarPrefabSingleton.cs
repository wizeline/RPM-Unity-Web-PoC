using UnityEngine;
[System.Serializable]
public class AvatarPrefabSingleton : MonoBehaviour
{
    public static AvatarPrefabSingleton Instance { get; private set; }

    public AvatarPrefabHolderScriptableObject avatarPrefabHolder;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this object persists between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
}