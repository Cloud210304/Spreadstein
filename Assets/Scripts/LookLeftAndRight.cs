using UnityEngine;

public class LookLeftAndRight : MonoBehaviour
{
    public Animator animator;
    public GameObject SlotsUI;
    public GameObject LeftUI;
    public GameObject RightUI;
    public GameObject HideUI;

    public void SlotsStraight()
    {
        animator.SetTrigger("Slots");
        SlotsUI.SetActive(true);
        LeftUI.SetActive(false);
        RightUI.SetActive(false);
        HideUI.SetActive(false);
    }

    public void CorridorRight()
    {
        animator.SetTrigger("Right");
        RightUI.SetActive(true);
        LeftUI.SetActive(false);
        HideUI.SetActive(false);
        SlotsUI.SetActive(false);
    }

    public void CorridorLeft()
    {
        animator.SetTrigger("Left");
        LeftUI.SetActive(true);
        RightUI.SetActive(false);
        HideUI.SetActive(false);
        SlotsUI.SetActive(false);
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        HideUI.SetActive(true);
        LeftUI.SetActive(false);
        RightUI.SetActive(false);
        SlotsUI.SetActive(false);
    }

}