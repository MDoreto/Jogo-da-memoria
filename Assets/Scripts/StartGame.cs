using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update

    int configJogo; // Variável onde é armazenada os modos de jogo
    void Start()
    {    
        PlayerPrefs.SetInt("score",0); // Mostra o score, ou seja, o numero de tentativas realizadas
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //abre a cena principal do jogo
    public void StartWorldGame()
    {
        configJogo = GameObject.Find("gameOptions").GetComponent<Dropdown>().value;  // Mostra o menu do jogo onde é possivel escolher entre os modos de jogo disponíveis
        PlayerPrefs.SetInt("Mode", configJogo); // Seleciona-se o modo de jogo desejado
        SceneManager.LoadScene("Game"); // Carrega o jogo seguindo as regras para o modo de jogo selecionado
    }
}
