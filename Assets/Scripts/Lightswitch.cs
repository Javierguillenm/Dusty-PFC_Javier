using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public GameObject on08;
    public GameObject off08;
    public GameObject lightsText;
    public GameObject lights08;
    public bool Lightsareon;
    public bool Lightsareoff;
    public bool inReach;
    public AudioSource switchClick;
    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        Lightsareon = false;
        Lightsareoff = true;
        on08.SetActive(false);
        off08.SetActive(true);
        lights08.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            lightsText.SetActive(true );
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach = false;
            lightsText.SetActive(false );
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Lightsareon && inReach && Input.GetButtonDown("Interact"))
        {
            lights08.SetActive(true);
            on08.SetActive(false);
            off08.SetActive(true);
            switchClick.Play();
            Lightsareoff = true;
            Lightsareon = false ;
        }
        else if (Lightsareoff && inReach && Input.GetButtonDown("Interact"))
        {
            lights08.SetActive(true);
            on08.SetActive(true);
            on08.SetActive(false);
            switchClick.Play();
            Lightsareoff = false;
            Lightsareon = true;
        } 
    }
}
