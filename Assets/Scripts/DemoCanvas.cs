using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Examples.WebGL
{
    public class DemoCanvas : MonoBehaviour
    {
        [SerializeField] private Button createAvatarButton;
        [SerializeField] private Button takePicture;
        [SerializeField] private Button changeAnimation;
        [SerializeField] private GameObject avatar;

        private void Start()
        {
            if (createAvatarButton != null)
            {
                createAvatarButton.onClick.AddListener(OnCreateAvatar);
            }
            takePicture.onClick.AddListener(OnTakeScreenshot);
            changeAnimation.onClick.AddListener(Wave);
        }

        public void OnCreateAvatar()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        WebInterface.SetIFrameVisibility(true);
#endif
        }

        public void OnTakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot("avatar.png",4);
        }

        public void OnTakeVideo()
        {
            
        }
        public void Wave()
        {
            var animator = avatar.GetComponent<Animator>();
            animator.Play("Base Layer.Wave");
        }
    }
}
