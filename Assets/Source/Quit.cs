using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerEnterHandler
{
    public void OnQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData ped) {
        SoundManager.PlaySound(SoundManager.Effect.Select);
    }
}
