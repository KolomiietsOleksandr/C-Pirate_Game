using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VesselsData : MonoBehaviour
{
    public int VesselHealth;
    public int MaxDaysInSail;
    public int Cost;
    public int Cannon;
    public int CannonDamage;

    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

    public void SpawnPlayerShip()
    {
        GameObject PlayerShip = Instantiate(this.gameObject, spawnPosition, Quaternion.identity);
    }
    private void Update()
    {
        GameLogic Logic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();
    }

    public void CannonShoot()
    {
        GameLogic gameLogic = GameObject.Find("EventManager").GetComponent<GameLogic>();
        EnemyShip enemyShip = GameObject.FindWithTag("EnemyVessel").GetComponent<EnemyShip>();
        if (Cannon != 0 && SceneManager.GetActiveScene().name == "Battle" && gameLogic.selectedEvent == "Fight")
        {
            enemyShip.health -= CannonDamage * Cannon;

            if (enemyShip.health == 0)
            {
                Destroy(enemyShip.gameObject);
            }
            else
            {
                enemyShip.CannonShootEnemy();
            }
        }
    }
}
