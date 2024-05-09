using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace ReadyPlayerMe.Examples.WebGL
{
    public class DemoCanvasCreateAvatar : MonoBehaviour
    {
        [SerializeField] private Button createAvatarButton;
        [SerializeField] private Button takePicture;

        [SerializeField] private Button idleAnimation;
        [SerializeField] private Button changeAnimation;
        [SerializeField] private Button standingAnimation;

        [SerializeField] private Button toggleBgsButton;
        [SerializeField] private Button togglePoseButton;

        [SerializeField] private GameObject bgPanel;
        [SerializeField] private GameObject posePanel;

        [SerializeField] private Button gdlOffice;
        [SerializeField] private Button cdmxOffice;
        [SerializeField] private Button plainBg;

        [SerializeField] private GameObject avatar;

        private void Start()
        {
           
            WebInterface.SetIFrameVisibility(true);
            
            if (toggleBgsButton != null)
            {
                toggleBgsButton.onClick.AddListener(ToggleBgs);
            }
            if (togglePoseButton != null)
            {
                togglePoseButton.onClick.AddListener(TogglePose);
            }

            if (createAvatarButton != null)
            {
                createAvatarButton.onClick.AddListener(OnCreateAvatar);                
            }
            if (takePicture != null)
            {
                takePicture.onClick.AddListener(OnTakeScreenshot);
            }

            if (idleAnimation != null)
            {
                idleAnimation.onClick.AddListener(Idle);
            }

            if (changeAnimation != null)
            {
                changeAnimation.onClick.AddListener(Wave);
            }

            if (standingAnimation != null)
            {
                standingAnimation.onClick.AddListener(Standing);
            }

            if (gdlOffice != null)
            {
                gdlOffice.onClick.AddListener(ChangeGdl);
                gdlOffice.enabled = false;
            }
            if (cdmxOffice != null)
            {
                cdmxOffice.onClick.AddListener(ChangeCdmx);
                cdmxOffice.enabled = false;
            }
            if (plainBg != null)
            {
                plainBg.onClick.AddListener(ChangeDefault);
                plainBg.enabled = false;
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


        public void ChangeGdl()
        {
            var ava1 = GameObject.Find("imported_avatar");
            InstantiateAvatar(ava1);
            ToggleBgs();   
            SceneManager.LoadScene(1);
        }
        public void ChangeCdmx()
        {
            var ava1 = GameObject.Find("imported_avatar");
            InstantiateAvatar(ava1);
            ToggleBgs();
            SceneManager.LoadScene(2);
            SceneManager.MoveGameObjectToScene(ava1, SceneManager.GetSceneByBuildIndex(2));
        }
        public void ChangeDefault()
        {
            var ava1 = GameObject.Find("imported_avatar");
            InstantiateAvatar(ava1);
            ToggleBgs();
            SceneManager.LoadScene(0);
            SceneManager.MoveGameObjectToScene(ava1, SceneManager.GetSceneAt(0));
        }


        public void Wave()
        {
            var avatar = GameObject.Find("imported_avatar");
            var animator = avatar.GetComponent<Animator>();
            var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
            TogglePose();
            head.SetBlendShapeWeight(1, (float)0.7);
            animator.Play("Base Layer.Wave");
            //StartCoroutine(WaveAnimation());
        }

        public void Idle()
        {
            var avatar = GameObject.Find("imported_avatar");
            var animator = avatar.GetComponent<Animator>();
            var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
            TogglePose();
            head.SetBlendShapeWeight(1, (float)0.7);
            animator.Play("Base Layer.Idle");
            //StartCoroutine(WaveAnimation());
        }

        public void Standing()
        {
            var avatar = GameObject.Find("imported_avatar");
            var animator = avatar.GetComponent<Animator>();
            var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
            TogglePose();
            head.SetBlendShapeWeight(1, (float)0.7);
            animator.Play("Base Layer.Standing");
            //StartCoroutine(WaveAnimation());
        }


        public void TogglePose()
        {
            float alphaValue = posePanel.GetComponent<CanvasGroup>().alpha;

            if (alphaValue == 0)
            {
                posePanel.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                posePanel.GetComponent<CanvasGroup>().alpha = 0;
            }

            changeAnimation.enabled = true;
        }

        public void ToggleBgs()
        {
            float alphaValue = bgPanel.GetComponent<CanvasGroup>().alpha;

            if (alphaValue == 0)
            {
                bgPanel.GetComponent<CanvasGroup>().alpha = 1;
            }
            else
            {
                bgPanel.GetComponent<CanvasGroup>().alpha = 0;
            }

            gdlOffice.enabled = true;
            cdmxOffice.enabled = true;
            plainBg.enabled = true;

        }

        public void InstantiateAvatar(GameObject receivedPrefab)
        {
           
            try
            {
            Vector3 position = receivedPrefab.transform.position;
            Quaternion rotation = receivedPrefab.transform.rotation;
            AvatarData avatarData = new AvatarData(position, rotation);
            string serializedData = avatarData.Serialize();
                AvatarPrefabHolder holder = new AvatarPrefabHolder(receivedPrefab);
               
                AvatarDataSingleton.Instance.avatarDataSO.avatarData = serializedData;
                //AvatarPrefabSingleton.Instance.avatarPrefabHolder.SetGameObject(receivedPrefab);
                AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab = receivedPrefab;
                DontDestroyOnLoad(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab);
            }
            catch
            {
                Debug.Log("Error");
            }
        }
    }
}
