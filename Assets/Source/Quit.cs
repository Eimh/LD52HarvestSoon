using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerEnterHandler, ISelectHandler {
    [SerializeField]
    private EventSystem eventSystem;
    public void OnQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData ped) {
        eventSystem.SetSelectedGameObject(gameObject);
        //SoundManager.PlaySound(SoundManager.Effect.Select);
    }

    public void OnSelect(BaseEventData eventData) {
        SoundManager.PlaySound(SoundManager.Effect.Select);
    }
}
