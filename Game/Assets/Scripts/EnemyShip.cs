using UnityEngine;
using static GameLogic;

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
        VesselsData vesselsData = GameObject.FindWithTag("EnemyVessel").GetComponent<EnemyShip>();
        if (Cannon != 0 && SceneManager.GetActiveScene().name == "Battle") && gameLogic.selectedEvent == "Fight")
        {
            VesselsData.VesselHealth -= CannonDamage * Cannon;

            if (VesselsData.VesselHealth == 0)
            {
                Destroy(VesselsData.gameObject);
            }
        }
    }
}
