using UnityEngine;

[System.Serializable]
public class AvatarDataSingleton : MonoBehaviour
{
    public static AvatarDataSingleton Instance { get; private set; }

    public AvatarDataScriptableObject avatarDataSO;

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