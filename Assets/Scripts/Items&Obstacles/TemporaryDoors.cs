using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryDoors : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject door;
    [SerializeField] private int keysNeeded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.GetComponent<Character>().basicKeys >= keysNeeded)
        {
            wall.SetActive(false);
            door.SetActive(false);
            SoundManager.PlaySound(SoundType.DOOR);
            anim.SetTrigger("Open");
            
        }
    }
}
