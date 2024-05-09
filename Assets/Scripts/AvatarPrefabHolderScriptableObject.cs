using UnityEngine;

[CreateAssetMenu(fileName = "AvatarPrefabHolder", menuName = "Avatar Prefab Holder", order = 1)]
public class AvatarPrefabHolderScriptableObject : ScriptableObject
{
    public GameObject avatarPrefab;
    public void SetGameObject(GameObject go)
    {
        avatarPrefab = go;
    }
    public GameObject GetGameObject()
    {
        return avatarPrefab;
    }
}