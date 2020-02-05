using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFlow : MonoBehaviour {

    PlayerCon PC;
    EnemyCon EC;

    public static bool playerturn = true;
    public static float currentDamage = 0;

    public static string damageDisplay = "n";
    public static string playerStatus = "Ok";

    public static bool clickedDef;

    public static int playerTotalXP = 0;

    public List<GameObject> EnemiesinBattle = new List<GameObject>();

    public static string selectedEnemy;

	// Use this for initialization
	void Start () {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
        EC = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCon>();

        EnemiesinBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
	}
	
	// Update is called once per frame
	void Update () {
        if (playerturn == false && !(EnemiesinBattle.Count == 0))
        {
            EnemyTurn();
            playerturn = true;
        }

    }

    void EnemyTurn()
    {
        foreach (GameObject enemy in EnemiesinBattle)
        {
            enemy.GetComponent<EnemyCon>().Attack();
        }
    }

    public void BattleOverPlayerWon()
    {
        Debug.Log("Battle won!");
        Debug.Log("player xp earned: " + playerTotalXP);
    }
}
