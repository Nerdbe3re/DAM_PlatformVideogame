using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public TextMeshProUGUI coinText;

    public void UpdatecoinUI(int coin)
    {
        coinText.text = coin.ToString(); //bisogna trasformare int coin in string in quanto poi testo
    }

}
