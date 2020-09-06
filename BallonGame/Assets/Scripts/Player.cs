using UnityEngine;

/*Classe que define a movimentação do jogador*/
public class Player : MonoBehaviour
{
    public float MaxSpeed;
    public float DirrectionalSpeed;
    public float SpeedModifier;
    public float JumpSpeed;
    private Rigidbody r;

    private Touch touch;

    public bool CanJump;
    public bool PlayerMovement = false;//flag para saber em que momentos podemos ativar o movimento do player 

    private void Start()
    {
        r = GetComponent<Rigidbody>();   
    }
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        //variavel auxiliar que deteta se estamos a pressionar nas teclas a ou d
        float moveHorizontal = Input.GetAxis("Horizontal");
        /*Esta linha de codigo basicamente esta a definir uma posicao nova
         so que essa posicao vai ser definida por Vector.Lerp que é responsavel por movimentar um gameobject de uma posicao para outra de forma smooth,
        para isso ele precisa de 3 coordenadas (x,y,z) so que nos no x queremos que esteja limitado uma vez que como o jogador vai movimentar na horizontal
        nao queremos que saia da tela do telemovel portanto definimos umas booundaries com o mathf.Clamp*/
        transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), 
            gameObject.transform.position.y, gameObject.transform.position.z), Time.deltaTime * DirrectionalSpeed);
#endif
        
        /*MOBILE CONTROLLERS CODE*/
        if (Input.touchCount > 0 && PlayerMovement == true)//se estiver a tocar no ecra
        {

            touch = Input.GetTouch(0);//primeiro toque na tela
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);//conversao de pixels coordinates para world coordinates
            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("DEDO A MOVIMENTAR NA TELA");
                transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x + touch.deltaPosition.x * SpeedModifier, -2.5f, 2.5f), 
                    transform.position.y, transform.position.z);//movimentamos o jogador para a posicao pretendida
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("DEDO tirado da tela");
            }
            if (touch.phase == TouchPhase.Began && CanJump == true && FindObjectOfType<JumpBoost>().jumpboost > 0)
            {
                //Debug.Log("Salto");
                Vector3 atas = new Vector3(0, 1, 0);
                r.AddForce(atas * JumpSpeed * Time.deltaTime, ForceMode.Impulse);
                FindObjectOfType<JumpBoost>().jumpboost--;
                FindObjectOfType<AudioManager>().Play("Jump");
                if (FindObjectOfType<JumpBoost>().jumpboost <= 0) 
                {
                    CanJump = false;
                }
            }

        }

        MaxSpeed += 0.002f;//aumentando a velocidade
        r.AddForce(0, 0, 1000 * Time.deltaTime);//força que impulsiona o player para a frente
        
        gameObject.transform.Rotate(Vector3.right * r.velocity.z / 3);//Rotação do objecto Player

        if (FindObjectOfType<JumpBoost>().jumpboost > 0)
        {
            CanJump = true;
            if (Input.GetKeyDown("space") )
            {
               // Debug.Log("SALTO");
                Vector3 atas = new Vector3(0, 1, 0);
                r.AddForce(atas * JumpSpeed * Time.deltaTime, ForceMode.Impulse);
                FindObjectOfType<AudioManager>().Play("Jump");
                FindObjectOfType<JumpBoost>().jumpboost--;
                if (FindObjectOfType<JumpBoost>().jumpboost <= 0)
                {
                    CanJump = false;
                }
            }
        }
        else
        { CanJump = false; }
        
    }
    private void FixedUpdate()
    {
        /*Condição para que a velocidade do player não ultrapassa a velocidade maxima*/
        if (r.velocity.magnitude > MaxSpeed)
        {
            r.velocity = r.velocity.normalized * MaxSpeed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        /*Se colidir com o Score damos play na musica respetiva*/
        if (other.gameObject.CompareTag("Score"))
        {
            FindObjectOfType<AudioManager>().Play("Score");
        }
        /*Se tocamos no triangle damos stop na musica Theme e GameOver*/
        if (other.gameObject.CompareTag("Triangle")) 
        {
            FindObjectOfType<AppInicialize>().GameOver();
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("Lose");
        }
        if (other.gameObject.CompareTag("Coin")) 
        {
            FindObjectOfType<AudioManager>().Play("Coin");
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("MissCoin")) 
        {
            //Debug.Log("miss coin");
            Destroy(other.transform.parent.gameObject);
        }
        if (other.gameObject.CompareTag("JumpPowerUP")) 
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            CanJump = true;
            

        }
        if (other.gameObject.CompareTag("MissPowerUP")) 
        {
            Debug.Log("MISS POWERUP");
            Destroy(other.transform.parent.gameObject);
        }
        
    }

}
