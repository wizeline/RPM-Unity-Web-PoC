﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace ReadyPlayerMe.Examples.WebGL
{
    public class DemoCanvas : MonoBehaviour
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
        [SerializeField] private AvatarData avatarData;
        public GameObject avatarPrefab;
        private GameObject avatarInstance;

        private void Start()
        {
                WebInterface.SetIFrameVisibility(false);
            if (AvatarDataSingleton.Instance != null && AvatarDataSingleton.Instance.avatarDataSO != null)
            {

                // Load the avatar data from the scriptable object
                string serializedData = AvatarDataSingleton.Instance.avatarDataSO.avatarData;
                AvatarData avatarData = AvatarData.Deserialize(serializedData);
                avatarPrefab = AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab;

                // Instantiate the avatar using the avatar data
               // InstantiateAvatar(avatarData);
            }
            else
            {
                Debug.Log("Avatar data not found.");
            }
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
        private void InstantiateAvatar(AvatarData data)
        {
            Debug.Log("gameObject: " + AvatarPrefabSingleton.Instance.avatarPrefabHolder);
            if (AvatarPrefabSingleton.Instance != null && AvatarPrefabSingleton.Instance.avatarPrefabHolder != null)
            {
                // Get the avatar prefab from the scriptable object          
                // Instantiate the avatar prefab
                if (avatarPrefab != null)
                {
                   // Instantiate(avatarPrefab, Vector3.zero, Quaternion.identity);
                    avatarInstance = Instantiate(avatarPrefab, data.position, data.rotation);
                    avatarInstance.SetActive(true);
                }
                
                else
                {
                    Debug.LogError("Avatar prefab is null.");
                }
            }
            else
            {
                Debug.LogError("Avatar prefab holder is null.");
            }

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
            DontDestroyOnLoad(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab);
            ToggleBgs();
            WebInterface.SetIFrameVisibility(false);
            SceneManager.LoadScene(1);
            SceneManager.MoveGameObjectToScene(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab, SceneManager.GetSceneAt(1));
        }
        public void ChangeCdmx()
        {
            DontDestroyOnLoad(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab);
            WebInterface.SetIFrameVisibility(false);
            ToggleBgs();
            SceneManager.LoadScene(2);
            SceneManager.MoveGameObjectToScene(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab, SceneManager.GetSceneByBuildIndex(2));
        }
        public void ChangeDefault()
        {
            DontDestroyOnLoad(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab);
            ToggleBgs();
            SceneManager.LoadScene(0);
            SceneManager.MoveGameObjectToScene(AvatarPrefabSingleton.Instance.avatarPrefabHolder.avatarPrefab, SceneManager.GetSceneAt(0));
            WebInterface.SetIFrameVisibility(false);
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

    }
}
