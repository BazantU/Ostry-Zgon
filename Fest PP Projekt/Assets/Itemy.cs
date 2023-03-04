using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemy : MonoBehaviour
{   
    public Camera kamera_gracza;
    public Transform reka_gracza;
    private List<GameObject> przedmioty_lista = new List<GameObject>(); 
    public float odleglosc;
    private bool trzyma_przedmiot = false;

    void Update()
    {
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) 
            && hit.transform.tag == "Item"
            && hit.transform.GetComponent<Outline>() != null
            && hit.distance <= odleglosc)
        {   
            var przedmiot = hit.transform;
            przedmiot.GetComponent<Outline>().enabled = true;

            if (!przedmioty_lista.Contains(przedmiot.gameObject))
            {
                przedmioty_lista.Add(przedmiot.gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && trzyma_przedmiot == false
            && hit.transform.tag == "Item"
            && hit.transform.GetComponent<Outline>() != null
            && hit.transform.GetComponent<Rigidbody>() != null
            && hit.distance <= odleglosc)
        {   
            trzyma_przedmiot = true;
            reka_gracza.localPosition = new Vector3(0.474f, -0.418f, 0.964f); //podstawowa wartosc
            reka_gracza.localRotation = Quaternion.Euler(0f, 0f, 0f); //podstawowa wartosc

            var item = hit.transform;
            item.GetComponent<Outline>().enabled = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.SetParent(reka_gracza, true);

            item.localPosition = new Vector3(0f, 0f, 0f);
            item.localRotation = Quaternion.Euler(Vector3.zero);

            //Specjalne przedmioty
            if(item.name == "Papier7312"
            || item.name == "annn"
            || item.name == "re"
            || item.name == "gm")
            {reka_gracza.localRotation = Quaternion.Euler(-90f, 0f, 0f);}

            if(item.name == "kartkasejf"
            || item.name == "jaszczomb3"
            || item.name == "werre")
            {reka_gracza.localRotation = Quaternion.Euler(-90f, 180f, 0f);}

            if(item.name == "Jagiello"
            || item.name == "Wielkie_czyny"
            || item.name == "Mieszko"
            || item.name == "Szarza"
            || item.name == "Rybak"
            || item.name == "Chrobry")
            {reka_gracza.localRotation = Quaternion.Euler(180f, 0f, 180f);
            reka_gracza.localPosition = new Vector3(0.474f, -0.234f, 0.964f);} //-0.234

            if(item.name == "xxx5"
            || item.name == "kawalek_kodu")
            {reka_gracza.localRotation = Quaternion.Euler(90f, 0f, 180f);}
        }

        if(Input.GetKeyDown(KeyCode.Q) && trzyma_przedmiot == true)
        {
            for (int i = 0; i < reka_gracza.childCount; i++)
            {   
                var wyrzuc_przedmiot = reka_gracza.GetChild(i);
                if(wyrzuc_przedmiot.tag == "Item" 
                    && wyrzuc_przedmiot.GetComponent<Rigidbody>() != null
                    && wyrzuc_przedmiot.GetComponent<Outline>() != null
                    )
                {   
                    wyrzuc_przedmiot.GetComponent<Outline>().enabled = false;
                    wyrzuc_przedmiot.SetParent(null);
                    wyrzuc_przedmiot.GetComponent<Rigidbody>().isKinematic = false;

                    reka_gracza.localPosition = new Vector3(0.474f, -0.418f, 0.964f); //podstawowa wartosc
                    reka_gracza.localRotation = Quaternion.Euler(0f, 0f, 0f); //podstawowa wartosc
                    trzyma_przedmiot = false;
                }
            }
        }

        for (int i = 0; i < przedmioty_lista.Count; i++)
        {
            var obiekt = przedmioty_lista[i];
            if(obiekt.transform.GetComponent<Outline>() != null && Physics.Raycast(ray, out hit) && obiekt != hit.transform.gameObject)
            {
                obiekt.transform.GetComponent<Outline>().enabled = false;           
                przedmioty_lista.Remove(obiekt);
            }
        }
    }
}
