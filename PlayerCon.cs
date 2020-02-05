using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour {

    public float level = 1;
    public static float levelCap = 100;

    public static float HP;
    public float maxHP = 300;

    public static float SP;
    public float maxSP = 250;

    public static float Atk;
    public float baseAtk = 100;

    public static float Def;
    public float baseDef = 30;

    public Transform damageTxtObj;
    public Transform fireballObj;

    // Use this for initialization
    void Start () {
        HP = maxHP;
        SP = maxSP;
        Atk = baseAtk;
        Def = baseDef;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space") && (BattleFlow.playerturn == true))
        {
            BattleFlow.clickedDef = false;
            Attack();
        }

        if (Input.GetKeyDown("a") && (BattleFlow.playerturn == true) && (SP >= 30))
        {
            BattleFlow.clickedDef = false;
            BurningShotAttack();
            SP -= 30;
        }
        else if(Input.GetKeyDown("a") && (BattleFlow.playerturn == true) && (SP <= 30))
        {
            Debug.Log("Insufficient SP");
            BattleFlow.clickedDef = false;
            Attack();
        }
        if (Input.GetKeyDown("d") && (BattleFlow.playerturn == true))
        {
            Defense();
        }

        if (HP <= 0)
        {
            BattleFlow.playerStatus = "dead";
            Destroy(gameObject);
        }
        while(BattleFlow.playerTotalXP > levelCap)
        {
            
        }
    }

    void Attack()
    {
        BattleFlow.currentDamage = Atk - EnemyCon.enemyDef;

        if(BattleFlow.currentDamage <= 0)
        {
            BattleFlow.currentDamage = 0;
        }

        GetComponent<Animator>().SetTrigger("player_arrow shot");
        StartCoroutine(Attacking());
    }

    void BurningShotAttack()
    {
        BattleFlow.currentDamage = (Atk*1.5f) - EnemyCon.enemyDef;
        SP -= 20;

        if (BattleFlow.currentDamage <= 0)
        {
            BattleFlow.currentDamage = 0;
        }

        GetComponent<Animator>().SetTrigger("player_arrow shot");
        Instantiate(fireballObj, new Vector2(3.36f, -0.38f), fireballObj.rotation);
        StartCoroutine(Attacking());
    }

    void Defense()
    {
        StartCoroutine(Defending());
    }


    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1.45f);
        Instantiate(damageTxtObj, new Vector2(-3.85f, 1f), damageTxtObj.rotation);
        BattleFlow.damageDisplay = "y";
        yield return new WaitForSeconds(1.45f);
        BattleFlow.playerturn = false;
    }

    IEnumerator Defending()
    {
        yield return new WaitForSeconds(0.5f);
        BattleFlow.clickedDef = true;
        BattleFlow.playerturn = false;
    }
}
