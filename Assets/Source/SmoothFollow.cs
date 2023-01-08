using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.01f;
    [SerializeField]
    private bool smooth = true;

    void LateUpdate() {
        Vector2 pos;
        if (smooth) {
            if (((Vector2)(transform.position - target.position)).sqrMagnitude > 0.1f)
                pos = Vector2.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
            else pos = transform.position;
        } else {
            pos = target.position;
        }
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
