using UnityEngine;
using UnityEngine.Rendering;

public class NomeClasse : MonoBehaviour
{
    [Header("Settings")]

    public Vector3 followOffset; //per modificare la posizione della camera rispetto all'oggetto a cui è ancorata 
    [Range(0f, 1f)] public float smoothsSpeed = 0.2f; 


    [Header("Components")]

    public Transform playerTransform;//va a ricavare le posizioni del player 

    float zPosition; // intercetta qual è la posizione della z in principio (adesso è -10, quindi la console capisce che è -10) 
    Vector3 currentVelocity = Vector3.zero;


    public void Awake()
    {
        zPosition = transform.position.z;  // traduce la posizione della camera in uno dei tre vettori - e lo fa subito all'avvio (awake) 
    }

    public void LateUpdate() // LateUpdate serve per fare in modo che l'update della camera segua meglio il fixedupdate del player 
    {
        Vector3 targetPosition =  playerTransform.position + followOffset; // serve per salvare la posizione del player in x y e z per poterla poi applicare alla camera
        targetPosition.z= zPosition;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothsSpeed); 
    }

}
