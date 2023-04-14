using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Beheviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("Highlighted");

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        anim.ResetTrigger("Highlighted");

    }
}
