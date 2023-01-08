using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    [SerializeField]
    private Sprite[] icons;

    private void Awake() {
        instance = this;
    }

    private static ItemManager instance;

    public static Sprite GetIcon(int id) {
        return instance.icons[id];
    }
}

