using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int highscore;
    public TextMeshProUGUI scoreUI;//referencia ao nosso texto UI
    public TextMeshProUGUI HighscoreUI;
    public TextMeshProUGUI scoreLoseUI;

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); //apaga os valores guardados neste caso o highscore.
        highscore = PlayerPrefs.GetInt("Highscore"); //Definimos o highscore com o valor que esta guardado
    }
    private void Update()
    {
        scoreUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
        HighscoreUI.GetComponent<TextMeshProUGUI>().text = highscore.ToString();
        scoreLoseUI.text = score.ToString();
       //SAVE SYSTEM
        if(score > highscore) 
        {/*Se o score > Highscore significa que houve um recorde logo 
          temos de guardar esse valor em Highscore para depois ser lido*/
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        
        
    }
}
