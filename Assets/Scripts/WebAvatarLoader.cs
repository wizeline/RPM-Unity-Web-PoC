using ReadyPlayerMe;
using UnityEngine;

public class WebAvatarLoader : MonoBehaviour
{
    private GameObject avatar;
    private string AvatarURL = "https://d1a370nemizbjq.cloudfront.net/5cae00e5-5622-47c7-af0d-02028fad5beb.glb";
    private AvatarLoader avatarLoader;

    private void Start()
    {
        PartnerSO partner = Resources.Load<PartnerSO>("Partner");
        WebInterface.SetupRpmFrame(partner.Subdomain);
        avatarLoader = new AvatarLoader();
    }

    private void OnAvatarImported(GameObject avatar)
    {
        Debug.Log($"Avatar imported. [{Time.timeSinceLevelLoad:F2}]");
    }

    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        this.avatar = avatar;
        Debug.Log($"Avatar loaded. [{Time.timeSinceLevelLoad:F2}]\n\n{metaData}");
    }

    public void LoadAvatar(string avatarUrl)
    {
        AvatarURL = avatarUrl;
        avatarLoader.LoadAvatar(AvatarURL, OnAvatarImported, OnAvatarLoaded);
        if (avatar) Destroy(avatar);
    }

    public void OnWebViewAvatarGenerated(string avatarUrl)
    {
        LoadAvatar(avatarUrl);
    }
}
