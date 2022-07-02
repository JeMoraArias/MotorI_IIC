using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetection : MonoBehaviour
{
    private Rigidbody2D rig;
    [SerializeField] private GameObject _doorChild;

    private void Start()
    {
        rig = _doorChild.GetComponent<Rigidbody2D>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Debug.Log("Abrir");
            rig.velocity = new Vector2(rig.velocity.x, 15);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Cerrar");
            rig.velocity = new Vector2(rig.velocity.x, -15);
        }
    }
}
