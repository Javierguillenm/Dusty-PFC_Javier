using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locks : MonoBehaviour
{
    [SerializeField] private GameObject lock1;
    [SerializeField] private GameObject lock2;
    [SerializeField] private GameObject lock3;
    [SerializeField] private GameManager gm;
    private int collectedKeys;
    private bool win;

    // Start is called before the first frame update
    void Awake()
    {
        win = false;
        collectedKeys = gm.GetComponent<GameManager>().totalKey;
    }

    // Update is called once per frame
    void Update()
    {
        collectedKeys = gm.totalKey;

        if (collectedKeys == 1)
        {
            lock1.SetActive(false); 
        }
        if (collectedKeys == 2)
        {
            lock2.SetActive(false);
        }
        if (collectedKeys == 3)
        {
            lock3.SetActive(false);
            win = true; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && win)
        {
            gm.GetComponent<GameManager>().Win();
        }
    }
}
