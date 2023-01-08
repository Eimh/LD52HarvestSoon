using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    [SerializeField]
    private int Health;

    private int hunger;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private GameObject[] icons;

    private void Awake() {
        hunger = Health;
    }

    public bool ImHungry(int damage = 1) {
        hunger -= damage;
        playerAnimator.SetInteger("Hunger", hunger);
        OnChange();
        return hunger >= 0;
    }

    public bool Eat(int heal) {
        if (hunger == Health) return false;
        hunger += heal;
        hunger = Mathf.Clamp(hunger, 0, Health);
        playerAnimator.SetInteger("Hunger", hunger);
        OnChange();
        return true;
    }

    public bool IsEmpty() {
        return hunger <= 0;
    }

    private void OnChange() {
        for (int i = 0; i < Health; i++) {
            icons[i].SetActive(i < hunger);
        }
    }
}
