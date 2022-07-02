using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 20f;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 05f;
    [SerializeField] private LayerMask _layers;
    private bool _grounded;
    private Rigidbody2D rig;    
    public int playerHP = 100;

    

   
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        /*if (Input.GetButton("Jump")) 
        {
            Jump();
        }*/

        if (Input.GetButtonDown("Jump") && _grounded == true)
        {
            Jump();
        }

        /*if (Input.GetButtonUp("Jump"))
        {
            Jump();
        }*/

        /* if(Input.GetKeyDown(KeyCode.X))
         {
             Jump();
         }
         */

        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _layers);


    }

    private void Move()
    {
        float horinzontalMove = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(horinzontalMove * _runSpeed, rig.velocity.y);
    }

    private void Jump() 
    {
        rig.AddForce(Vector2.up * _jumpForce);    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
           playerHP -= collision.gameObject.GetComponent<EnemyBehaviour>().GetDamage();
        }
    }


}
