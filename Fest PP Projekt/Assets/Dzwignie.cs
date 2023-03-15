using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dzwignie : MonoBehaviour
{
    public Camera kamera_gracza;
    public float odleglosc;

    public Transform Dz1;
    public Transform Dz2;
    public Transform Dz3;
    public Transform Dz4;

    //Stan, false = wylaczona, true = wlaczona
    private bool stan_dz1 = false;
    private bool stan_dz2 = false;
    private bool stan_dz3 = false;
    private bool stan_dz4 = false;

    private float rotacja_wylaczona = -52f;
    private float rotacja_wlaczona = 38f;

    public Material kolor_led;
    private Color poczatkowy_kolor = new Color(255f/255f, 0f/255f, 0f/255f);

    public bool dozwolona_interakcja = true;
    public Transform drzwi_sejfiku;
    public AudioSource dzwiek;
    public AudioSource wygrana;

    void Start()
    {   
        DOTween.Init();
        kolor_led.DOColor(poczatkowy_kolor, 0f);
    }



    private void wlacz_dzwignie(Transform jakas_dzwignia)
    {
        DOTween.Init();
        jakas_dzwignia.DOLocalRotate(new Vector3(0f, 0f, rotacja_wlaczona), 0.5f);
    }
    private void wylacz_dzwignie(Transform jakas_dzwignia)
    {
        DOTween.Init();
        jakas_dzwignia.DOLocalRotate(new Vector3(0f, 0f, rotacja_wylaczona), 0.5f);
    }



    private void update_rotacja()
    {   
        //dzwignie po zanegowaniu stanu
        if(stan_dz1 == false)
        {   
            wylacz_dzwignie(Dz1);
            if(stan_dz3 == false){wylacz_dzwignie(Dz3);}
            else{wlacz_dzwignie(Dz3);}
        }
        else
        {   
            wlacz_dzwignie(Dz1);
            if(stan_dz3 == false){wylacz_dzwignie(Dz3);}
            else{wlacz_dzwignie(Dz3);}
        }



        if(stan_dz2 == false)
        {   
            wylacz_dzwignie(Dz2);
            if(stan_dz1 == false){wylacz_dzwignie(Dz1);}
            else{wlacz_dzwignie(Dz1);}
            if(stan_dz4 == false){wylacz_dzwignie(Dz4);}
            else{wlacz_dzwignie(Dz4);}
        }
        else
        {   
            wlacz_dzwignie(Dz2);
            if(stan_dz1 == false){wylacz_dzwignie(Dz1);}
            else{wlacz_dzwignie(Dz1);}
            if(stan_dz4 == false){wylacz_dzwignie(Dz4);}
            else{wlacz_dzwignie(Dz4);}
        }



        if(stan_dz3 == false)
        {   
            wylacz_dzwignie(Dz3);
        }
        else
        {   
            wlacz_dzwignie(Dz3);
        }



        if(stan_dz4 == false)
        {   
            wylacz_dzwignie(Dz4);
            if(stan_dz1 == false){wylacz_dzwignie(Dz1);}
            else{wlacz_dzwignie(Dz1);}
            if(stan_dz2 == false){wylacz_dzwignie(Dz2);}
            else{wlacz_dzwignie(Dz2);}
            if(stan_dz3 == false){wylacz_dzwignie(Dz3);}
            else{wlacz_dzwignie(Dz3);}
        }
        else
        {   
            wlacz_dzwignie(Dz4);
            if(stan_dz1 == false){wylacz_dzwignie(Dz1);}
            else{wlacz_dzwignie(Dz1);}
            if(stan_dz2 == false){wylacz_dzwignie(Dz2);}
            else{wlacz_dzwignie(Dz2);}
            if(stan_dz3 == false){wylacz_dzwignie(Dz3);}
            else{wlacz_dzwignie(Dz3);}
        }
    }



    void Update()
    {
        var ray = kamera_gracza.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out hit)
            && hit.transform.tag == "Dzwignia"
            && hit.distance <= odleglosc
            && dozwolona_interakcja == true)
        {   
            var obiekt = hit.transform;
            
            if(obiekt.name == Dz1.name){stan_dz1 = !stan_dz1; stan_dz3 = !stan_dz3;}
            if(obiekt.name == Dz2.name){stan_dz2 = !stan_dz2; stan_dz1 = !stan_dz1; stan_dz4 = !stan_dz4;}
            if(obiekt.name == Dz3.name){stan_dz3 = !stan_dz3;}
            if(obiekt.name == Dz4.name){stan_dz4 = !stan_dz4; stan_dz1 = !stan_dz1; stan_dz2 = !stan_dz2; stan_dz3 = !stan_dz3;}

            dzwiek.Play();

            if(stan_dz1 == true
            && stan_dz2 == false
            && stan_dz3 == false
            && stan_dz4 == false)
            {   
                dozwolona_interakcja = false;
                wygrana.Play();
                DOTween.Init();
                kolor_led.DOColor(Color.green, 0.5f);
                DOTween.Init();
                drzwi_sejfiku.DOLocalMoveX(5, 10f);
            }
            update_rotacja();
        }
    }
}
