using SmallHedge.SoundManager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    private int numBullet;
    [SerializeField] private bool inmunity = false;
    [SerializeField] private float inmunityF;
    [SerializeField] public int mony = 0;
    [SerializeField] public bool horquilla;
    [SerializeField] public int HP = 1;
    public int basicKeys;
    [SerializeField] public float range;
    [SerializeField] public int maxHP = 1;
    private float inputH;
    private float inputV;
    [SerializeField] private Transform atkP;
    [SerializeField] private LayerMask esSaltable;
    [SerializeField] private float jumpHeight = 8;
    [SerializeField] private int SPD = 5;
    [SerializeField] private float factorGravedad;
    [SerializeField] private int numSalto = 1;
    [SerializeField] private int saltoMax = 1;
    [SerializeField] private bool dobleSalto = false;
    [SerializeField] private bool wallJump;
    [SerializeField] private float wallJumpF;
    [SerializeField] private LayerMask whatIsEnemy;
    public CharacterController controller;
    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    private Vector3 posInicial;
    private Vector3 movimientoY;
    [SerializeField] private WeaponsSO[] weapon;
    [SerializeField] public int weaponId;
    [SerializeField] private SoundsSO[] sounds;
    private Vector2 moveInput;
    private Vector2 lookInput;
    public int rKey;
    public int oKey;
    public int yKey;
    public int pKey;
    public int glassBonus;
    public int metalBonus;
    public int woodBonus;

    // Start is called before the first frame update
    void Awake()
    {
        wallJump = true;
        posInicial = transform.position;
        //controller= GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("Run", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * SPD * Time.deltaTime);
        }
        else if (direction.magnitude == 0f)
        {
            anim.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && numSalto > 0)
        {
            anim.SetTrigger("Jump");
            SoundManager.PlaySound(SoundType.JUMP);
            movimientoY.y = Mathf.Sqrt(-2 * Physics.gravity.y * factorGravedad * jumpHeight);
            numSalto--;
            //rb.AddForce(new Vector3(0, 1, 0).normalized * jumpHeight, ForceMode.Impulse);
        }

        AplicarGravedad();
    }
    private void FixedUpdate()
    {
        if (inmunity)
        {
            inmunityF += Time.deltaTime;
            if (inmunityF >= 3)
            {
                inmunity = false;
                inmunityF = 0;
            }
        }
        if (wallJump == false)
        {
            wallJumpF += Time.deltaTime;
            if (wallJumpF >= 0.3f)
            {
                wallJump = true;
                wallJumpF = 0;
            }
        }
        if (weaponId != 0)
        {
            anim.SetBool("Weapon", true);
        }
        else
        {
            anim.SetBool("Weapon", false);
        }
        Suelo();
        MejoraDobleSalto();
        Correr();
        Ataque();
    }

    private void AplicarGravedad()
    {
        movimientoY.y += factorGravedad * Physics.gravity.y * Time.deltaTime;
        controller.Move(movimientoY * Time.deltaTime);
    }
    void Suelo()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f, esSaltable))
        {
            anim.SetBool("Fall", false);
            if (movimientoY.y < 0)
            {
                numSalto = saltoMax;
            }
        }
        else
        {
            anim.SetBool("Fall", true);
        }

        if (Physics.Raycast(transform.position, Vector3.forward, 0.5f, esSaltable) && wallJump)
        {
            anim.SetBool("WallJ", true);
            anim.SetBool("Fall", false);
            numSalto = saltoMax;
            wallJump = false;
        }
        else
        {
            anim.SetBool("WallJ", false);
        }

    }
    void MejoraDobleSalto()
    {
        if (dobleSalto)
        {
            saltoMax = 2;
        }
    }
    void Correr()
    {
        if (Input.GetKey(KeyCode.X))
        {
            SPD = 6;
        }
        else
        {
            SPD = 3;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collect"))
        {
            if (HP < maxHP)
            {
                HP++;
            }
            Polvo collectScript = other.gameObject.GetComponent<Polvo>();
            mony += collectScript.value;
            SoundManager.PlaySound(SoundType.DUST);

            Destroy(other.gameObject);
            Debug.Log(mony);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            SoundManager.PlaySound(SoundType.KEY);
            int key = other.gameObject.GetComponent<Keys>().keyId;
            Destroy(other.gameObject);
            Debug.Log("I have collected " + other.name);
            if (key == 0)
            {
                rKey = 1;
            }
            else if (key == 1)
            {
                oKey = 1;
            }
            else if (key == 2)
            {
                yKey = 1;
            }
            else
            {
                pKey = 1;
            }
        }
    }
    public void Ouch()
    {
        if (inmunity == false)
        {
            HP--;
            Debug.Log("Ouch");
            if (HP == 0)
            {
                anim.enabled = false;
                controller.enabled = false;
                GetComponent<Character>().enabled = false;
                player.SetActive(false);
            }

        }
    }
    void Ataque()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (weaponId != 0)
            {
                anim.SetTrigger("Attack");
                SoundManager.PlaySound(SoundType.ATTACKWOOD);

                Debug.Log("Boing");
                Collider[] collsTocados = Physics.OverlapSphere(atkP.position, range, whatIsEnemy);
                for (int i = 0; i < collsTocados.Length; i++)
                {
                    if (weaponId == 1)
                    {
                        collsTocados[i].GetComponent<EnemyHPManager>().Ouch(weapon[weaponId].DMG + glassBonus);
                        Debug.Log("Boyoiyoiyoing");
                    }
                    else if (weaponId == 3)
                    {
                        collsTocados[i].GetComponent<EnemyHPManager>().Ouch(weapon[weaponId].DMG + woodBonus);
                        Debug.Log("Boyoiyoiyoing");
                    }
                    else
                    {
                        collsTocados[i].GetComponent<EnemyHPManager>().Ouch(weapon[weaponId].DMG);
                        Debug.Log("Boyoiyoiyoing");
                    }
                    if (weaponId != 0)
                    {
                        weapon[weaponId].durability--;
                    }
                }
                Debug.Log(weapon[weaponId].durability);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atkP.position, range);
    }
  
}
