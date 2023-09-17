using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cross : MonoBehaviour
{
    public Image image;
    public Sprite RedCross;
    public Sprite GreenCross;

    public bool TreasureCross;
    public float fadeDuration = 1f;
    public float blinkDuration = 0.2f;
    public int blinkCount = 3;

    public GameObject eventObject;
    private GameLogic gameLogic;

    public void Awake()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

        public IEnumerator FadeOutAndDestroy()
    {
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        for (float t = 0; t < 1f; t += Time.deltaTime / fadeDuration)
        {
            image.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        image.color = targetColor;

        Destroy(gameObject);       
    }

    public IEnumerator BlinkAndTriggerEvent()
    {
        Color originalColor = image.color;

        for (int i = 0; i < blinkCount; i++)
        {
            image.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);

            image.color = Color.clear;
            yield return new WaitForSeconds(blinkDuration);
        }

        if (eventObject != null)
        {
            eventObject.SetActive(true);
            SceneManager.LoadScene("Map");
            gameLogic.selectedEvent = "Fight";
        }
    }
}
