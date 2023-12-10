using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_Script : MonoBehaviour
{

    public float Speed = 1f;
    public bool Touch_Ground = true;
    public bool Change = false;
    private Animator animations;


    // Start is called before the first frame update
    void Start()
    {
        animations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animations.SetFloat("Speed", Speed);
        animations.SetBool("Touch_Ground", Touch_Ground);
        animations.SetBool("Change", Change);
        
    }
}
