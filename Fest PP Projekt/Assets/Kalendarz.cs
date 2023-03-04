using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Kalendarz : MonoBehaviour
{
    public Camera kamera_gracza;
    public GameObject gracz;
    public float odleglosc;
    public bool interakcja;

    public GameObject kamera_glowna;
    public GameObject kamera_kalendarz;

    void Start()
    {   
        kamera_kalendarz.SetActive(false);
    }

    void Update()
    {   
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && hit.transform.tag == "Kalendarz"
            && hit.transform.GetComponent<Outline>() != null
            && hit.distance <= odleglosc
            && interakcja == false)
        {   
            interakcja = true;
            var item = hit.transform;
            item.GetComponent<Outline>().enabled = false;
            kamera_glowna.SetActive(false); kamera_kalendarz.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q)
            && interakcja == true)
        {   
            kamera_glowna.SetActive(true); kamera_kalendarz.SetActive(false);
            interakcja = false;
        }
    }
}
