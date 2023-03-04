using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Kod_2 : MonoBehaviour
{
    public Camera kamera_gracza;
    public TMP_Text tekst_2;
    public Transform kod_2_obiekt;
    public Material tlo_2;
    public float odleglosc_2;
    public int prawidlowy_kod_2;
    public Transform zawias_2;

    private bool moszna_wpisac_2 = true;
    private Color poczatkowy_kolor_2 = new Color(93f/255f, 207f/255f, 255f/255f);
    private string kod_4 = "";
    private float poczatkowy_x_2;

    private void powrot(Transform przycisk_2)
    {
        DOTween.Init();
        przycisk_2.DOLocalMoveX(poczatkowy_x_2, 0.2f);
    }

    private void obraz_kolor_2()
    {
        tlo_2.DOColor(poczatkowy_kolor_2, 0.5f);
    }

    void Start()
    {   
        poczatkowy_x_2 = kod_2_obiekt.Find("Przyciski_2").Find("0_2").localPosition.x;
        tekst_2.text = kod_4;
        DOTween.Init();
        tlo_2.DOColor(poczatkowy_kolor_2, 0f);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)
                && hit.transform.parent != null
                && hit.transform.parent.tag == "Przycisk_Kod_2"
                && hit.distance <= odleglosc_2
                && moszna_wpisac_2 == true)
            {   
                var przycisk_4 = hit.transform;
                string n = hit.transform.name;
                
                if(n == "0" 
                    || n == "1" 
                    || n == "2" 
                    || n == "3" 
                    || n == "4" 
                    || n == "5" 
                    || n == "6" 
                    || n == "7" 
                    || n == "8" 
                    || n == "9")
                {
                    kod_4 += n;
                    tekst_2.text = kod_4;
                }
                if(n == "Reset")
                {
                    kod_4 = "";
                    tekst_2.text = kod_4;
                }
                if(n == "Zatwierdz")
                {   
                    if(tekst_2.text == prawidlowy_kod_2.ToString())
                    {   
                        moszna_wpisac_2 = false;
                        kod_4 = "";
                        tekst_2.text = "";
                        DOTween.Init();
                        tlo_2.DOColor(Color.green, 0.5f);
                        DOTween.Init();
                        zawias_2.DOLocalRotate(new Vector3(0f, 125f, 0), 2f);
                    }
                    else
                    {
                        kod_4 = "";
                        tekst_2.text = "";
                        DOTween.Init();
                        tlo_2.DOColor(Color.red, 0.5f).OnComplete(obraz_kolor_2);
                    }
                }

                DOTween.Init();
                przycisk_4.DOLocalMoveX(poczatkowy_x_2 - 0.03f, 0.2f).OnComplete(()=>powrot(przycisk_4));
            }
        }
    }
}
