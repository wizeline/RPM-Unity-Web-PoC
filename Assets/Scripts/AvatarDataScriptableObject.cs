using UnityEngine;

[CreateAssetMenu(fileName = "AvatarData", menuName = "Avatar Data", order = 1)]
public class AvatarDataScriptableObject : ScriptableObject
{
    public string avatarData;

   
    public void SetAvatarData(string data)
    {
        avatarData = data;
    }

    public string GetAvatarData()
    {
        return avatarData;
    }
}