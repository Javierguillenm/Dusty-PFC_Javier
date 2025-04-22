using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Raton : MonoBehaviour
{
    [SerializeField] private GameObject itemSpawn;
    [SerializeField] private int enemyId;
    [SerializeField] private float timer;
    [SerializeField] private float Wtimer;
    [SerializeField] private float radius;
    [SerializeField] private GameObject rKey, oKey, yKey, pKey, sDust, mDust, lDust;
    GameObject player;
    public Transform playerPOS;
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Collider[] sight;
    [SerializeField] private float minDistance;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        radius = 10;
        Wtimer = 6;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.SetDestination(playerPOS.position);
        }
        else
        {
            if (timer >= Wtimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, radius, -1);
                agent.SetDestination(newPos);
                timer = 0;
                float wander = Random.Range(2, 7);
                Wtimer = wander;
            }
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;



    }
    public void Ouch(int DMG)
    {
        
    }
   
}
