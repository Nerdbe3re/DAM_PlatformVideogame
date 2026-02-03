using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //------ADD THIS SCRIPT TO PLAYER COMPONENTS!!!!! --------

    public CoinManager coinManager;

    public int coins = 0; 

    public void AddCoins(int amount) // tiene traccia di quanti soldi ha il giocatore nell'inventario 
    { 
        coins += amount; 
        coinManager.UpdatecoinUI(coins);
    }


}
