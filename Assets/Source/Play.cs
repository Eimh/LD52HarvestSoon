using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Play : MonoBehaviour, IPointerEnterHandler {
    private Button button;
    void Start() {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData ped) {
        SoundManager.PlaySound(SoundManager.Effect.Select);
    }

    public void OnPlay() {
        SoundManager.PlaySound(SoundManager.Effect.Start);
        button.interactable = false;
        SceneLoader.StartLoadScene("Game");
        SceneLoader.CompleteLoadScene();
        //begin animation
    }
}
