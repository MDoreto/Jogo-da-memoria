using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Abre a Cena com os creditos
    public void StartCredits()
    {
        //Mostra os créditos do jogo, com o nome dos integrantes do grupo
        SceneManager.LoadScene("Credits");
    }
}
