using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedPassage : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] public int order;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && player.GetComponent<Character>().horquilla && order == 0)
        {
            Destroy(gameObject);
            player.GetComponent<Character>().horquilla = false;
            gm.Reorder();
        }
    }
    
}
