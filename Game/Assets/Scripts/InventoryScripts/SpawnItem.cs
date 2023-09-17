using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject LootDispley;
    private Item[] dropItems;

    public void SpawnLoot()
    {
        if (SceneManager.GetActiveScene().name == "Battle")
        {
            GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
            EnemyData Enemydata = Enemy.GetComponent<EnemyData>();

            dropItems = Enemydata.DropItems;

            int numTypes = Random.Range(1, dropItems.Length + 1);

            Instantiate(LootDispley, GameObject.FindGameObjectWithTag("Canvas").transform);
            GameObject Lootdisplay = GameObject.FindGameObjectWithTag("LootDisplay");
            if (Lootdisplay != null)
            {
                List<int> selectedIndices = new List<int>();
                for (int i = 0; i < numTypes; i++)
                {
                    int randomIndex = GetRandomUniqueIndex(dropItems.Length, selectedIndices);
                    int randomCount = 1; // Замість випадкового значення встановлюємо 1

                    // Перевіряємо, чи предмет stackable
                    if (dropItems[randomIndex].stackable)
                    {
                        randomCount = Random.Range(1, dropItems[randomIndex].MaxStackValue + 1);
                    }

                    bool result = inventoryManager.AddItemLoot(dropItems[randomIndex], randomCount);
                    selectedIndices.Add(randomIndex);
                }
                Lootdisplay.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name == "Map")
        {
            GameObject Map = GameObject.FindGameObjectWithTag("TreasureChest");
            TreasureChest TreasureChest = Map.GetComponent<TreasureChest>();
            PlayerData playerData = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerData>();


            dropItems = TreasureChest.DropItems;

            int numTypes = Random.Range(1, dropItems.Length + 1);

            Instantiate(LootDispley, GameObject.FindGameObjectWithTag("Canvas").transform);
            GameObject Lootdisplay = GameObject.FindGameObjectWithTag("LootDisplay");
            if (Lootdisplay != null)
            {
                List<int> selectedIndices = new List<int>();
                for (int i = 0; i < numTypes; i++)
                {
                    int randomIndex = GetRandomUniqueIndex(dropItems.Length, selectedIndices);
                    int randomCount = Random.Range(1, 1); // Випадкова кількість
                    bool result = inventoryManager.AddItemLoot(dropItems[randomIndex], randomCount);
                    selectedIndices.Add(randomIndex);
                }
                playerData.ChestsOpened += 1;
                Lootdisplay.SetActive(false);
            }
        }
    }

    private int GetRandomUniqueIndex(int maxIndex, List<int> excludeIndices)
    {
        List<int> possibleIndices = new List<int>();
        for (int i = 0; i < maxIndex; i++)
        {
            if (!excludeIndices.Contains(i))
            {
                possibleIndices.Add(i);
            }
        }

        if (possibleIndices.Count == 0)
        {
            return -1; // Якщо всі індекси виключені, повертаємо -1 або можемо обробити цю ситуацію за потреби
        }

        int randomIndex = Random.Range(0, possibleIndices.Count);
        return possibleIndices[randomIndex];
    }

    public void OnSearhLootButtonClicked()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas != null)
        {
            Transform lootTransform = canvas.transform.Find("LootInv(Clone)");
            if (lootTransform != null)
            {
                GameObject lootDisplay = lootTransform.gameObject;
                lootDisplay.SetActive(true);
            }
        }
    }

    /*
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(dropItems[id]);
    }

    public void PickupItemRandom()
    {
        int randomIndex = Random.Range(0, dropItems.Length);
        bool result = inventoryManager.AddItem(dropItems[randomIndex]);
    }
    */
}
