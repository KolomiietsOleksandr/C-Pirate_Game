/*--------------------------------------
   Email  : hamza95herbou@gmail.com
   Github : https://github.com/herbou
----------------------------------------*/

using System;
using System.Collections;
using UnityEngine ;
using UnityEngine.Events ;
using UnityEngine.EventSystems ;
using UnityEngine.UI ;


[RequireComponent(typeof(Button))]
public class ButtonLongPressListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [Tooltip("Hold duration in seconds")]
    [Range(0.3f, 5f)] public float holdDuration = 0.5f;
    public UnityEvent onLongPress;

    public bool isPointerDown = false;
    public bool isLongPressed = false;
    public DateTime pressTime;

    public Button button;

    private WaitForSeconds delay;


    private void Awake() {
        button = GetComponent<Button>();
        delay = new WaitForSeconds(0.1f);
    }

    public void OnPointerDown(PointerEventData eventData) {
        isPointerDown = true;
        pressTime = DateTime.Now;
        StartCoroutine(Timer());
    }


    public void OnPointerUp(PointerEventData eventData) {
        isPointerDown = false;
        isLongPressed = false;
    }

    private IEnumerator Timer() {
        while (isPointerDown && !isLongPressed) {
            double elapsedSeconds = (DateTime.Now - pressTime).TotalSeconds;

            if (elapsedSeconds >= holdDuration) {
                isLongPressed = true;
                if (button.interactable)
                {
                    onLongPress?.Invoke();
                    isPointerDown = false;
                    isLongPressed = false;
                }
                yield break;
            }

            yield return delay;
        }
    }
}
