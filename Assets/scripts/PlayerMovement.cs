using UnityEditor;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction; // libreria dalla quale dipende e alla uqale fa riferimento CallbackContext



public class PlayerMovement : MonoBehaviour
{

    [Header("General Settings")]

    public float playerSpeed = 10; //mettendolo public questo diventa visibile anche nele uimpostazioni del component 
    public float jumpForce = 10;


    [Header("Gravity Settings")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Ground Check")]

    bool isGrounded; //<-- bool che verifica se giocatore è a terra (per fare in modo non faccia doppi salti) 
    public LayerMask groundLayer; 
    public Transform groundCheckTransform;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f); //<- determina le dimensioni del ground check 

    [Header("Components")]

    public Animator playerAnimator;
    public SpriteRenderer playerRenderer; 



    public Rigidbody2D body; //da un nome al rigidbody impostato nei component per poterlo usare nello script

    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckTransform.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true;
        } // richiede punto, dimensione e angolo (0), ci dice cosa è colliso
        else
        {
            isGrounded = false;
        }//qualsiasi altro risultato determina rende falsa la collisione 
    }

    float horizontalMovement = 0;

    /*

    public void Start()
    {
        body.position = transform.position;
    } */

    //oppure 

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();// va a prendere il componente indicato se è presente anch'esso sull'oggetto a cui è applicato lo script
    }


    public void FixedUpdate() //<-- fa rifertimento a una fisica fissa che è più precisa del normale update, preferisce rallentare il rendering per tenere fisso il tempo
    {
        body.linearVelocityX = horizontalMovement * playerSpeed;
        GroundCheck();
        setGravity();
    }

    public void Update()
    {
        playerAnimator.SetFloat("XSpeed",Mathf.Abs(body.linearVelocityX)); // rende il valore assoluto in modo che non calcoli il tornare indietro come meno di zero
        if (body.linearVelocityX < 0)
        {
            bool needFlip = body.linearVelocityX > 0;
            playerRenderer.flipX = needFlip;
        }
    }

    private void setGravity()
    {
        if( body.linearVelocityY < 0 )
        {
            body.gravityScale = baseGravity * fallSpeedMultiplier; 
            body.linearVelocityY= Mathf.Max (body.linearVelocityY, -maxFallSpeed); //MathF.Max restituisce il valore massimo dei due riportati
        }
    }


    // -------- sotto modo che però preferiamo non usare perchè andiamo ad utilizzare il rigidbody al suo posto --------

    //Update = metodo chiamato ad ogni frame per dire o di aggiungere un valore o di toglierlo in base al key premuto per muoverlo 
    // a sx va a -1, a dx va a +1, se fermo rimane 0 - MOLTIPLICATO X LA SPEED (di default adesso messa a 10) - E MOLTIPLICATO PER il tempo trascorso dall'ultmo frame 

    //Time.deltaTime fa in modo che conti i fame indipendetemente dagli fps del gioco effettivo (tempo percorso dll'ultimo update) - usato solo xk siamo nell'update 

    

    /*
    public void Update()
    {
        transform.position += new Vector3(horizontalMovement * playerSpeed * Time.deltaTime, 0, 0);
    } */


    public void PlayerInput_Move(CallbackContext context) // 'ctrl + .' selezionando CallbackContext = trova la libreria e lo fa funzionare 
    {
        // vettore2 è un vettore composto da un float xe un float y 
        //qui sotto si va a riprendere quello x 

        horizontalMovement = context.ReadValue<Vector2>().x;

    }   

    public void PlayerInput_Jump (CallbackContext context)
    {
        if (isGrounded) // <-- serve per fare in modo che debba per forza riaterrare per fare un nuovo salto 
        {
            if (context.performed) //mettendo solo performed fa capire alla console che va eseguito solo nel momento che si preme
            {
                body.linearVelocityY = jumpForce;
            }
           
        }

        if (context.canceled && body.linearVelocityY > 0)
        {
            body.linearVelocityY = body.linearVelocityY / 2;
        }
    }

    public void OnDrawGizmos() //<-- i gizmo sono i simboli e le cose visibili nell'interfaccia diunity, (esempio: le freccette colorate xyz, l'icona della camera etc) 
    {
        Gizmos.DrawCube(groundCheckTransform.position, groundCheckSize); //chiede posizione e grandezza, definiti prima 
    }


}


