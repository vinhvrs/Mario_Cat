using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Mario_Script : MonoBehaviour
{
    public float velo = 5f;
    private bool Turn_around = false;
    private float Speed = 0;
    private float Run_Jump = 0;
    private float pre_y = -7.975002f;
    private bool Touch_Ground = true;
    private bool Change = false;
    private bool Jump = false;
    private Rigidbody2D body;
    private Animator animations;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animations.SetFloat("Speed", Speed);
        animations.SetBool("Touch_Ground", Touch_Ground);
        animations.SetBool("Change", Change);
        animations.SetBool("Jump", Jump);
        pre_y = body.velocity.y;
        Jump_Up();  
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float Horizontal_Move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2 (velo*Horizontal_Move, body.velocity.y);
        Speed = Mathf.Abs(velo*Horizontal_Move);
        Run_Jump = Speed;
        if (Horizontal_Move > 0 && Turn_around) Change_Direct();
        if (Horizontal_Move < 0 && !Turn_around) Change_Direct();
        if (Speed == 0 )
        {
            Jump = false;
            //Touch_Ground = true;
        }
    }

    public void Change_Direct()
    {
        Turn_around = !Turn_around;
        Vector2 Direction = transform.localScale;
        Direction.x *= -1;
        transform.localScale = Direction;
    }

    public void Jump_Up()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && Touch_Ground && Jump == false)
        {
            Jump = true;
            Touch_Ground = false;
            if (Run_Jump == 0)
            {
                Run_Jump = 4;
            }
            body.AddForce((Vector2.up) * Run_Jump * 100);
        }
        if (body.velocity.y == pre_y)
        {
            Touch_Ground = true;
        }
    }

}
