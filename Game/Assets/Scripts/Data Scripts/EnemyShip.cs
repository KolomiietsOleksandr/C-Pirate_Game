using UnityEngine;
using static GameLogic;
using UnityEngine.SceneManagement;

public class EnemyShip : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(10f, 0f, 5f); // ���� ���������� ������
    public int health;
    public int Cannon;
    public int CannonDamage;

    public void SpawnEnemyShip()
    {
        GameObject enemyShip = Instantiate(this.gameObject, spawnPosition, Quaternion.identity);
    }
    private void Update()
    {
        GameLogic Logic = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLogic>();

        if (Logic != null && Logic.selectedEvent != "EnemyVessel")
        {
            Destroy(this.gameObject);
        }
    }
    // �������� ������ �� ���������������� ��� �������� �������

    public void CannonShootEnemy()
    {
        GameLogic gameLogic = GameObject.Find("EventManager").GetComponent<GameLogic>();
        GameObject enemyVesselGO = GameObject.FindWithTag("EnemyVessel");
        VesselsData vesselsData = enemyVesselGO.GetComponent<VesselsData>();
        if (Cannon != 0 && SceneManager.GetActiveScene().name == "Battle" && gameLogic.selectedEvent == "Fight")
        {
            vesselsData.VesselHealth -= CannonDamage * Cannon;

            if (vesselsData.VesselHealth == 0)
            {
                Destroy(enemyVesselGO);
            }
        }
    }
}
