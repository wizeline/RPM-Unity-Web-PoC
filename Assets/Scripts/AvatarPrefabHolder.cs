using UnityEngine;
[System.Serializable]
public class AvatarPrefabHolder
{
    public GameObject avatarPrefab;

    public AvatarPrefabHolder(GameObject receivedPrefab)
    {
        this.avatarPrefab = receivedPrefab;
    }

    // Serialize the avatar data to a string for storage in PlayerPrefs
    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }

    // Deserialize the avatar data from a string retrieved from PlayerPrefs
    public static AvatarPrefabHolder Deserialize(string json)
    {
        return JsonUtility.FromJson<AvatarPrefabHolder>(json);
    }
}