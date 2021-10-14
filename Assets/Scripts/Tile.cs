using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false; // indicador de carta virada ou nao
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCarta;         //Sprite da parte de tras da carta
    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        print("vc pressionou um tile");

        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
        
    }
    //carrega as cartas viradas para baixo
    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }
    //carrega as cartas viradas para cima
    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }
    //coloca o sprite da parte de tras da carta na carta selecionada
    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
    //coloca o sprite da parte da frente da carta na carta selecionada
    public void setCartaBack(Sprite novaCarta)
    {
        backCarta = novaCarta;
    }

}
