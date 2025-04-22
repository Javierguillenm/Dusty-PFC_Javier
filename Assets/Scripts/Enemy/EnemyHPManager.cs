using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : MonoBehaviour
{
    [SerializeField] public int HP;
    // Start is called before the first frame update
    public void Ouch(int DMG)
    {
        HP -= DMG;
    }
}
