using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SleepTransition : MonoBehaviour
{
    [SerializeField]
    private DayManager dayManager;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private World world;
    [SerializeField]
    private Hunger hunger;
    private int days;
    [SerializeField]
    private GameObject died;
    // Start is called before the first frame update
    void Start() {
        days = 0;
        dayManager.Subscribe(OnSleep, OnWake);
    }

    private void OnWake() {

    }

    private void OnSleep() {
        animator.SetTrigger("Sleep");
    }

    private void OnCovered() {
        days++;
        if (hunger.ImHungry(days/5+1)) {
            world.OvernightProcess();
        } else {
            died.SetActive(true);
            died.GetComponent<TMP_Text>().text += $"after {days} days";
            SoundManager.PlaySound(SoundManager.Effect.DeathScreen);
            SceneLoader.StartLoadScene("Menu");
        }
        Debug.Log("asdasd");
    }

    private void OnUncover() {
        if (died.activeSelf) {
            SceneLoader.CompleteLoadScene();
        }
        dayManager.StartDay();
    }

}
