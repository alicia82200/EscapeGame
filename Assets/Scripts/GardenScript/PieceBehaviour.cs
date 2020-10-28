using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PieceBehaviour : MonoBehaviour
{
    public int numero;
    public GameObject piece;
    public GameObject textNum;
    public List<Material> pieceMat;

    // Start is called before the first frame update
    void Start()
    {
        textNum.GetComponent<TextMeshPro>().SetText(numero.ToString());
        piece.GetComponent<Renderer>().material = pieceMat[numero - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
