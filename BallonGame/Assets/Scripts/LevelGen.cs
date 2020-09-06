using System.Collections;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("Transfer");
    }
    IEnumerator Transfer() 
    {
        yield return new WaitForSeconds(1f);
        /*Passado 1 segundo, o chao muda de posicao para +200 no eixo do z para simular que seja infinito o nivel*/
        gameObject.transform.parent.position = new Vector3(0.35f, 0.56f, gameObject.transform.position.z + 200);
    }
}
