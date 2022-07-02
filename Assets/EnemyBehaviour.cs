using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 20f;
    [SerializeField] private float horizontalMove = -1f;
    [SerializeField] protected int enemyDamage = 1;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layers;
    private Vector2 _direction;
    private Transform _target;
    private bool _detected ;
    private Rigidbody2D rig;

    private void Start()
    {
        _target = GameObject.FindObjectOfType<PlayerControl>().GetComponent<Transform>();

        rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 targetPos = _target.position;
        _direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _direction, _range, _layers);
        if (rayInfo) 
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(transform.position, _direction * _range, Color.red);
                rig.velocity = new Vector2(horizontalMove * _runSpeed, rig.velocity.y);
                Debug.Log("Lo veo");
                _detected = true;
            }
            else 
            {
                _detected = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range) ;  
    }

    public int GetDamage() 
    {
        return enemyDamage;
    }

    
 }
