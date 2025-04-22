using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horquilla : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.GetComponent<Character>().horquilla == false)
        {
            player.GetComponent<Character>().horquilla = true;
            Destroy(gameObject);
        }
    }
}
