using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class HumanFight : MonoBehaviour
{
    public bool PlayerStep = true;
    public bool EnemyStep = false;
    public bool CriticalBoolPl = false;
    public bool CriticalBoolEn = false;

    GameObject Player;
    GameObject Enemy;
    GameObject PlayerStats;
    Animator AnimatorPlayer;
    Animator AnimatorEnemy;


    public SpawnItem spawnItem;
    public InventoryManager inventoryManager;

    private Vector3 PlPos;
    private Vector3 EnPos;
    public Vector3 EnFightPos;

    public float speed;
    public float progressMove;
    public float progressMovePreFight;

    public int IsMove = 0;

    public TextMeshProUGUI eventText;

    private int EnergyForAttack;

    public int EnemyNum = 1;

    [SerializeField] public UIController UiController;

    public void Start()
    {
        PlayerStats = GameObject.FindGameObjectWithTag("GameController");
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = GameObject.FindGameObjectWithTag("Enemy");

        AnimatorPlayer = Player.GetComponent<Animator>();
        AnimatorEnemy = Enemy.GetComponent<Animator>();

        IsAttacked IsAttackedPL = Player.GetComponent<IsAttacked>();
        if (Enemy != null)
        {
            IsAttacked IsAttackedEn = Enemy.GetComponent<IsAttacked>();
        }       
    }


    public void PlayerAttack()
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        EnemyData EnemyData = Enemy.GetComponent<EnemyData>();
        PlayerStep = true;
        EnemyStep = false;

        if (PlayerStep == true && PlayerData.energy >= PlayerData.EnergyForAttack && IsMove != 4) 
        {
            CriticalBoolPl = false;
            CriticalBoolEn= false;
            PlPos = Player.transform.position;
            EnPos = Enemy.transform.position;
            IsMove = 1;

            PlayerData.energy -= PlayerData.EnergyForAttack;

            if (PlayerData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolPl = true; }
            if (EnemyData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
        }
        else if (PlayerStep == true && PlayerData.energy < PlayerData.EnergyForAttack && IsMove != 4)
        {
            eventText.text += "<#fcf403>Not enough energy!</color>" + '\n';
            CriticalBoolEn = false;
            PlayerStep = false;
            EnemyStep = true;
            IsMove = 1;

            PlPos = Player.transform.position;
            EnPos = Enemy.transform.position;

            UiController.AttackInteractableFalse();

            if (EnemyData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
        }
    }


    public void Attacks()
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        EnemyData EnemyData = Enemy.GetComponent<EnemyData>();
        IsAttacked IsAttackedPL = Player.GetComponent<IsAttacked>();
        IsAttacked IsAttackedEn = Enemy.GetComponent<IsAttacked>();

        if (PlayerStep == true)
        {
            if (CriticalBoolPl == true)
            {
                AnimatorPlayer.SetBool("CriticalAttack", true);
                if (IsAttackedPL.AttackCheker == 1)
                {
                    EnemyData.health -= (PlayerData.damage * PlayerData.CriticalMultiplier - EnemyData.armor);
                    if (PlayerData.damage * PlayerData.CriticalMultiplier - EnemyData.armor < 0)
                    {
                        eventText.text += $"<#fcf403>You dealt critical damage: 0 </color> \n";
                    }
                    else
                    {
                        if (PlayerData.DamageScore == true)
                        {
                            PlayerData.DamageGiven += PlayerData.damage * PlayerData.CriticalMultiplier - EnemyData.armor;
                        }
                        eventText.text += $"<#fcf403>You dealt critical damage: {PlayerData.damage * PlayerData.CriticalMultiplier - EnemyData.armor} </color> \n";
                    }

                    AnimatorPlayer.SetBool("CriticalAttack", false);
                    IsMove = 2;
                    IsAttackedPL.AttackReset();
                }
            }

            if (CriticalBoolPl == false)
            {
                AnimatorPlayer.SetBool("Attack", true);
                if (IsAttackedPL.AttackCheker == 1)
                {
                    EnemyData.health -= (PlayerData.damage - EnemyData.armor);
                    if (PlayerData.damage - EnemyData.armor < 0)
                    {
                        eventText.text += $"<#fcf403>You dealt damage: 0 </color> \n";
                    }
                    else
                    {
                        if (PlayerData.DamageScore == true)
                        {
                            PlayerData.DamageGiven += PlayerData.damage - EnemyData.armor;
                        }
                        eventText.text += $"<#fcf403>You dealt damage: {PlayerData.damage - EnemyData.armor} </color> \n";
                    }

                    AnimatorPlayer.SetBool("Attack", false);
                    IsMove = 2;
                    IsAttackedPL.AttackReset();
                }
            }
        }

        if (EnemyStep == true)
        {
            if (CriticalBoolEn == true)
            {
                AnimatorEnemy.SetInteger("CriticalAttack", 2);
                if (IsAttackedEn.AttackCheker == 1)
                {
                    PlayerData.health -= (EnemyData.damage * EnemyData.CriticalMultiplier - PlayerData.armor);
                    if (EnemyData.damage * EnemyData.CriticalMultiplier - PlayerData.armor < 0)
                    {
                        eventText.text += $"<#fcf403>You took critical damage: 0 </color> \n";
                    }
                    else { eventText.text += $"<#fcf403>You took critical damage: {EnemyData.damage * EnemyData.CriticalMultiplier - PlayerData.armor} </color> \n"; }
                    AnimatorEnemy.SetInteger("CriticalAttack", 1);

                    IsMove = 2;
                    IsAttackedEn.AttackReset();
                }
            }
            if (CriticalBoolEn == false)
            {
                AnimatorEnemy.SetInteger("Attack", 2);

                if (IsAttackedEn.AttackCheker == 1)
                {
                    PlayerData.health -= (EnemyData.damage - PlayerData.armor);
                    if (EnemyData.damage - PlayerData.armor < 0) { eventText.text += $"<#fcf403>You took damage: 0 </color> \n"; }
                    else { eventText.text += $"<#fcf403>You took damage: {EnemyData.damage - PlayerData.armor} </color> \n"; }
                    AnimatorEnemy.SetInteger("Attack", 1);

                    IsMove = 2;
                    IsAttackedEn.AttackReset();
                }
            }
        }
    }


    public void Die() 
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        EnemyData EnemyData = Enemy.GetComponent<EnemyData>();

        if (EnemyData.health < 1 && EnemyNum == 1) 
        { 
            AnimatorEnemy.SetBool("Die", true); 
            IsMove = 4; 
            UiController.AfterFightUI(); 
            PlayerData.experience += EnemyData.level;
            eventText.text += $"<#fcf403>Enemy killed, you gain {EnemyData.level} experience </color> \n";

            PlayerData.PiratesKilled += 1;
            spawnItem.SpawnLoot();
        }

        if (EnemyData.health < 1 && EnemyNum == 2)
        {
            AnimatorEnemy.SetBool("Die", true);
            PlayerData.experience += EnemyData.level;
            eventText.text += $"<#fcf403>Enemy killed, you gain {EnemyData.level} experience </color> \n";
            UiController.AttackInteractableFalse();
            StartCoroutine(FadeOutAndDestroy(Enemy, 2f));

            PlayerData.PiratesKilled += 1;
            ResetData();
        }

        if (PlayerData.health < 1 && IsMove == 4) 
        { 
            AnimatorPlayer.SetBool("Die", true);
            UiController.AttackInteractableFalse();

            //Invoke("PlayerDie()", 2f);
        }
    }
    

    void FixedUpdate()
    {
        try
        {
            EnemyData EnemyData = Enemy.GetComponent<EnemyData>();
            if (EnemyData != null)
            {

                GameObject FP = GameObject.Find("FightPosition");
                if (FP != null)

                {
                    EnFightPos = FP.transform.position;
                }

                if (IsMove == 0 && EnemyData.MovingFromBack == true)
                {
                    Enemy.transform.position = Vector3.Lerp(EnPos, EnFightPos, progressMovePreFight);
                    if (progressMovePreFight <= 1)
                    {
                        UiController.AttackInteractableFalse();
                        AnimatorEnemy.SetBool("Walk", true);
                        progressMovePreFight += speed;
                    }
                    else if (progressMovePreFight >= 1)
                    {
                        EnPos = Enemy.transform.position;
                        AnimatorEnemy.SetBool("Walk", false);
                        UiController.AttackInteractableTrue();
                    }
                }
                if (PlayerStep == true && IsMove != 4)
                {
                    if (IsMove == 1)
                    {
                        UiController.AttackInteractableFalse();
                        AnimatorPlayer.SetBool("Walk", true);
                        Player.transform.position = Vector3.Lerp(PlPos, EnPos, progressMove);
                        if (progressMove <= 1)
                        {
                            progressMove += speed;
                        }
                    }
                    if (progressMove >= 1 && IsMove == 1)
                    {
                        AnimatorPlayer.SetBool("Walk", false);
                        Attacks();
                    }
                    if (IsMove == 2)
                    {
                        AnimatorPlayer.SetBool("Walk", true);
                        Player.transform.position = Vector3.Lerp(PlPos, EnPos, progressMove);
                        progressMove -= speed;
                    }
                    if (progressMove <= 0 && IsMove == 2) { AnimatorPlayer.SetBool("Walk", false); PlayerStep = false; EnemyStep = true; CriticalBoolPl = false; IsMove = 1; Die(); }
                }

                if (EnemyStep == true && IsMove != 4)
                {
                    if (IsMove == 1)
                    {
                        AnimatorEnemy.SetBool("Walk", true);
                        Enemy.transform.position = Vector3.Lerp(EnPos, PlPos, progressMove);
                        if (progressMove <= 1)
                        {
                            progressMove += speed;
                        }
                    }
                    if (progressMove >= 1 && IsMove == 1)
                    {
                        AnimatorEnemy.SetBool("Walk", false);
                        Attacks();
                    }

                    if (IsMove == 2)
                    {
                        AnimatorEnemy.SetBool("Walk", true);
                        Enemy.transform.position = Vector3.Lerp(EnPos, PlPos, progressMove);
                        progressMove -= speed;
                    }
                    if (progressMove <= 0 && IsMove == 2) { AnimatorEnemy.SetBool("Walk", false); PlayerStep = true; EnemyStep = false; CriticalBoolEn = false; IsMove = 0; Die(); UiController.AttackInteractableTrue(); }
                }
            }
        }
        finally
        {
            //Nothing
        }
    }


    public void FieldEscape()
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        EnemyData EnemyData = Enemy.GetComponent<EnemyData>();

        CriticalBoolEn = false;
        PlayerStep = false;
        EnemyStep = true;
        IsMove = 1;

        PlPos = Player.transform.position;
        EnPos = Enemy.transform.position;

        UiController.AttackInteractableFalse();

        if (EnemyData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
    }


    public void ResetData()
    {
        IsMove = 0;
        PlayerStep = true;
        EnemyStep = false;
        CriticalBoolPl = false;
        CriticalBoolEn = false;
        progressMovePreFight = 0;
        //UiController.AttackInteractableTrue();
    }


    public void OnDefendButtoClicked()
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        EnemyData EnemyData = Enemy.GetComponent<EnemyData>();

        PlayerData.energy += PlayerData.maxEnergy / 2;
        eventText.text += "<#fcf403>You've recovered some energy</color> \n";

        CriticalBoolEn = false;
        PlayerStep = false;
        EnemyStep = true;
        IsMove = 1;

        PlPos = Player.transform.position;
        EnPos = Enemy.transform.position;

        UiController.AttackInteractableFalse();

        if (EnemyData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
    }

    public void SkipPlayerStep()
    {
        if (IsMove != 4)
        {
            EnemyData EnemyData = Enemy.GetComponent<EnemyData>();

            CriticalBoolEn = false;
            PlayerStep = false;
            EnemyStep = true;
            IsMove = 1;

            PlPos = Player.transform.position;
            EnPos = Enemy.transform.position;

            UiController.AttackInteractableFalse();

            if (EnemyData.CriticalChance >= Random.Range(0, 100)) { CriticalBoolEn = true; }
        }

    }

    private IEnumerator FadeOutAndDestroy(GameObject target, float fadeTime)
    {
        try
        {
            if (target != null)
            {
                GameLogic GameLogic = PlayerStats.GetComponent<GameLogic>();
                Renderer renderer = target.GetComponent<Renderer>();
                float elapsedTime = 0f;
                Color initialColor = renderer.material.color;

                while (elapsedTime < fadeTime)
                {
                    float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
                    renderer.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                target.SetActive(false);

                // ќсь тут перев≥рте, чи об'Їкт ще ≥снуЇ перед тим, €к до нього звернутис€
                if (GameLogic != null)
                {
                    GameLogic.SpawnEnemy(1, true);
                }

                // ќсь тут ви звертаЇтес€ до ≥нших компонент≥в, перев≥р€ючи, чи об'Їкт не Ї null
                if (Enemy != null)
                {
                    Enemy = GameObject.FindGameObjectWithTag("Enemy");
                    EnPos = Enemy.transform.position;
                    AnimatorEnemy = Enemy.GetComponent<Animator>();
                    IsAttacked IsAttackedEn = Enemy.GetComponent<IsAttacked>();
                    UiController.AttackInteractableTrue();
                }
            }
        }
        finally
        {
            //Nothing
        }
    }

    public void IsMoveChange(int a)
    {
        IsMove = a;
    }    
    /*
    public void PlayerDie()
    {
        PlayerData PlayerData = PlayerStats.GetComponent<PlayerData>();
        PlayerData.Die();
    }*/
}