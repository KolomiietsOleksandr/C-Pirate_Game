using UnityEngine;
using TMPro;
using static PlayerData;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class GameLogic : MonoBehaviour
{
    // Text with events  
    public TextMeshProUGUI eventText;

    // Оdds of events
    public float nothingChance;
    public float enemyVesselChance;
    public float merchantShipChance;
    public float BottleMapChance;

    private int stepsSinceLastMerchantShip = 0;
    private const int stepsThresholdForMerchantShip = 10;

    [SerializeField] private string[] PassivePhrases;

    public GameObject[] enemyVesselPrefab;
    public GameObject[] enemyPrefab;

    public GameObject[] TreasureMaps;

    public GameObject IslandsPrefab;
    public GameObject MerchantShipPrefab;

    [SerializeField] private UIController UiController;

    public string selectedEvent;

    public GameObject Canvas;
    public GameObject EventMenager;
    public GameObject MainCamera;
    public GameObject PlayerVessel;
    private static GameObject persistentCanvas;
    private static GameObject persistentEventMenager;
    private static GameObject persistentCamera;
    private static GameObject persistentPlayerVessel;

    public GameObject Square;

    public int PreFight = 0;
    public bool AfterIsland = false;

    public void OnSailButtonClicked()
    {
        if (selectedEvent != "Pier" && selectedEvent != "Fight")
        {
            GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (Inventory != null)
            {
                Inventory.SetActive(false);
            }
            if (selectedEvent == "BottleMap")
            {
                GameObject Map = GameObject.FindGameObjectWithTag("Map");
                if (Map != null)
                {
                    Destroy(Map);
                }
            }

            AlphaAnimator alphaAnimator = Square.GetComponent<AlphaAnimator>();

            PlayerData playerData = GetComponent<PlayerData>(); // Отримання посилання на інший скрипт
            playerData.experience += 10; // Отримання значення змінної
            playerData.Thirst -= playerData.ThirstForStep;
            playerData.Hunger -= playerData.HungerForStep;

            if (playerData.Thirst <= 0)
            {
                playerData.energy -= playerData.ThirstForStep;
            }
            if (playerData.Hunger <= 0)
            {
                playerData.health -= playerData.HungerForStep;
            }

            float randomValue = Random.value;


            if (randomValue <= nothingChance)
            {
                if (selectedEvent != "Nothing")
                {
                    alphaAnimator.PlayAnimation();
                    selectedEvent = "Nothing";
                }
                else { selectedEvent = "Nothing"; }
            }
            else if (randomValue <= nothingChance + enemyVesselChance)
            {
                eventText.text += "Captain, on the course <#bf2424>enemy vessel</color>" + '\n';
                selectedEvent = "EnemyVessel";
                alphaAnimator.PlayAnimation();
            }
            else if (stepsSinceLastMerchantShip <= stepsThresholdForMerchantShip && randomValue <= nothingChance + enemyVesselChance + merchantShipChance)
            {
                eventText.text += "Captain, on the course <#fcf403>merchant ship</color>" + '\n';
                selectedEvent = "MerchantShip";
                stepsSinceLastMerchantShip = 0;
                alphaAnimator.PlayAnimation();
            }
            else if (randomValue <= nothingChance + enemyVesselChance + merchantShipChance + BottleMapChance)
            {
                eventText.text += "<#218009>I see a bottle, maybe there's a map in it?</color>" + '\n';
                selectedEvent = "BottleMap";
                alphaAnimator.PlayAnimation();
            }

            stepsSinceLastMerchantShip++;

            switch (selectedEvent)
            {
                case "Nothing":
                    // Логіка для івенту "Нічого"
                    eventText.text += PassivePhrases[Random.Range(1, PassivePhrases.Length)] + '\n';
                    UiController.RefreshUI();
                    UiController.SailUI();
                    break;
                case "EnemyVessel":
                    // Логіка для івенту "Ворожий корабель"
                    // Вибираємо випадковий префаб ворожого судна з масиву
                    GameObject enemyVessel = enemyVesselPrefab[Random.Range(0, enemyVesselPrefab.Length)];
                    // Спавн префабу ворожого судна
                    EnemyShip enemyShipScript = enemyVessel.GetComponent<EnemyShip>();
                    enemyShipScript.SpawnEnemyShip();
                    UiController.RefreshUI();
                    UiController.PreFightUI();
                    break;
                case "MerchantShip":
                    // Логіка для івенту "Торговий корабель"
                    if (GameObject.FindGameObjectWithTag("MerchantShip") == null)
                    {
                        Instantiate(MerchantShipPrefab);

                        UiController.RefreshUI();
                        UiController.MerchantVesselUI();
                    }
                    break;
                case "BottleMap":
                    // Логіка для івенту "Острови"
                    if (GameObject.FindGameObjectWithTag("Islands") == null)
                    {
                        Instantiate(IslandsPrefab);

                        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
                        if (canvas != null)
                        {
                            Transform mapTransform = canvas.transform.Find("Map1(Clone)");
                            if (mapTransform == null)
                            {
                                mapTransform = canvas.transform.Find("Map2(Clone)");
                                if (mapTransform == null)
                                {
                                    mapTransform = canvas.transform.Find("Map3(Clone)");
                                }
                            }

                            if (mapTransform == null)
                            {
                                GameObject treasureMap = TreasureMaps[Random.Range(0, TreasureMaps.Length)];
                                Instantiate(treasureMap, GameObject.FindGameObjectWithTag("Canvas").transform);
                                treasureMap.SetActive(false);
                            }

                            UiController.RefreshUI();
                            UiController.BottleMapUI();
                        }
                    }
                    break;
            }
            if (selectedEvent != "BottleMap")
            {
                Destroy(GameObject.FindGameObjectWithTag("Islands"));
            }
            if (selectedEvent != "MerchantShip")
            {
                Destroy(GameObject.FindGameObjectWithTag("MerchantShip"));
            }
        }
    }


    public void SailAfterFight()
    {
        if (selectedEvent == "Fight" && SceneManager.GetActiveScene().name == "Battle")
        {
            AlphaAnimator alphaAnimator = Square.GetComponent<AlphaAnimator>();
            alphaAnimator.PlayAnimation();

            GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (Inventory != null)
            {
                Inventory.SetActive(false);
            }

            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                Transform lootTransform = canvas.transform.Find("LootInv(Clone)");
                if (lootTransform != null)
                {
                    GameObject lootDisplay = lootTransform.gameObject;
                    Destroy(lootDisplay);
                }
            }

            selectedEvent = "Nothing";

            eventText.text += "It was a difficult battle..." + '\n';
            SceneManager.LoadScene("Sail");

            UiController.RefreshUI();
            UiController.SailUI();

            SpawnPlayerVessel();
        }
    }


    public void SailAfterPier()
    {
        if (selectedEvent == "Pier")
        {
            AlphaAnimator alphaAnimator = Square.GetComponent<AlphaAnimator>();
            alphaAnimator.PlayAnimation();

            GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (Inventory != null)
            {
                Inventory.SetActive(false);
            }

            selectedEvent = "Nothing";

            eventText.text = "Good riddance, sailors!" + '\n';
            SceneManager.LoadScene("Sail");

            UiController.RefreshUI();
            UiController.SailUI();

            SpawnPlayerVessel();
        }
    }


    public void SailAfterMap()
    {
        if (SceneManager.GetActiveScene().name == "Map" && selectedEvent == "Fight")
        {
            AlphaAnimator alphaAnimator = Square.GetComponent<AlphaAnimator>();
            alphaAnimator.PlayAnimation();

            GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
            if (Inventory != null)
            {
                Inventory.SetActive(false);
            }

            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                Transform lootTransform = canvas.transform.Find("LootInv(Clone)");
                if (lootTransform != null)
                {
                    GameObject lootDisplay = lootTransform.gameObject;
                    Destroy(lootDisplay);
                }
            }

            eventText.text += "A good treasure, captain!" + '\n';
            AfterIsland = true;
            UiController.RefreshUI();
            UiController.BottleMapUI();
            SceneManager.LoadScene("Sail");

            //selectedEvent = "BottleMap";        

            SpawnPlayerVessel();
        }
    }


    public void OnEscapeButtonClicked()
    {
        AlphaAnimator alphaAnimator = Square.GetComponent<AlphaAnimator>();

        PlayerData playerData = GetComponent<PlayerData>(); // Отримання посилання на скрипт PlayerData
        HumanFight humanFight = GetComponent<HumanFight>();

        float escapeChance = playerData.escapeChance; // Отримання шансу на уникнення бійки з PlayerData

        float randomValue = Random.value;

        if (selectedEvent == "EnemyVessel")
        {
            if (randomValue < escapeChance)
            {
                // Гравець успішно уникнув бійки
                eventText.text += "<#218009>You managed to escape!</color>" + '\n';
                Destroy(GameObject.FindGameObjectWithTag("EnemyVessel"));
                alphaAnimator.PlayAnimation();
                UiController.RefreshUI();
                UiController.SailUI();


                GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
                if (Inventory != null)
                {
                    Inventory.SetActive(false);
                }

                playerData.SuccessfulEscapes += 1;
            }
            else
            {
                // Гравець не зміг уникнути бійки
                eventText.text += "<#bf2424>You failed to escape!</color>" + '\n';
                alphaAnimator.PlayAnimation();
                UiController.RefreshUI();
                UiController.FightUI();
                SceneManager.LoadScene("Battle");
                selectedEvent = "Fight";
                PreFight = 1;

                GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
                if (Inventory != null)
                {
                    Inventory.SetActive(false);
                }
            }
        }

        if (selectedEvent == "Fight" && PreFight != 1)
        {
            if (randomValue < escapeChance)
            {
                // Гравець успішно уникнув бійки
                eventText.text += "<#218009>You managed to escape!</color>" + '\n';
                SceneManager.LoadScene("Sail");
                selectedEvent = "Nothing";
                UiController.RefreshUI();
                UiController.SailUI();               
                alphaAnimator.PlayAnimation();
                SpawnPlayerVessel();

                playerData.SuccessfulEscapes += 1;
            }
            else
            {
                // Гравець не зміг уникнути бійки
                eventText.text += "<#bf2424>You failed to escape!</color>" + '\n';               
                humanFight.FieldEscape();
            }
        }

    }


    public void OnDockButtonClicked()
    {
        // Перехід на іншу сцену з допомогою SceneManager
        SceneManager.LoadScene("Pier");
        eventText.text += "Holy ground!" + '\n';
        selectedEvent = "Pier";
        UiController.RefreshUI();
        UiController.StreetUI();

        GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
        if (Inventory != null)
        {
            Inventory.SetActive(false);
        }
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        Transform InvTransform = canvas.transform.Find("Inventory");
        GameObject InventoryGo = InvTransform.gameObject;

        InventoryGo.SetActive(true);
        InventoryGo.SetActive(false);

        SpawnPlayerVessel();
    }


    public void OnBoardButtonClicked()
    {
        // Перехід на іншу сцену з допомогою SceneManager
        SceneManager.LoadScene("Battle");
        selectedEvent = "Fight";
        UiController.RefreshUI();
        UiController.FightUI();
        PreFight = 1;

        GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
        if (Inventory != null)
        {
            Inventory.SetActive(false);
        }
    }

    public void OnSearchMapButtonClicked()
    {
        if (SceneManager.GetActiveScene().name != "Map")
        {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                Transform mapTransform = canvas.transform.Find("Map1(Clone)");
                if (mapTransform == null)
                {
                    mapTransform = canvas.transform.Find("Map2(Clone)");
                    if (mapTransform == null)
                    {
                        mapTransform = canvas.transform.Find("Map3(Clone)");
                    }
                }

                if (mapTransform != null)
                {
                    GameObject Map = mapTransform.gameObject;
                    Map.SetActive(true);
                    if (GameObject.Find("Inventory") != null)
                    {
                        GameObject.Find("Inventory").SetActive(false);
                    }
                }
            }
        }       
    }

    public void OnInventoryButtonClicked()
    {
        if (GameObject.Find("Map1(Clone)") != null)
        {
            GameObject.Find("Map1(Clone)").SetActive(false);
        }

        if (GameObject.Find("Map2(Clone)") != null)
            {
            GameObject.Find("Map2(Clone)").SetActive(false);
        }

        if (GameObject.Find("Map3(Clone)") != null)
            {
            GameObject.Find("Map3(Clone)").SetActive(false);
        }
    }



    void Awake()
    {
        if (persistentCanvas == null)
        {
            persistentCanvas = Canvas;
            DontDestroyOnLoad(persistentCanvas);
        }
        else
        {
            Destroy(Canvas);
        }

        if (persistentEventMenager == null)
        {
            persistentEventMenager = EventMenager;
            DontDestroyOnLoad(persistentEventMenager);
        }
        else
        {
            Destroy(EventMenager);
        }

        if (persistentCamera == null)
        {
            persistentCamera = MainCamera;
            DontDestroyOnLoad(persistentCamera);
        }
        else
        {
            Destroy(MainCamera);
        }        
        /*
        if (persistentPlayerVessel == null)
        {
            persistentPlayerVessel = PlayerVessel;
            DontDestroyOnLoad(PlayerVessel);
        }
        else
        {
            Destroy(PlayerVessel);
        }
        */
    }


    public void Update()
    {
        HumanFight humanFight = GetComponent<HumanFight>();
        PlayerData playerData = GetComponent<PlayerData>();

        PlayerVessel = playerData.currentShipPrefab;

        if (SceneManager.GetActiveScene().name == "Pier" && !GameObject.FindWithTag("PlayerVessel"))
        {
            SpawnPlayerVessel();
        }

        if (SceneManager.GetActiveScene().name != "Battle" && SceneManager.GetActiveScene().name != "Map")
        {
            if (!GameObject.FindWithTag("PlayerVessel") || GameObject.FindWithTag("PlayerVessel").name != playerData.currentShipPrefab.name + "(Clone)")
            {
                if (GameObject.FindWithTag("PlayerVessel") != null)
                {
                    Destroy(GameObject.FindWithTag("PlayerVessel"));
                }

                PlayerVessel = playerData.currentShipPrefab;
                SpawnPlayerVessel();
            }
        }

        if (SceneManager.GetActiveScene().name == "Battle" && PreFight == 1)
        {
            SpawnEnemy(1,false);
            PreFight = 0;
        }

        if (SceneManager.GetActiveScene().name == "Sail" && AfterIsland == true)
        {
            selectedEvent = "BottleMap";
            Instantiate(IslandsPrefab);
            AfterIsland = false;
        }

        else if (SceneManager.GetActiveScene().name != "Battle" && SceneManager.GetActiveScene().name != "Map")
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            humanFight.enabled = false;
            UiController.AttackInteractableTrue();
        }

        if (SceneManager.GetActiveScene().name == "Map")
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                Transform mapTransform = canvas.transform.Find("Map1(Clone)");
                if (mapTransform == null)
                {
                    mapTransform = canvas.transform.Find("Map2(Clone)");
                    if (mapTransform == null)
                    {
                        mapTransform = canvas.transform.Find("Map3(Clone)");
                    }
                }

                if (mapTransform != null)
                {
                    GameObject Map = mapTransform.gameObject;
                    Map.SetActive(false);
                }
            }
        }

        else if (selectedEvent == "Pier")
        {
            UiController.StreetUI();
        }

        if (SceneManager.GetActiveScene().name != "Map" && selectedEvent != "BottleMap")
        {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                Transform mapTransform = canvas.transform.Find("Map1(Clone)");
                if (mapTransform == null)
                {
                    mapTransform = canvas.transform.Find("Map2(Clone)");
                    if (mapTransform == null)
                    {
                        mapTransform = canvas.transform.Find("Map3(Clone)");
                    }
                }

                if (mapTransform != null)
                {
                    GameObject Map = mapTransform.gameObject;
                    Destroy(Map);
                }
            }
        }


        if (SceneManager.GetActiveScene().name == "Map" && PreFight == 1)
        {
            SpawnEnemy(2,false);
            PreFight = 0;
            UiController.RefreshUI();
            UiController.FightUI();
        }
        
    }

    public void SpawnEnemy(int num, bool Mov)
    {
        HumanFight humanFight = GetComponent<HumanFight>();
        PlayerData playerData = GetComponent<PlayerData>();

        int randomIndex = Random.Range(0, enemyPrefab.Length);
        if (Mov == true)
        {
            GameObject enemy = Instantiate(enemyPrefab[randomIndex], new Vector3(0.3f, enemyPrefab[randomIndex].transform.position.y, enemyPrefab[randomIndex].transform.position.z), Quaternion.identity);
            enemy.GetComponent<EnemyData>().MovingFromBack = true;
        }
        if (Mov == false)
        {
            Instantiate(enemyPrefab[randomIndex]);
        }
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerData.PlayerPrefab);
        }
        humanFight.enabled = true;
        humanFight.Start();
        humanFight.ResetData();
        humanFight.EnemyNum = num;
    }


    public void SpawnPlayerVessel()
    {
        Instantiate(PlayerVessel);
    }
}