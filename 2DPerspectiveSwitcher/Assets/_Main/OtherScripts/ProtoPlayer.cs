using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sierra.PerspectiveSwitcher2D;

public class ProtoPlayer : MonoBehaviour
{
    public float MoveSpeed;
    public PerspectiveSwitcher PerspectiveSwitcher;
    public Vector2 MoveInput { get; set; }
    public Vector3 Velocity { get; set; }

    private void FixedUpdate()
    {
        GetDirectionalInput();
        GetVelocity();
        UpdatePosition();
    }

    private void GetDirectionalInput()
    {
        var w = Input.GetKey(KeyCode.W);
        var a = Input.GetKey(KeyCode.A);
        var s = Input.GetKey(KeyCode.S);
        var d = Input.GetKey(KeyCode.D);
        var moveInput = Vector2.zero;

        if (!(a && d))
        {
            if (a) moveInput.x = -1;
            else if (d) moveInput.x = 1;
        }
        if (!(w && s))
        {
            if (w) moveInput.y = 1;
            else if (s) moveInput.y = -1;
        }
        MoveInput = moveInput;
    }
    private void GetVelocity()
    {
        var velocity = Vector3.zero;

        switch (PerspectiveSwitcher.CurrentPerspective)
        {
            case PerspectiveSwitcher.CubePerspective.top:
                velocity.x = MoveInput.x;
                velocity.z = MoveInput.y;
                break;
            case PerspectiveSwitcher.CubePerspective.bottom:
                break;
            case PerspectiveSwitcher.CubePerspective.left:
                velocity.z = MoveInput.x * -1;
                break;
            case PerspectiveSwitcher.CubePerspective.right:
                velocity.z = MoveInput.x;
                break;
            case PerspectiveSwitcher.CubePerspective.back:
                velocity.x = MoveInput.x;
                break;
            case PerspectiveSwitcher.CubePerspective.front:

                velocity.x = MoveInput.x * -1;
                break;
            default:
                break;
        }

        Velocity = velocity * MoveSpeed;
    }
    private void UpdatePosition()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + Velocity);
        Velocity = Vector3.zero;    
    }
}
