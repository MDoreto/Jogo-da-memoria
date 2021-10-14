using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta;    //Minha carta a ser descartada
    private int countCartasSelecionadas=0;
    private GameObject [] cartas = new GameObject [4];
    private string[] linhasCartas = new string[4];
    bool timerPausado, timerAcionado;
    float timer;

    int numTentativas = 0;
    int numAcertos = 0;
    AudioSource somOk;

    int ultimoJogo;
    int record;
    int mode;
    int maxCartas = 2;
    string[] nipes = { "_of_clubs", "_of_hearts", "_of_spades", "_of_diamonds" };
    string[] baralhos = { "Red", "Blue" };
    // Start is called before the first frame update
    void Start()
    {
        mode = PlayerPrefs.GetInt("Mode", 0);
        if (mode==3)
            maxCartas = 4;
        MostraCartas();
        UpdateTentativas();
        somOk = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);
        record = PlayerPrefs.GetInt("Record", 0);
        

        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;
        GameObject.Find("record").GetComponent<Text>().text = "Record = " + record;
        timerPausado = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                VerificaCarta();
                timerPausado = true;
                timerAcionado = false;
                timer = 0;
            }
        }
    }
    void MostraCartas()
    {
        if (mode == 3)
        {
            int[] arrayEmbaralhado = criaArrayEmbaralhado();
            int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado3 = criaArrayEmbaralhado();
            int[] arrayEmbaralhado4 = criaArrayEmbaralhado();
            for (int i = 0; i < 13; i++)
            {
                AddUmaCarta(0, i, arrayEmbaralhado[i]);
                AddUmaCarta(1, i, arrayEmbaralhado2[i]);
                AddUmaCarta(2, i, arrayEmbaralhado3[i]);
                AddUmaCarta(3, i, arrayEmbaralhado4[i]);
            }
        }
        else
        { 
            int[] arrayEmbaralhado = criaArrayEmbaralhado();
            int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
            for (int i = 0; i < 13; i++)
            {
                AddUmaCarta(0,i, arrayEmbaralhado[i]);
                AddUmaCarta(1, i, arrayEmbaralhado2[i]);
            }
        }
    }
    void AddUmaCarta(int linha, int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y + ((linha - 2 / 2) * fatorEscalaY), centro.transform.position.z);
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor +1);
        c.name = "" + linha + "_" +valor;
        string nomeDaCarta = "";
        string numeroCarta = "";
        switch (valor)
        {
            case 0:
                numeroCarta = "ace";
                break;
            case 10:
                numeroCarta = "jack";
                break;
            case 11:
                numeroCarta = "queen";
                break;
            case 12:
                numeroCarta = "king";
                break;
            default:
                numeroCarta = "" + (valor + 1);
                break;
        }
        switch (mode)
        {
            case 0:
                nomeDaCarta = numeroCarta + nipes[linha*2];
                break;
            case 1:
                nomeDaCarta = numeroCarta + nipes[linha*2 +1];
                break;
            case 2:
                nomeDaCarta = numeroCarta + "_of_clubs";
                break;
            case 3:
                nomeDaCarta = numeroCarta + nipes[linha];
                break;
        }
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        GameObject.Find("" +linha+"_"+ valor).GetComponent<Tile>().setCartaOriginal(s1);
        if (mode == 2)
        {
            Sprite backCarta = (Sprite)(Resources.Load<Sprite>("playCardBack" + baralhos[linha]));
            GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaBack(backCarta);
        }
    }
    public int[] criaArrayEmbaralhado()
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;
        for (int t=0; t<13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;

        }
        return novoArray;
    }
    public void CartaSelecionada(GameObject carta)
    {
        if (timerPausado)
        {
            DisparaTimer();
            string linha = carta.name.Substring(0, 1);
            linhasCartas[countCartasSelecionadas] = linha;
            cartas[countCartasSelecionadas] = carta;
            cartas[countCartasSelecionadas].GetComponent<Tile>().RevelaCarta();
            countCartasSelecionadas++;
        }
    }
    public void VerificaCarta()
    {
        if (countCartasSelecionadas > 1)
        {
            if (cartas[countCartasSelecionadas - 1].tag == cartas[countCartasSelecionadas - 2].tag)
            {
                if (countCartasSelecionadas == maxCartas)
                {
                    somOk.Play();
                    numAcertos++;
                    numTentativas++;
                    for (int i = 0; i < countCartasSelecionadas; i++)
                    {
                        Destroy(cartas[i]);
                    }
                    countCartasSelecionadas = 0;
                }

            }
            else
            {
                numTentativas++;
                for (int i = 0; i < countCartasSelecionadas; i++)
                {
                    cartas[i].GetComponent<Tile>().EscondeCarta();
                }

                countCartasSelecionadas = 0;
            }
        }
        UpdateTentativas();
        if (numAcertos == 13)
        {
            PlayerPrefs.SetInt("Jogadas", numTentativas);
            if (numTentativas < record || record == 0)
            {
                PlayerPrefs.SetInt("Record", numTentativas);
                SceneManager.LoadScene("Record");
            }
            SceneManager.LoadScene("EndGame");
        }
    }
    public void DisparaTimer()
    {
        timerAcionado = true;
        timerPausado = false;
    }
    void UpdateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
