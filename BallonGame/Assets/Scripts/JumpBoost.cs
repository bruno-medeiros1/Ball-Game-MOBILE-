using UnityEngine.UI;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public GameObject JumpTxt;
    public int jumpboost;

    void Start()
    {
        jumpboost = PlayerPrefs.GetInt("JumpBoost");
    }

    void Update()
    {
        JumpTxt.GetComponent<Text>().text =  jumpboost + "x";
        PlayerPrefs.SetInt("JumpBoost", jumpboost);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPowerUP"))
        {
            jumpboost++;         
        }
    }
}
