using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Botones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jugar(){
        SceneManager.LoadScene(1);
    }
    public void Instrucciones(){
        SceneManager.LoadScene(2);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }

}
