using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    int configJogo;
    void Start()
    {
        PlayerPrefs.SetInt("score",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //abre a cena principal do jogo
    public void StartWorldGame()
    {
        configJogo = GameObject.Find("gameOptions").GetComponent<Dropdown>().value;
        PlayerPrefs.SetInt("Mode", configJogo);
        SceneManager.LoadScene("Game");
    }
}
