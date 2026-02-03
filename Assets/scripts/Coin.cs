using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) //indica quando un oggetto entra in trigger 
    {
        if (collision.CompareTag("Player")) //questo per verificare che accada solo quando ci entra a contatto un oggetto con uno specifico tag 
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>(); 
            //nel momento della collisione va a prendere l'oggetto PlayerInventory x interagirci

            inventory.AddCoins(1); //aggiunge 1 pz a inventario 

            Destroy(gameObject); //distrugge coin

        }
    }

    private void OnTriggerExit2D(Collider2D collision) //indica quando esce dal trigger 
    {
        
    }

}
