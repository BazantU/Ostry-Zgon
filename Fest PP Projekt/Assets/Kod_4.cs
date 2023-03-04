using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Kod_4 : MonoBehaviour
{
    public Camera kamera_gracza;
    public TMP_Text tekst;
    public Transform kod_4_obiekt;
    public Material tlo;
    public float odleglosc;
    public int prawidlowy_kod;
    public Transform zawias;
    public GameObject glowna_kamera;
    public GameObject kamera_koniec;
    public Transform gracz_model;
    public TMP_Text koniec;
    //public Transform zawias;

    private bool mozna_wpisac = true;
    private Color poczatkowy_kolor = new Color(93f/255f, 207f/255f, 255f/255f);
    private string kod = "";
    private float poczatkowy_x;

    private void info_koniec()
    {
        koniec.enabled = true;
    }

    private void The_End()
    { 
        DOTween.Init();
        kamera_koniec.transform.DOLocalMoveZ(-92.5f, 14f).OnComplete(info_koniec);
    }

    private void powrot(Transform przycisk_0)
    {
        DOTween.Init();
        przycisk_0.DOLocalMoveX(poczatkowy_x, 0.2f);
    }

    private void obraz_kolor()
    {
        tlo.DOColor(poczatkowy_kolor, 0.5f);
    }

    void Start()
    {   
        kamera_koniec.SetActive(false);
        poczatkowy_x = kod_4_obiekt.Find("Przyciski").Find("0").localPosition.x;
        tekst.text = kod;
        DOTween.Init();
        tlo.DOColor(poczatkowy_kolor, 0f);
    }

    void Update()
    {   
        if(Input.GetMouseButtonDown(0))
        {   
            var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)
                && hit.transform.parent != null
                && hit.transform.parent.tag == "Przycisk_Kod_4"
                && hit.distance <= odleglosc
                && mozna_wpisac == true)
            {   
                var przycisk = hit.transform;
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
                    kod += n;
                    tekst.text = kod;
                }
                if(n == "Reset")
                {
                    kod = "";
                    tekst.text = kod;
                }
                if(n == "Zatwierdz")
                {   
                    if(tekst.text == prawidlowy_kod.ToString())
                    {   
                        mozna_wpisac = false;
                        kod = "";
                        tekst.text = "";
                        DOTween.Init();
                        tlo.DOColor(Color.green, 0.5f);

                        glowna_kamera.SetActive(false); kamera_koniec.SetActive(true);
                        gracz_model.localPosition = new Vector3(33.9778f, 9.325f, 14.828f);

                        DOTween.Init();
                        zawias.DOLocalRotate(new Vector3(0f, -39f, 0), 8f).OnComplete(The_End);
                    }
                    else
                    {
                        kod = "";
                        tekst.text = "";
                        DOTween.Init();
                        tlo.DOColor(Color.red, 0.5f).OnComplete(obraz_kolor);
                    }
                }

                DOTween.Init();
                przycisk.DOLocalMoveX(poczatkowy_x - 0.03f, 0.2f).OnComplete(()=>powrot(przycisk));
            }
        }
    }
}
