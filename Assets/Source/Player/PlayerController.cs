using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float movementSpeed = 0.1f;
    [SerializeField]
    private GameObject selector;
    [SerializeField]
    private World world;
    [SerializeField]
    private Animator animator;
    public Inventory inventory;
    public Hunger hunger;
    [SerializeField]
    private DayManager dayManager;

    private Inputs inputs;
    private Vector2 movement = Vector2.zero;

    private Vector2[] collider = {
        new Vector2(-2*0.125f, -4*0.125f),
        new Vector2(-2*0.125f, 3*0.125f),
        new Vector2(2*0.125f, 3*0.125f), 
        new Vector2(2*0.125f, -4*0.125f)};
    
    
    void Awake() {
        inputs = new Inputs();
        inputs.Player.Use.performed += ctx => OnUse();
        inputs.Player.Movement.started += ctx => animator.SetBool("Moving", true);
        inputs.Player.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += ctx => { movement = Vector2.zero; animator.SetBool("Moving", false); };
    }

    private void Start() {
        dayManager.Subscribe(OnSleep, OnWake);
        Debug.Log("Player Start");
    }

    private void OnWake() {
        Debug.Log("Player wake");
        animator.SetBool("Sleep", false);
        inputs.Player.Enable();
    }

    private void OnSleep() {
        Debug.Log("Player Sleep");
        inputs.Player.Disable();
        animator.SetBool("Sleep", true);
        if (hunger.IsEmpty()) {
            SoundManager.PlaySound(SoundManager.Effect.Death);
        }
    }

    private void OnUse() {
        Debug.Log("Used");
        Vector2Int pos = new Vector2Int((int)selector.transform.position.x, (int)selector.transform.position.y);
        Item item = inventory.GetItem(inventory.GetSelected());
        if (item == null) {
            SoundManager.PlaySound(SoundManager.Effect.Invalid);
            return;
        }
        if (item.Use(pos, world, this)) {
            inventory.Remove(inventory.GetSelected());
        }
    }

    private void OnDestroy() {
        inputs.Player.Disable();
    }

    void Update() {
        if (!movement.Equals(Vector2.zero)) {
            Vector2 direction = movement.normalized;
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            Vector2 next = (Vector2)transform.position + direction * movementSpeed * Time.deltaTime;
            if (world.CanMove((int)(transform.position.x + collider[0].x), (int)(transform.position.y + collider[0].y), (int)(next.x + collider[0].x), (int)(next.y + collider[0].y)) &&
                world.CanMove((int)(transform.position.x + collider[1].x), (int)(transform.position.y + collider[1].y), (int)(next.x + collider[1].x), (int)(next.y + collider[1].y)) &&
                world.CanMove((int)(transform.position.x + collider[2].x), (int)(transform.position.y + collider[2].y), (int)(next.x + collider[2].x), (int)(next.y + collider[2].y)) &&
                world.CanMove((int)(transform.position.x + collider[3].x), (int)(transform.position.y + collider[3].y), (int)(next.x + collider[3].x), (int)(next.y + collider[3].y)) &&
                world.CanExist(
                    new Vector2Int((int)(next.x + collider[0].x), (int)(next.y + collider[0].y)),
                    new Vector2Int((int)(next.x + collider[1].x), (int)(next.y + collider[1].y)),
                    new Vector2Int((int)(next.x + collider[2].x), (int)(next.y + collider[2].y)),
                    new Vector2Int((int)(next.x + collider[3].x), (int)(next.y + collider[3].y))))
                transform.position = next;
            else if (direction.y != 0 &&
                world.CanMove((int)(transform.position.x + collider[0].x), (int)(transform.position.y + collider[0].y), (int)(transform.position.x + collider[0].x), (int)(next.y + collider[0].y)) &&
                world.CanMove((int)(transform.position.x + collider[1].x), (int)(transform.position.y + collider[1].y), (int)(transform.position.x + collider[1].x), (int)(next.y + collider[1].y)) &&
                world.CanMove((int)(transform.position.x + collider[2].x), (int)(transform.position.y + collider[2].y), (int)(transform.position.x + collider[2].x), (int)(next.y + collider[2].y)) &&
                world.CanMove((int)(transform.position.x + collider[3].x), (int)(transform.position.y + collider[3].y), (int)(transform.position.x + collider[3].x), (int)(next.y + collider[3].y)) &&
                world.CanExist(
                    new Vector2Int((int)(transform.position.x + collider[0].x), (int)(next.y + collider[0].y)),
                    new Vector2Int((int)(transform.position.x + collider[1].x), (int)(next.y + collider[1].y)),
                    new Vector2Int((int)(transform.position.x + collider[2].x), (int)(next.y + collider[2].y)),
                    new Vector2Int((int)(transform.position.x + collider[3].x), (int)(next.y + collider[3].y))))
                transform.position = new Vector2(transform.position.x, next.y);
            else if (direction.x != 0 &&
                world.CanMove((int)(transform.position.x + collider[0].x), (int)(transform.position.y + collider[0].y), (int)(next.x + collider[0].x), (int)(transform.position.y + collider[0].y)) &&
                world.CanMove((int)(transform.position.x + collider[1].x), (int)(transform.position.y + collider[1].y), (int)(next.x + collider[1].x), (int)(transform.position.y + collider[1].y)) &&
                world.CanMove((int)(transform.position.x + collider[2].x), (int)(transform.position.y + collider[2].y), (int)(next.x + collider[2].x), (int)(transform.position.y + collider[2].y)) &&
                world.CanMove((int)(transform.position.x + collider[3].x), (int)(transform.position.y + collider[3].y), (int)(next.x + collider[3].x), (int)(transform.position.y + collider[3].y)) &&
                world.CanExist(
                    new Vector2Int((int)(next.x + collider[0].x), (int)(transform.position.y + collider[0].y)),
                    new Vector2Int((int)(next.x + collider[1].x), (int)(transform.position.y + collider[1].y)),
                    new Vector2Int((int)(next.x + collider[2].x), (int)(transform.position.y + collider[2].y)),
                    new Vector2Int((int)(next.x + collider[3].x), (int)(transform.position.y + collider[3].y))))
                transform.position = new Vector2(next.x, transform.position.y);
            selector.transform.position = new Vector3(Mathf.Round(transform.position.x-0.5f), Mathf.Round(transform.position.y-0.75f), 0);
        }
    }

    public void SetStartPosition(Vector3 position) {
        transform.position = position;
        selector.transform.position = new Vector3(Mathf.Round(transform.position.x - 0.5f), Mathf.Round(transform.position.y - 0.75f), 0);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)collider[0]);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)collider[1]);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)collider[2]);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)collider[3]);
        Gizmos.color = Color.red;
        Vector2 next = (Vector2)transform.position + movement.normalized * movementSpeed * Time.deltaTime;
        Gizmos.DrawLine(next, next + collider[0]);
        Gizmos.DrawLine(next, next + collider[1]);
        Gizmos.DrawLine(next, next + collider[2]);
        Gizmos.DrawLine(next, next + collider[3]);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(next, new Vector2((int)(next.x + collider[0].x), (int)(next.y + collider[0].y)));
        Gizmos.DrawLine(next, new Vector2((int)(next.x + collider[1].x), (int)(next.y + collider[1].y)));
        Gizmos.DrawLine(next, new Vector2((int)(next.x + collider[2].x), (int)(next.y + collider[2].y)));
        Gizmos.DrawLine(next, new Vector2((int)(next.x + collider[3].x), (int)(next.y + collider[3].y)));

    }
}
