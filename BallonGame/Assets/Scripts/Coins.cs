﻿using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
   //[HideInInspector]
    public int TotalCoins;
    [HideInInspector] 
    int CoinsWon = 0; //Variavel que guarda as moedas ganhas em cada tentativa

    public Text TotalCoinstxt; //referencia à nossa UI que mostra o total de moedas
    public Text CoinsWonTxt;//referencia à nossa UI que mostra as moedas ganhas em cada tentativa

    private void Start()
    {
        /*LOAD DAS MOEDAS TOTAIS*/
        TotalCoins =  PlayerPrefs.GetInt("COINS");     
    }

    private void Update()
    {
        TotalCoinstxt.GetComponent<Text>().text = TotalCoins + "x";
        PlayerPrefs.SetInt("COINS", TotalCoins);
        CoinsWonTxt.GetComponent<Text>().text = "Ganho: " + CoinsWon;        
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
