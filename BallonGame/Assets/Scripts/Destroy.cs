using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        /*Verificação se o PLayer ja passou pelos obstaculos se sim destrui-los*/
        if(gameObject.transform.position.z < player.transform.position.z - 6) 
        {   
            if(gameObject.tag == "Triangle") 
            {
                FindObjectOfType<Score>().score++;
            }
            Destroy(gameObject);
        }
        
    }
}
