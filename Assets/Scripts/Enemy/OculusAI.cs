using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class OculusAI : MonoBehaviour
{
    
    [SerializeField] public EnemyHPManager hPManager;
    [SerializeField] private Animator anim;
    [SerializeField] public Character dusty;
    [SerializeField] Collider[] sight;
    private bool noticed;
    private NavMeshAgent agent;


    [Header("Combat")]
    private int HP;
    [SerializeField] private float SPD;
    [SerializeField] private Transform atkPoint;
    [SerializeField] private float atRadius;
    [SerializeField] private LayerMask isPlayer;
    // Start is called before the first frame update

    [Header("Reward")]
    [SerializeField] private GameObject rKey;

    void Awake()
    {
        HP = hPManager.HP;
        noticed = false;
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            noticed = true;
        }
    }
    void PlayerNoticed()
    {
        if (noticed)
        {
            agent.SetDestination(dusty.transform.position);
            anim.SetBool("Walk", true);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                anim.SetBool("Walk", false);
                anim.SetTrigger("Atk");
            }
        }
    }
    private void Atk()
    {
        agent.isStopped = true;
        Collider[] collsTocados = Physics.OverlapSphere(atkPoint.position, atRadius, isPlayer);
        SoundManager.PlaySound(SoundType.ATTACK);

        if (collsTocados.Length > 0)
        {
            collsTocados[0].GetComponent<Character>().Ouch();
            Debug.Log("Boyoiyoiyoing");
        }
    }
    private void AtkEnd()
    {
        //Si el player se ha salido de rango
        //Dejar de estar quieto
        //Poner attacking a false
        if (agent.remainingDistance >= agent.stoppingDistance)
        {
            agent.isStopped = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(atkPoint.position, atRadius);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerNoticed();
        Oof();
        HP = hPManager.HP;
    }

    public void Oof()
    {
        if (HP <= 0)
        {
            Instantiate(rKey,atkPoint.position, atkPoint.rotation);
            anim.SetTrigger("Ouch");
            SoundManager.PlaySound(SoundType.DEAD);

            agent.isStopped = true;
            GetComponent<OculusAI>().enabled = false;
        } 
    }
}
