using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
       
    public GameObject[] TrianglePrefabs; //lista dos prefabs do triangle
    public GameObject CoinPrefab;
    public GameObject JumpUpPrefab;

    private Vector3 SpawnObs_Position;//guarda a posicao de cada prefab da lista trianglePrefabs (0,0,0)
    private Vector3 SpawnCoin_Pos;//Guarda a posicao do prefab Coin (inicial = (0,0,0))
    private Vector3 SpawnJump_Pos;
    public GameObject player;

    
    // Update is called once per frame
    void Update()
    {
        float distancetoHorizon_2 = Vector3.Distance(player.transform.position, SpawnCoin_Pos);
        /*Variavel que guarda a distancia entre um ponta A e B, neste caso distancia do player em relação ao SpawnObs_Position*/
        float distancetoHorizon = Vector3.Distance(player.transform.position, SpawnObs_Position);

        float distancetoHorizon_3 = Vector3.Distance(player.transform.position, SpawnJump_Pos);
       // Debug.Log(SpawnObs_Position);
        /*Quanto maior for a distancia mais prefabs vai carregar
         Enquanto não estiverem o Player e O vector SpawnObs a uma distância  de pelo menos 50 
        irá colocar um prefab lembrando que inicialmente SpawnObs tem valor(0,0,0) portanto como a 
        distancia continua inferior a 50 pois num primeiro instante estão à mesma distancia e num segundo
        instante estao a 25 unidades de distancia logo irá carregar 2 prefabs*/
        if(distancetoHorizon < 51)
        {
            SpawnTriangles();           
        }
        if (distancetoHorizon_2 < 51) 
        {
            SpawnCoin();
        }
        if (distancetoHorizon_3 < 20) 
        {
            SpawnJump();
        }
        

    }
    void SpawnTriangles() 
    {
        SpawnObs_Position = new Vector3(0, 0, SpawnObs_Position.z + 25);//O mais 25 é a distancia entre cada set de Triangulos
        Instantiate(TrianglePrefabs[Random.Range(0, TrianglePrefabs.Length)] , SpawnObs_Position, Quaternion.identity);//inicializa o prefab com a posicao do spawnobs e rotacao default
    }
    /*Função que carrega automaticamente as moedas em runtime*/
    void SpawnCoin() 
    {
        SpawnCoin_Pos = new Vector3(Random.Range(-2, 2.7f), 1, (SpawnCoin_Pos.z+75) - Random.Range(4,8));
        
        Instantiate(CoinPrefab, SpawnCoin_Pos, Quaternion.identity);
    }
    void SpawnJump() 
    {
        SpawnJump_Pos = new Vector3(Random.Range(-2, 2.7f), 1f, SpawnJump_Pos.z + 260);
        Instantiate(JumpUpPrefab, SpawnJump_Pos, Quaternion.identity);
    }
}
