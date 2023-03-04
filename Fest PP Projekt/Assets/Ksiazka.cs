using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ksiazka : MonoBehaviour
{   
    public Camera kamera_gracza;
    public Transform ksiazka;
    public float odleglosc;
    public bool otwarta = false;

    void Update()
    {
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && hit.transform.tag == "Ksiazka"
            && otwarta == false      
            && hit.distance <= odleglosc)
        {   
            otwarta = true;

            DOTween.Init();
            ksiazka.DOLocalRotate(new Vector3(0f, 360f, 0f), 1f);
        }
    }
}
