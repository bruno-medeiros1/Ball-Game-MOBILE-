using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;//referência ao nosso Player
    public float distance;//variavel que vai guardar o valor de diferença na distância em relação ao player
    void LateUpdate() 
    {
        /*Usamos novamente Vector3.Lerp para conseguir uma movimentacao smooth de um ponto a outro num determinado tempo,
         mas o nossa posicao nova vai ser a do player no eixo dos z mas com um offset chamado distance que podemos personalizar no editor*/
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0, gameObject.transform.position.y, Player.gameObject.transform.position.z - distance), Time.deltaTime * 100); 
    }
}
