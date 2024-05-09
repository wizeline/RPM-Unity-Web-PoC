using ReadyPlayerMe.Core;
using System;
using UnityEngine;

namespace ReadyPlayerMe.Examples.WebGL
{
    [RequireComponent(typeof(WebFrameHandler))]
    public class WebGLAvatarLoader : MonoBehaviour
    {
        public static WebGLAvatarLoader Instance;
        private const string TAG = nameof(WebGLAvatarLoader);
        private GameObject avatar;
        [SerializeField] private RuntimeAnimatorController masculineController;
        [SerializeField] private RuntimeAnimatorController feminineController;
        [SerializeField] private AvatarData avatarData;
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
            if (avatar&&avatar.name!= "imported_avatar") 
                Destroy(avatar);
            avatar = args.Avatar;
                SetAnimatorController(args.Metadata.OutfitGender);

            avatar.name = "imported_avatar";
            //avatarManager.InstantiateAvatar(avatarData);
            var animator = avatar.GetComponent<Animator>();
            var eye1 =  GameObject.Find("Renderer_EyeRight").GetComponent<SkinnedMeshRenderer>();
            var eye2 = GameObject.Find("Renderer_EyeLeft").GetComponent<SkinnedMeshRenderer>();            
            avatar.AddComponent<EyeAnimationHandler>();
            avatar.GetComponent<EyeAnimationHandler>().BlinkInterval = 5;
            avatar.GetComponent<EyeAnimationHandler>().BlinkDuration = (float)0.3;
            animator.applyRootMotion = false;
            animator.Play("Base Layer.Idle");

        }

        public static explicit operator WebGLAvatarLoader(GameObject v)
        {
            throw new NotImplementedException();
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

            var avatarLoader = new AvatarObjectLoader
            {
                AvatarConfig = Resources.Load<AvatarConfig>("CustomAvatarConfig")
            };
            avatarUrl = newAvatarUrl;
                avatarLoader.OnCompleted += OnAvatarLoadCompleted;
                avatarLoader.OnFailed += OnAvatarLoadFailed;
            
                avatarLoader.LoadAvatar(avatarUrl);
        
        }
    }
}
