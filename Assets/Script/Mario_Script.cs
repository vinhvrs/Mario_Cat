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
    private float gra_down = 4;
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && Touch_Ground && !Jump)
        {
            Jump = true;
            Touch_Ground = false;
            if (Run_Jump == 0)
            {
                Run_Jump = 4;
            }
            if (Run_Jump < 3)
            {
                Run_Jump += 2;
            }
            body.AddForce((Vector2.up) * Run_Jump * 100);
        }
        if (body.velocity.y < 0)
        {
            print("DOWN");
            body.velocity += Vector2.up * Physics2D.gravity.y * (gra_down - 1) * Time.deltaTime;
        }
        if (body.velocity.y >= 0 && Input.GetKey(KeyCode.UpArrow))
        {
            // Gravity = default
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Touch_Ground = true;
        Jump = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Jump = false;
        Touch_Ground = true;
    }

}
