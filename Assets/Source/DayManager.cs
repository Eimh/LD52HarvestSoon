using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class DayManager : MonoBehaviour
{
    [SerializeField]
    private Color night;

    [SerializeField]
    private int dayLength = 60;

    [SerializeField]
    private int nightLength = 10;

    private Light2D sun;
    private float dayRemaining;
    private UnityEvent DayEnded;
    private UnityEvent DayStarted;
    private bool dayStarted;
    // Start is called before the first frame update
    private void Awake() {
        DayEnded = new UnityEvent();
        DayStarted = new UnityEvent();
    }
    void Start() {
        sun = GetComponent<Light2D>();
    }

    public void StartDay() {
        dayStarted = true;
        sun.color = Color.white;
        dayRemaining = dayLength;
        Debug.Log("Day Wake");
        DayStarted.Invoke();
    }

    // Update is called once per frame
    void Update() {
        if (dayStarted) {
            dayRemaining -= Time.deltaTime;
            Mathf.Clamp(dayRemaining, 0, dayLength);
            if (dayRemaining < nightLength) {
                sun.color = Color.Lerp(Color.white, night, (nightLength - dayRemaining)/nightLength);
            }
            if (dayRemaining <= 0) {
                Debug.Log("Day sleep");
                DayEnded.Invoke();
                dayStarted = false;
            }
        }
    }

    public void Subscribe(UnityAction onSleep, UnityAction onWake) {
        DayEnded.AddListener(onSleep);
        DayStarted.AddListener(onWake);
        if (dayStarted) onWake();
    }
}
