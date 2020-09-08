using UnityEngine;

public class Destroy : MonoBehaviour
{
   private GameObject player;

   private void Start()
   {
        player = GameObject.FindGameObjectWithTag("Player");
   }
    private void Update()
    {
        /*Verificação se o PLayer ja passou pelos obstaculos se sim destrui-los*/
        if(gameObject.transform.position.z < player.gameObject.transform.position.z - 6) 
        {   
            if(gameObject.tag == "Triangle") 
            {
                FindObjectOfType<Score>().score++;
                FindObjectOfType<AudioManager>().Play("Score");
            }
            Debug.Log("PONTO");
            Destroy(gameObject);
        }
        
    }
}
