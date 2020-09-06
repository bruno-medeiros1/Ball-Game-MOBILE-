using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
   //[HideInInspector]
    public int TotalCoins;
    [HideInInspector] 
    int CoinsWon = 0; //Variavel que guarda as moedas ganhas em cada tentativa

    public Text TotalCoinstxt; //referencia à nossa UI que mostra o total de moedas
    public Text CoinsWonTxt;//referencia à nossa UI que mostra as moedas ganhas em cada tentativa

    void Start()
    {
        /*LOAD DAS MOEDAS TOTAIS*/
        TotalCoins =  PlayerPrefs.GetInt("COINS");     
    }

    void Update()
    {
        TotalCoinstxt.GetComponent<Text>().text = "x " + TotalCoins;
        PlayerPrefs.SetInt("COINS", TotalCoins);
        CoinsWonTxt.GetComponent<Text>().text = "Won: " + CoinsWon;        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin")) 
        {
            CoinsWon++;
            TotalCoins++;
        }
    }
}
