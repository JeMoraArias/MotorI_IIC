using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    [SerializeField] private float _runSpeed = 20f; // Variable que define la magnitud de velocidad a la que se deplaza el player.
    [SerializeField] private float _jumpForce = 20f; // Variable que define la fuerza que se aplica en contra de la gravedad.
    [SerializeField] private bool _facingRight = true; // Booleando que define si el jugador esta viendo a la derecha.
    [SerializeField] private Transform _groundCheck; // Variable que indica la ubicacion en xyz el pivote para detectar el suelo. 
    [SerializeField] private float _groundRadius = 05f; //Variable que define el radio de dectaccion de suelo. 
    [SerializeField] private LayerMask _layers; // Variable que permite definir los layers con los que el OverlapCircle puede interactuar
    private bool _grounded; // Booleando que define si el jugador esta o no en contacto con el piso
    private Rigidbody2D rig;  //Permite que el objeto se vea afectado por fuerzas.  
    public int playerHP = 100; //Variable que indica el valor 
    private bool shielded; //Booleando que indica si el escudo esta activo o no.
    [SerializeField] private GameObject shield; //GameObject se utiliza para referenciar y llamar el sprite del escudo que se encuentra en el juego.

    

   

    void Start()
    {
        //Referencias 
        rig = GetComponent<Rigidbody2D>();
        //Booleando que indica que el shield se encuentra desactivado al iniciar el juego.
        shielded = false;
        //Indica que shield esta desactivado al iniciar el juego.
        shield.SetActive(false); 
    }

 
    void Update()
    {
       //Cada frame que pase va a llamar a Move el cual le va a indicar el input y la velocidad a la que se mueve el jugador. 
        Move();

        //Solo se ejecuta ShieldOn() cuando el player presiona la tecla x y el escudo no se encuentra activo al mismo tiempo.
        if (Input.GetKeyDown(KeyCode.X) && shielded == false)
        {
            ShieldOn();
        }

        // Solo se ejecuta el salto cuando el jugador presiona el input y ademas esta en contacto con el suelo.
        if (Input.GetButtonDown("Jump") && _grounded == true)
        {
            Jump();
        }

        //OverlapCirle requiere una posicion y un radio que dibuja debajo del jugador para que el raycast pueda detectar el layer del suelo.    
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _layers);


    }

    private void Move()
    {
        //horizontalMove es una variable temporal que define la difereccion a la que el player se mueve, por lo que captura el input del jugador que puede ser positivo o negativo.
        float horinzontalMove = Input.GetAxisRaw("Horizontal");

        //Velocity indica a que direccion definido por horizontalMove (+/-) y a que velocidad se mueve el player definido por _runSp eed, el jugador se mueve unicamente en x.      
        rig.velocity = new Vector2(horinzontalMove * _runSpeed, rig.velocity.y);
        {
            //Si la direccion del player es positiva (derecha) y el jugador esta mirando a la izquierda entonces se ejecuta un flip hacia la derecha.
            if (horinzontalMove > 0 && _facingRight == false)
            {
                Flip();
            }
            //Si la direccion del player es negativa (izquierda) y el jugador esta mirando a la derecha entonces se ejecuta un flip hacia la izquierda.
            else if (horinzontalMove < 0 && _facingRight == true)
            {
                Flip();
            }
        }
                
    }

    private void Jump() 
    {
        //AddForce aplica una fuerza en direccion contraria a la gravedad.
        rig.AddForce(Vector2.up * _jumpForce);    
    }

    private void Flip()
    {
        _facingRight = !_facingRight; //si facingRight es verdad, despues de ejecutar esta funcion automaticamente pasa a ser falso y viceversa.
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); //le asigna un valor de -1 en x para que el sprite gire y ahora este mirando en direccion hacia -x
    }

   //Activar el escudo
    private void ShieldOn()
    {
        shielded = true; //Activa el booleano que indica que estamos escudados   
        shield.SetActive(true); //Llama al sprite del escudo.          
        Invoke("ShieldOff", 5f); //Invoke permite llamar una funcion por su nombre y ejecutarlo despues de un tiempo determinado.
       
    }
    //Desactivar el escudo
     private void ShieldOff()
    {
        shielded = false; // Desactiva el codigo del escudo
        shield.SetActive(false);  //Desactiva el sprite del escudo
        
    }

    //OnCollisionStay hace que la colisiohn sea efectiva cada frame que esten en contacto.
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Inidica que si hay una colision con el gameObject con el tag "Enemy" y si el escudo no esta activado el player recibe daño por parte del enemigo.
        if (collision.gameObject.tag == "Enemy" && shielded == false) 
        {
            //Permite referenciar el daño de cada enemigo por separado.
            playerHP -= collision.gameObject.GetComponent<EnemyBehaviour>().GetDamage();
        }
    }


}
