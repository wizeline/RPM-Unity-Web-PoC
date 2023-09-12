using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ReadyPlayerMe.Examples.WebGL
{
    public class DemoCanvas : MonoBehaviour
    {
        [SerializeField] private Button createAvatarButton;
        [SerializeField] private Button takePicture;
        [SerializeField] private Button changeAnimation;
        [SerializeField] private Button changeBg;
        [SerializeField] private GameObject avatar;
     

        private void Start()
        {
            WebInterface.SetIFrameVisibility(true);
           
            if (createAvatarButton != null)
            {
                createAvatarButton.onClick.AddListener(OnCreateAvatar);
            }
            if (takePicture != null)
            {
                takePicture.onClick.AddListener(OnTakeScreenshot);
            }
           if(changeAnimation != null)
            {
                changeAnimation.onClick.AddListener(Wave);
            }
            if (changeBg != null)
            {
                changeBg.onClick.AddListener(ChangeScene);
            }

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
            StartCoroutine(RecordFrame());
           
        }

        IEnumerator RecordFrame()
        {
            yield return new WaitForEndOfFrame();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            byte[] textureBytes = texture.EncodeToPNG();
            DownloadFile(textureBytes, textureBytes.Length, "screenshot.png");
            Destroy(texture);
        }

        public void OnTakeVideo()
        {
            Wave();
        }


        public void ChangeScene()
        {
            Debug.Log(SceneManager.sceneCountInBuildSettings);
            
            

            var ava1 = GameObject.Find("imported_avatar");
            SceneManager.LoadScene(1);
            Debug.Log(ava1.name);
            SceneManager.MoveGameObjectToScene(ava1, SceneManager.GetActiveScene());

        }


        public void Wave()
        {
            var avatar = GameObject.Find("imported_avatar");
            var animator = avatar.GetComponent<Animator>();
            var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
            head.SetBlendShapeWeight(1,(float)0.7);
            animator.Play("Base Layer.Wave");
            //StartCoroutine(WaveAnimation());
        }

        IEnumerator WaveAnimation()
        {
             var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
            yield return new WaitForEndOfFrame();
            head.SetBlendShapeWeight(1, 0);
        }
    }
}
