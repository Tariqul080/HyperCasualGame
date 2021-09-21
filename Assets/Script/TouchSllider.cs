using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSllider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnpointerDownEvent;
    public UnityAction<float> OnpointerDragEvent;
    public UnityAction OnpointerUpEvent;

    private Slider uiSlider ;
    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValuChanged);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnpointerDownEvent != null)
        {
            OnpointerDownEvent.Invoke();
        }
        if (OnpointerDragEvent != null)
        {
            OnpointerDragEvent.Invoke(uiSlider.value);
        }
       
    }
    private void OnSliderValuChanged(float valu)
    {
        if (OnpointerDragEvent != null)
        {
            OnpointerDragEvent.Invoke(-valu);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnpointerUpEvent != null)
        {
            OnpointerUpEvent.Invoke();
        }
        //resr sliderValue 
        uiSlider.value = 0.0f;
    }
    private void OnDestroy()
    {
        //
        uiSlider.onValueChanged.RemoveListener(OnSliderValuChanged);
    }
}
