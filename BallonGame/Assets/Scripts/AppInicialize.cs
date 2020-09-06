using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AppInicialize : MonoBehaviour, IUnityAdsListener
{
    /*Referencias UI para colocarmos nos Menus*/
    public GameObject InMenuUI;
    public GameObject InGameUI;
    public GameObject GameOverUI;
    public GameObject InPauseUI;    
    public GameObject AdButtonUI;
    public GameObject RestartButtonUi;
    public GameObject SettingsUI;
    public GameObject ShopUI;
    public GameObject InfoUI;

    public GameObject Player;//referencia ao player

    /*Referencias a UI do nº de coins e Jump Boosters*/
    public GameObject CoinsShop;
    public GameObject JumpBoostTxt;
    
    /*Flag usadas para saber estados do jogador ou do jogo 
     em determinado momento*/
    private bool HasSeenAd = false;
    private bool HasGameStarted;

    /*Variavel usada para os Anuncios*/
    string placement = "rewardedVideo";
    private void Awake()
    {  
        Shader.SetGlobalFloat("_Curvature", 2.0f);// variaveis que fazem parte da shader que proporciona curvatura ao nosso mundo
        Shader.SetGlobalFloat("_Trimming",0.1f);//
        Application.targetFrameRate = 60;//linha de codigo para tornar o nosso jogo mais smooth
        
    }
    private void Start()
    {   /*ADS SYSTEM*/
        Advertisement.AddListener(this);
        Advertisement.Initialize("3796841", true);      
        /*Inicialmente o Player irá aparecer parado*/
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
        /*Ativamos a UI do Menu Inicial*/
        MenuButton();
    }
    /*Funcoes que ativam Menus e os seus componentes respetivos*/
    public void MenuButton() 
    {
        JumpBoostTxt.GetComponent<Text>().text =  FindObjectOfType<JumpBoost>().jumpboost + "x";
        if (HasGameStarted == true) 
        {
            InMenuUI.gameObject.SetActive(false);
            InGameUI.gameObject.SetActive(false);
            GameOverUI.gameObject.SetActive(false);
            InPauseUI.gameObject.SetActive(true);
            ShopUI.gameObject.SetActive(false);
            SettingsUI.gameObject.SetActive(false);
            InfoUI.gameObject.SetActive(false);
            CoinsShop.SetActive(false);
        }
        else 
        {
            InMenuUI.gameObject.SetActive(true);
            InGameUI.gameObject.SetActive(false);
            GameOverUI.gameObject.SetActive(false);
            InPauseUI.gameObject.SetActive(false);
            ShopUI.gameObject.SetActive(false);
            SettingsUI.gameObject.SetActive(false);
            InfoUI.gameObject.SetActive(false);
            CoinsShop.SetActive(false);
        }     
    }
    public void SettingsButton() 
    {
        InMenuUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        InPauseUI.gameObject.SetActive(false);
        ShopUI.gameObject.SetActive(false);
        SettingsUI.gameObject.SetActive(true);
        InfoUI.gameObject.SetActive(false);
    }
    public void ShopButton()
    {
        InMenuUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        InPauseUI.gameObject.SetActive(false);
        ShopUI.gameObject.SetActive(true);
        SettingsUI.gameObject.SetActive(false);
        CoinsShop.SetActive(true);
        InfoUI.gameObject.SetActive(false);
    }

    public void PauseButton() 
    {
        FindObjectOfType<Player>().PlayerMovement = false;
        HasGameStarted = true;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //Faz com o que o nosso player esteja parado
        InMenuUI.gameObject.SetActive(false);
        InPauseUI.gameObject.SetActive(true);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);


    }
    public void InfoButton() 
    {
        InMenuUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        InPauseUI.gameObject.SetActive(false);
        ShopUI.gameObject.SetActive(true);
        SettingsUI.gameObject.SetActive(false);
        CoinsShop.SetActive(false);
        InfoUI.gameObject.SetActive(true);
    }
    public void GameOver()
    {
        FindObjectOfType<Player>().PlayerMovement = false;
        HasGameStarted = true;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //Faz com o que o nosso player esteja parado
        InMenuUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(true);
        InPauseUI.gameObject.SetActive(false);
        /*Se já viu um anuncio quando voltar a morrer não pode dar mais*/
        if (HasSeenAd == true) 
        {
            AdButtonUI.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f); //transparencia a 50% da imagem do ad button
            AdButtonUI.GetComponent<Button>().enabled = false;
            AdButtonUI.GetComponent<Animator>().enabled = false;
            RestartButtonUi.GetComponent<Animator>().enabled = true;
        }
    }
    public void RestartButton() 
    {
        /*É necessário remover o Listener pq quando o jogo dá load de uma scene nova o gameobject que tinha a implementação
         do nosso monobehavior é destruido logo temos de o remover quando não precisamos dele que é numa load scene*/
        Advertisement.RemoveListener(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    public void PlayButton() 
    {
        /*Se o jogo ja começou e dermos pausa, queremos dar 1 segundo ao player se ajustar aonde ficou o jogo*/
        if(HasGameStarted == true) 
        {
            Player.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(StartGame(1.0f));            
            StartCoroutine("NoGod");
            
        }
        /*Caso estejam a iniciar do 0 queremos que seja emediato*/
        else
        {
            StartCoroutine(StartGame(0.0f));
        }
    }
    IEnumerator StartGame(float waittime)
    {
        InMenuUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(true);
        InPauseUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(false);
        ShopUI.gameObject.SetActive(false);
        SettingsUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waittime);//So vai executar o que estiver abaixo do yield passado o tempo que definirmos!

        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;//desativa todas as constrains
        FindObjectOfType<Player>().PlayerMovement = true;
        FindObjectOfType<AudioManager>().Play("Theme");
    }
    IEnumerator NoGod() 
    {
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<SphereCollider>().enabled = true;
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady(placement))
        {
            Advertisement.Show(placement);
        }
        else
        {
            //Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
           // Debug.Log("A mostrar Anuncio com sucesso");
            HasSeenAd = true;
            FindObjectOfType<Player>().MaxSpeed = 25f;
            StartCoroutine(StartGame(1.0f)); 
        }
        else if (showResult == ShowResult.Failed)
        {
            //Debug.Log("Nao foi possivel mostrar anuncio!");
        }
        else if(showResult == ShowResult.Skipped) 
        {
            //Debug.Log("Anuncio passado à frente");
        }
    }
    public void OnUnityAdsDidError(string message)
    {
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
       
    }
}
