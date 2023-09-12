using ReadyPlayerMe.Core;
using UnityEngine;
using BodyType = ReadyPlayerMe.Core.BodyType;

namespace ReadyPlayerMe.Examples.WebGL
{
    [RequireComponent(typeof(WebFrameHandler))]
    public class WebGLAvatarLoader : MonoBehaviour
    {
        private const string TAG = nameof(WebGLAvatarLoader);
        private GameObject avatar;
        [SerializeField] private RuntimeAnimatorController masculineController;
        [SerializeField] private RuntimeAnimatorController feminineController;
        private string avatarUrl = "";
        private WebFrameHandler webFrameHandler;
  

        private void Start()
        {
            webFrameHandler = GetComponent<WebFrameHandler>();
            webFrameHandler.OnAvatarExport += HandleAvatarLoaded;
            webFrameHandler.OnUserSet += HandleUserSet;
            webFrameHandler.OnUserAuthorized += HandleUserAuthorized;
        }

        private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
        {
            if (avatar) Destroy(avatar);
            avatar = args.Avatar;
            avatar.name = "imported_avatar";
                SetAnimatorController(args.Metadata.OutfitGender);          
                var animator = avatar.GetComponent<Animator>();
            var eye1 =  GameObject.Find("Renderer_EyeRight").GetComponent<SkinnedMeshRenderer>();
            var eye2 = GameObject.Find("Renderer_EyeLeft").GetComponent<SkinnedMeshRenderer>();
            avatar.AddComponent<EyeAnimationHandler>();
            avatar.GetComponent<EyeAnimationHandler>().BlinkInterval = 5;
            avatar.GetComponent<EyeAnimationHandler>().BlinkDuration = (float)0.3;
            animator.Play("Base Layer.Idle");

        }
        private void SetAnimatorController(OutfitGender outfitGender)
        {
            var animator = avatar.GetComponent<Animator>();
        
            if (animator != null && outfitGender == OutfitGender.Masculine)
            {
                
                animator.runtimeAnimatorController = masculineController;             
                
            }
            else
            {
                animator.runtimeAnimatorController = feminineController;
            }
        }
        private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
        {
            SDKLogger.Log(TAG, $"Avatar Load failed with error: {args.Message}");
        }
        
        public void HandleAvatarLoaded(string newAvatarUrl)
        {
            LoadAvatarFromUrl(newAvatarUrl);
        }

        public void HandleUserSet(string userId)
        {
           // Debug.Log($"User set: {userId}");
        }

        public void HandleUserAuthorized(string userId)
        {
            //Debug.Log($"User authorized: {userId}");
        }
        
        public void LoadAvatarFromUrl(string newAvatarUrl)
        {
            var avatarLoader = new AvatarObjectLoader();
            avatarUrl = newAvatarUrl;
            avatarLoader.OnCompleted += OnAvatarLoadCompleted;
            avatarLoader.OnFailed += OnAvatarLoadFailed;
            avatarLoader.LoadAvatar(avatarUrl);
        }

        public void setMasculineStates(Animator anim)
        {
            
        }
    }
}
