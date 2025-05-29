using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxillaAI : MonoBehaviour
{
    [SerializeField] public EnemyHPManager hPManager;
    [SerializeField] private Animator anim;
    [SerializeField] public Character dusty;
    [SerializeField] Collider[] sight;
    [SerializeField] private bool atkReady;
    private bool noticed;
    private UnityEngine.AI.NavMeshAgent agent;


    [Header("Combat")]
    private int HP;
    [SerializeField] private float atkCooldown;
    [SerializeField] private float SPD;
    [SerializeField] private Transform atkPoint;
    [SerializeField] private float atRadius;
    [SerializeField] private LayerMask isPlayer;

    [Header("Reward")]
    [SerializeField] private GameObject oKey;
    // Start is called before the first frame update

    void Awake()
    {
        HP = hPManager.HP;
        noticed = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
     //   SoundManager.PlaySound(SoundType.MAXILLASEESPLAYER);

        if (noticed)
        {
            agent.SetDestination(dusty.transform.position);
            anim.SetBool("Walk", true);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && atkReady)
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
    //    SoundManager.PlaySound(SoundType.MAXILLASOUND);
        Collider[] collsTocados = Physics.OverlapSphere(atkPoint.position, atRadius, isPlayer);
        if (collsTocados.Length > 0)
        {
            collsTocados[0].GetComponent<Character>().Ouch();
            Debug.Log("Boyoiyoiyoing");
        }
    }
    private void AtkEnd()
    {
        atkReady = false;
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
        ATKReady();
        HP = hPManager.HP;
    }

    public void Oof()
    {
        if (HP <= 0)
        {
            Instantiate(oKey, atkPoint.position, atkPoint.rotation);
            anim.SetTrigger("Ouch");
            agent.isStopped = true;
            GetComponent<MaxillaAI>().enabled = false;
        }
    }
    void ATKReady()
    {
        if (atkReady == false)
        {
            atkCooldown += Time.deltaTime;

            if (atkCooldown >= 3f)
            {
                atkReady = true;
                atkCooldown = 0;
            }
        }

    }
}
