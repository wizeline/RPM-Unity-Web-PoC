using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            WebInterface.SetIFrameVisibility(true);
           
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

        [DllImport("__Internal")]
        private static extern void DownloadFile(byte[] array, int byteLength, string fileName);
        public void OnTakeScreenshot()
        {            
           var texture = ScreenCapture.CaptureScreenshotAsTexture();
           byte[] textureBytes=texture.EncodeToPNG();
           DownloadFile(textureBytes, textureBytes.Length,"screenshot.png");
           Destroy(texture);
        }

        public void OnTakeVideo()
        {
            
        }
        public void Wave()
        {
            var avatar = GameObject.Find("imported_avatar");
            var animator = avatar.GetComponent<Animator>();
            animator.Play("Base Layer.Wave");
        }
    }
}
