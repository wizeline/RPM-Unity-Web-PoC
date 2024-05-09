using UnityEngine;
[System.Serializable]
public class AvatarData
{
    public Vector3 position;
    public Quaternion rotation;

    public AvatarData(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    // Serialize the avatar data to a string for storage in PlayerPrefs
    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }

    // Deserialize the avatar data from a string retrieved from PlayerPrefs
    public static AvatarData Deserialize(string json)
    {
        return JsonUtility.FromJson<AvatarData>(json);
    }
}

public class AvatarManager : MonoBehaviour
{
    public GameObject avatarPrefab;
    private GameObject avatarInstance;

    public AvatarDataScriptableObject avatarDataSO;

    // Method to instantiate avatar from serialized data
    public void InstantiateAvatar(AvatarData data)
    {
        if (avatarPrefab != null)
        {
            avatarInstance = Instantiate(avatarPrefab,data.position, data.rotation);
        }
        else
        {
            Debug.Log("Avatar prefab is not assigned!");
        }
    }

    // Method to destroy the avatar instance
    public void DestroyAvatar()
    {
        if (avatarInstance != null)
        {
            Destroy(avatarInstance);
        }
    }


}