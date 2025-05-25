using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKeys : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Character>().basicKeys++;
            SoundManager.PlaySound(SoundType.KEY);

            Destroy(gameObject);
        }
    }
}
