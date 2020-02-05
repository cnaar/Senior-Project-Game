using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour {

    BattleFlow BF;

    public float enemymaxHP = 150;
    public float enemyHP;

    public static float enemyAtk;
    public float enemybaseAtk = 80;

    public static float enemyDef;
    public float enemybaseDef = 50;

    public Transform hitEffectObj;
    public Transform damageTxtObj;
    public static float playerDef;

    public Vector2 startingposition;

    public static float enemyDamage;
    public int enemyXP = 100;

	// Use this for initialization
	void Start () {

        playerDef = 0;

        BF = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleFlow>();

        enemyHP = enemymaxHP;
        enemyAtk = enemybaseAtk;
        enemyDef = enemybaseDef;
	}
	
	// Update is called once per frame
	void Update () {

        if ((BattleFlow.damageDisplay == "y") && (gameObject.name == BattleFlow.selectedEnemy))
        {
            enemyHP -= BattleFlow.currentDamage;
            Instantiate(hitEffectObj, transform.position, hitEffectObj.rotation);
            Debug.Log(gameObject.name + "'s HP is currently " + enemyHP);
            BattleFlow.damageDisplay = "n";
        }

        if(enemyHP <= 0)
        {
            BattleFlow.playerTotalXP += enemyXP;
            BF.EnemiesinBattle.Remove(this.gameObject);
            Destroy(gameObject);

            if(BF.EnemiesinBattle.Count == 0)
            {
                BF.BattleOverPlayerWon();
            }
        }
	}

    public void Attack()
    {
        startingposition = this.transform.position;
        GetComponent<Rigidbody>().velocity = new Vector2(5, 0);
        StartCoroutine(enemyReturn());
    }

    IEnumerator enemyReturn()
    {
        yield return new WaitForSeconds(0.5f);

        if(BattleFlow.clickedDef == true)
        {
            enemyDamage = enemyAtk - (PlayerCon.Def * 2);
        }
        else
        {
            enemyDamage = enemyAtk - PlayerCon.Def;
        }

        Debug.Log("Damage from enemy: " + enemyDamage);

        if (enemyDamage < 0)
        {
            enemyDamage = 0;
        }

        BattleFlow.currentDamage = enemyDamage;
        PlayerCon.HP -= BattleFlow.currentDamage;
        Instantiate(damageTxtObj, new Vector2(4.22f, 1f), damageTxtObj.rotation);
        Instantiate(hitEffectObj, new Vector2(4.22f, -0.22f), hitEffectObj.rotation);
        Debug.Log("Player HP = " + PlayerCon.HP);
        GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        GetComponent<Transform>().position = startingposition;
        yield return new WaitForSeconds(0.8f);

    }

    void OnMouseDown()
    { 
        BattleFlow.selectedEnemy = gameObject.name;
    }

}
