using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hpIcon1, hpIcon2, hpIcon3, hue, hpBar3, hpBar2, hpBar1,DIcon1, DIcon2, DIcon3, DIcon4;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private WeaponsSO[] weaponSO;
    [SerializeField] private BlockedPassage[] walls;
    [SerializeField] public int currentW;
    [SerializeField] private TMP_Text glassCostText;
    [SerializeField] private TMP_Text glassLvText;
    [SerializeField] private TMP_Text metalCostText;
    [SerializeField] private TMP_Text metalLvText;
    [SerializeField] private TMP_Text woodCostText;
    [SerializeField] private TMP_Text woodLvText;
    [HideInInspector] private int glassLv;
    [HideInInspector] private int glassCost, baseGlassCost;
    [HideInInspector] private int metalLv;
    [HideInInspector] private int metalCost, baseMetalCost;
    [HideInInspector] private int woodLv;
    [HideInInspector] private int woodCost, baseWoodCost;
    [SerializeField] private TMP_Text hpCostText;
    [SerializeField] private TMP_Text hpLvText;
    [HideInInspector] private int hpLv;
    [HideInInspector] private int hpCost, baseHpCost;
    public int totalKey;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject opcionMenu;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject menu;
    [SerializeField] private TMP_Text moni;
    [SerializeField] private TMP_Text keyR;
    [SerializeField] private TMP_Text keyO;
    [SerializeField] private TMP_Text keyY;
    [SerializeField] private TMP_Text keyP;
    [HideInInspector] private bool menuOn;
    [HideInInspector] private bool upgradeOn;
    public Character character;
    [SerializeField] private int currentHp;
    [SerializeField] private int reorder;
    // Start is called before the first frame update
    
    void Awake()
    {
        currentW = 0;
        hpLv = 1;
        baseHpCost = 75;
        glassLv = 1;
        baseGlassCost = 50;
        metalLv = 1;
        baseMetalCost = 50;
        woodLv = 1;
        baseWoodCost = 50;
        hpCost = baseHpCost * hpLv;
        glassCost = baseGlassCost * glassLv;
        metalCost = baseMetalCost * metalLv;
        woodCost = baseWoodCost * woodLv;
        upgradeMenu.SetActive(false);
        deathMenu.SetActive(false);
        opcionMenu.SetActive(false);
        controlMenu.SetActive(false);
        menuOn = false;
        menu.SetActive(false);
        hud.SetActive(true);
        hue.SetActive(false);
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        weapons[3].SetActive(false);
        hpBar3.SetActive(true);
        hpBar2.SetActive(false);
        hpBar1.SetActive(false);
        DIcon1.SetActive(true);
        DIcon2.SetActive(false);
        DIcon3.SetActive(false);
        DIcon4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        character.GetComponent<Character>().weaponId = currentW;
        currentHp = character.GetComponent<Character>().HP;
        Menu();
        InventarioUpdate();
        UpgradeUpdate();
        UpgradeMenu();
        HPIcons();
        Weapon();
        WeaponBreak();
        Dead();
    }
    public void OnClickQuitButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickOptionsButton()
    {
        opcionMenu.SetActive(true);
        menu.SetActive(false);
    }
    public void OnClickControlsButton()
    {
        controlMenu.SetActive(true);
        menu.SetActive(false);
    }
    public void OnClickSalirButton()
    {
        opcionMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void OnClickSalir2Button()
    {
        controlMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void OnClickHpUpButton()
    {
        if (hpLv < 3 && character.mony >= hpCost)
        {
            character.mony -= hpCost;
            character.HP++;
            character.maxHP++;
            hpLv++;
            hpCost = hpCost * hpLv;
            currentHp = character.GetComponent<Character>().HP;
            hpCost = baseHpCost * hpLv;
        }
    }
    public void OnClickGlassDMGUpButton()
    {
        if (glassLv < 4 && character.mony >= glassCost)
        {
            character.mony -= glassCost;
            character.glassBonus += 2;
            glassLv++;
            glassCost = glassCost * glassLv;
            glassCost = baseGlassCost * glassLv;

        }
    }
    public void OnClickMetalDurabilityUpButton()
    {
        if (metalLv < 4 && character.mony >= metalCost)
        {
            character.mony -= metalCost;
            character.metalBonus += 2;
            metalLv++;
            metalCost = metalCost * metalLv;
            metalCost = baseMetalCost * metalLv;
        }
    }
    public void OnClickWoodStatsUpButton()
    {
        if (woodLv < 4 && character.mony >= woodCost)
        {
            character.mony -= woodCost;
            character.woodBonus++;
            woodLv++;
            woodCost = woodCost * woodLv;
            woodCost = baseWoodCost * woodLv;
        }
    }
    private void Menu()
    {
        if(Input.GetKeyDown(KeyCode.I) && menuOn == false && upgradeOn == false)
        {
            menu.SetActive(true);
            hud.SetActive(false);
            SoundManager.PlaySound(SoundType.PAUSE);
            menuOn = true;
            hue.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.I) && menuOn == true && upgradeOn == false)
        {
            menu.SetActive(false);
            hud.SetActive(true);
            SoundManager.PlaySound(SoundType.PAUSE);
            menuOn = false;
            hue.SetActive(false);
        }
    }
    private void UpgradeMenu()
    {
        if (Input.GetKeyDown(KeyCode.U) && upgradeOn == false && menuOn == false)
        {
            upgradeMenu.SetActive(true);
            hud.SetActive(false);
            SoundManager.PlaySound(SoundType.PAUSE);
            upgradeOn = true;
            hue.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U) && upgradeOn == true && menuOn == false)
        {
            upgradeMenu.SetActive(false);
            hud.SetActive(true);
            SoundManager.PlaySound(SoundType.PAUSE);
            upgradeOn = false;
            hue.SetActive(false);
        }
    }
    private void InventarioUpdate()
    {
        moni.text = "Polvo: " + character.mony;
        keyR.text = "Red Key: " + character.rKey;
        keyO.text = "Orange Key: " + character.oKey;
        keyY.text = "Yellow Key: " + character.yKey;
        keyP.text = "Purple Key: " + character.pKey;
        totalKey = character.oKey + character.yKey + character.pKey + character.rKey;
    }
    private void UpgradeUpdate()
    {
        hpLvText.text = "HP Up Lv. " + hpLv;
        hpCostText.text = "HP +1 - Cost: " + hpCost;
        if (hpLv >= 3)
        {
            hpCostText.text = "Maxed!";
        }
        glassLvText.text = "Glass DMG Up Lv. " + glassLv;
        glassCostText.text = "Glass DMG +2 - Cost: " + glassCost;
        if (glassLv >= 4)
        {
            glassCostText.text = "Maxed!";
        }
        metalLvText.text = "Metal Durability Up Lv. " + metalLv;
        metalCostText.text = "Metal Durability +2 - Cost: " + metalCost;
        if (metalLv >= 4)
        {
            metalCostText.text = "Maxed!";
        }
        woodLvText.text = "Wood Stats Up Lv. " + woodLv;
        woodCostText.text = "Wood DMG & Durability +1 - Cost: " + woodCost;
        if (woodLv >= 4)
        {
            woodCostText.text = "Maxed!";
        }
    }
    public void Win()
    {
        SceneManager.LoadScene(2);
    }
    private void HPIcons()
    {

        if (currentHp == 3)
        {
            hpIcon1.SetActive(true);
            hpIcon2.SetActive(true);
            hpIcon3.SetActive(true);
        }
        else if (currentHp == 2)
        {
            hpIcon1.SetActive(true);
            hpIcon2.SetActive(true);
            hpIcon3.SetActive(false);
        }
        else if (currentHp == 1)
        {
            hpIcon1.SetActive(true);
            hpIcon2.SetActive(false);
            hpIcon3.SetActive(false);
        }
        else
        {
            hpIcon1.SetActive(false);
            hpIcon2.SetActive(false);
            hpIcon3.SetActive(false);
        }

        if (currentHp == character.maxHP)
        {
            DIcon1.SetActive(true);
            DIcon2.SetActive(false);
            DIcon3.SetActive(false);
            DIcon4.SetActive(false);
            hpBar3.SetActive(true);
            hpBar2.SetActive(false);
            hpBar1.SetActive(false);
        }
        else if (currentHp == 2)
        {
            DIcon1.SetActive(false);
            DIcon2.SetActive(true);
            DIcon3.SetActive(false);
            DIcon4.SetActive(false);
            hpBar3.SetActive(false);
            hpBar2.SetActive(true);
            hpBar1.SetActive(false);
        }
        else if (currentHp == 1)
        {
            DIcon1.SetActive(false);
            DIcon2.SetActive(false);
            DIcon3.SetActive(true);
            DIcon4.SetActive(false);
            hpBar3.SetActive(false);
            hpBar2.SetActive(false);
            hpBar1.SetActive(true);
        }
        else
        {
            DIcon1.SetActive(false);
            DIcon2.SetActive(false);
            DIcon3.SetActive(false);
            DIcon4.SetActive(true);
            hpBar3.SetActive(false);
            hpBar2.SetActive(false);
            hpBar1.SetActive(true);
        }

    }
    private void Weapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && currentW != 0)
        {
            weapons[currentW].SetActive(false);
            currentW = 0;
            weaponSO[currentW].durability = weaponSO[currentW].maxDurability;
            weapons[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentW != 1)
        {
            weapons[currentW].SetActive(false);
            currentW = 1;
            weaponSO[currentW].durability = weaponSO[currentW].maxDurability;
            weapons[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentW != 2)
        {
            weapons[currentW].SetActive(false);
            currentW = 2;
            weaponSO[currentW].durability = weaponSO[currentW].maxDurability + character.metalBonus;
            weapons[2].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentW != 3)
        {
            weapons[currentW].SetActive(false);
            currentW = 3;
            weaponSO[currentW].durability = weaponSO[currentW].maxDurability + character.woodBonus;
            weapons[3].SetActive(true);
        }
    }
    private void WeaponBreak()
    {
        if (weaponSO[currentW].durability <= 0)
        {
            weapons[currentW].SetActive(false);
            currentW = 0;
            weapons[0].SetActive(true);
        }
    }
    public void Reorder()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].order--;
        }
    }
    private void Dead()
    {
        if (currentHp == 0)
        {
            deathMenu.SetActive(true);
            SoundManager.PlaySound(SoundType.GAMEOVER);
            hue.SetActive(true);    
        }
    }
    
}
