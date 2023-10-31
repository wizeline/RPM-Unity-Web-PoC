using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimations : MonoBehaviour
{
    [SerializeField] private GameObject avatar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Wave()
    {
        var avatar = GameObject.Find("imported_avatar");
        var animator = avatar.GetComponent<Animator>();
        var head = GameObject.Find("Renderer_Head").GetComponent<SkinnedMeshRenderer>();
        head.SetBlendShapeWeight(1, (float)0.7);
        animator.Play("Base Layer.Wave");
        //StartCoroutine(WaveAnimation());
    }
}
