using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public Text NoMoney;
    public Text BuySucess;
    public GameObject player;
    public GameObject BuyTrailUI;

    private int HasTrail =0;

    private void Start()
    {
        /*Se for um significa que já tinha comprado entao estara sempre ativo*/
        if((HasTrail = PlayerPrefs.GetInt("num")) == 1)
        {
            player.GetComponent<TrailRenderer>().enabled = true;
            BuyTrailUI.SetActive(false);
        }
    }
    private void Update()
    {
        FindObjectOfType<AppInicialize>().CoinsShop.GetComponent<Text>().text = "* " + FindObjectOfType<Coins>().TotalCoins;
        
    }
    public void BuyJump1() 
   {
        if(FindObjectOfType<Coins>().TotalCoins >= 50 ) 
        {
            FindObjectOfType<JumpBoost>().jumpboost++;
            FindObjectOfType<Coins>().TotalCoins -= 50;
            FindObjectOfType<AudioManager>().Play("Buy");
            BuySucess.gameObject.SetActive(true);
            StartCoroutine(FadeText(BuySucess));
        }
        else 
        {
            FindObjectOfType<AudioManager>().Play("NoBuy");
            NoMoney.gameObject.SetActive(true);
            StartCoroutine(FadeText(NoMoney));
        }
   }
    public void BuyJump2()
    {
        if (FindObjectOfType<Coins>().TotalCoins >= 250)
        {
            FindObjectOfType<JumpBoost>().jumpboost += 5;
            FindObjectOfType<Coins>().TotalCoins -= 250;
            FindObjectOfType<AudioManager>().Play("Buy");
            BuySucess.gameObject.SetActive(true);
            StartCoroutine(FadeText(BuySucess));
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("NoBuy");
            NoMoney.gameObject.SetActive(true);
            StartCoroutine(FadeText(NoMoney));
        }
    }
    public void BuyJump3()
    {
        if (FindObjectOfType<Coins>().TotalCoins >= 500)
        {
            FindObjectOfType<JumpBoost>().jumpboost+=10;
            FindObjectOfType<Coins>().TotalCoins -= 500;
            FindObjectOfType<AudioManager>().Play("Buy");
            BuySucess.gameObject.SetActive(true);
            StartCoroutine(FadeText(BuySucess));
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("NoBuy");
            NoMoney.gameObject.SetActive(true);
            StartCoroutine(FadeText(NoMoney));
        }
    }
    public void BuyTrail() 
    {
        if (FindObjectOfType<Coins>().TotalCoins >= 1000)
        {
            player.GetComponent<TrailRenderer>().enabled = true;
            HasTrail = 1;
            PlayerPrefs.SetInt("num", HasTrail);
            FindObjectOfType<Coins>().TotalCoins -= 1000;
            FindObjectOfType<AudioManager>().Play("Buy");
            BuySucess.gameObject.SetActive(true);
            StartCoroutine(FadeText(BuySucess));
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("NoBuy");
            NoMoney.gameObject.SetActive(true);
            StartCoroutine(FadeText(NoMoney));
        }
    }
    IEnumerator FadeText(Text _txt) 
    {
        yield return new WaitForSeconds(2f);
        _txt.gameObject.SetActive(false);
    }
    
}
