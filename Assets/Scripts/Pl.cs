using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class Pl : MonoBehaviour
{
    float step;
    public AudioClip audioStart, audioLose, audioEat,audioWin;
    AudioSource fuenteDeAudio;
    bool growingPending;
    bool colision = false;
    public GameObject tailPrefab;
    public GameObject foodPrefab;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject topSide;
    public GameObject bottomSide;
    public float timer = 10;
    public TextMeshProUGUI textoTimerPro;
	public TextMeshProUGUI ganaste;
    public List<Transform> tail = new List<Transform>();
    public TextMeshProUGUI temporizador;
    public BoxCollider2D gridArea;
    
    Vector3 lastPos;
    Controls controls;
    enum Controls
    {
        up,
        down,
        left,
        right
    }
    // Start is called before the first frame update
    void Start()
    {
        fuenteDeAudio = GetComponent<AudioSource>();
        fuenteDeAudio.clip = audioStart;
	    fuenteDeAudio.Play();
        
        CreateFood();
        
        step = GetComponent<SpriteRenderer>().bounds.size.x;
        StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.RightArrow)){
            controls = Controls.right;
            }else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            controls = Controls.left;
            }else if (Input.GetKeyDown(KeyCode.UpArrow)){
            controls = Controls.up;
            }else if (Input.GetKeyDown(KeyCode.DownArrow)){
            controls = Controls.down;
            }
            timer -= Time.deltaTime;
            if(int.Parse(textoTimerPro.text)==0){
            ganaste.text="Ganaste!";
            
            StartCoroutine(win());
        }

	if(timer>=0 && ganaste.text.Length <=0){
        textoTimerPro.text = "" + timer.ToString("f0");
	}

            
    }

    void FixedUpdate(){
       
            
	
	}
    IEnumerator win()
    	{
     
        //Wait for 8 seconds
        fuenteDeAudio.clip = audioWin;
	    fuenteDeAudio.Play();
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(0);



    	}
    IEnumerator waiter()
    	{
     
        //Wait for 8 seconds
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);



    	}

    private void Move(){
        lastPos = transform.position;
        Vector3 nextPos = Vector3.zero;

    
        if (controls == Controls.left && colision == false){
        nextPos = Vector3.left;
       
        }else if (controls == Controls.right && colision == false){
        nextPos = Vector3.right;
        }else if (controls == Controls.up && colision == false){
        nextPos = Vector3.up;
        }else if (controls == Controls.down && colision == false){
        nextPos = Vector3.down;
        }else{
            
            nextPos = Vector3.zero;
            
        }
        if(int.Parse(textoTimerPro.text)==0){
            
            nextPos = Vector3.zero;
            
        }
        transform.position += nextPos * step;
        MoveTail();
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
    if (collision.gameObject.tag == "Food"){
      growingPending = true;
      Destroy(collision.gameObject);
      fuenteDeAudio.clip = audioEat;
      fuenteDeAudio.Play();
      CreateFood();
      
    }
    

    if (collision.gameObject.tag == "Limit" || collision.gameObject.tag == "Tail"){
      colision = true;
      fuenteDeAudio.clip = audioLose;
	        fuenteDeAudio.Play();
            ganaste.text="Perdiste!";
            StartCoroutine(waiter());
      
    }

    }
    void MoveTail(){
        
    for (int i = 0; i < tail.Count; i++){
      Vector3 temp = tail[i].position;
      tail[i].position = lastPos;
      lastPos = temp;
    }
    if (growingPending) CreateTail();
    }

void CreateTail(){
    GameObject newTail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
    newTail.name = "Tail_" + tail.Count;
    tail.Add(newTail.transform);
    growingPending = false;
}

void CreateFood (){
Vector2 pos = new Vector2(Random.Range(leftSide.transform.position.x, rightSide.transform.position.x),
Random.Range(topSide.transform.position.y,bottomSide.transform.position.y)
);

Instantiate(foodPrefab,pos,Quaternion.identity);
}
    IEnumerator MoveCoroutine(){
        while(true){
            yield return new WaitForSeconds(0.10f);
            Move();
        }
    }


    


}
