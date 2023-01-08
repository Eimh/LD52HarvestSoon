using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    public enum Effect {
        Select = 4,
        Death = 0,
        Invalid = 1,
        Axe = 8,
        Scythe = 9,
        Hoe = 7,
        FillWateringCan = 11,
        UseWateringCan = 10,
        Seeds = 12,
        Start = 2,
        DeathScreen = 3,
        Eat = 14
    }
    private AudioSource audioSource;
    private static SoundManager instance;
    [SerializeField]
    private AudioClip[] clips;
    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
    }
    void Start() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(Effect effect) {
        instance.audioSource.PlayOneShot(instance.clips[(int)effect]);
    }
}
