using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sierra.PerspectiveSwitcher2D;

public class ProtoPlayer : MonoBehaviour
{
    public float MoveSpeed;
    public PerspectiveSwitcher PerspectiveSwitcher;
    public AttackData[] Attacks;
    public bool LogAttacks = true;

    [ReadOnly]
    public bool Attacking;

    public Vector2 MoveInput { get; set; }
    public Vector3 Velocity { get; set; }

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        GetDirectionalInput();
        GetActionInput();
    }
    private void FixedUpdate()
    {
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
    private void GetActionInput()
    {
        var sLP = KeyCode.U;
        var sMP = KeyCode.I;
        var sHP = KeyCode.O;
        
        if (Input.GetKeyDown(sLP))
        {
            Debug.Log("hello");
            StartCoroutine(IE_Attack(Attacks[0]));
            _anim.SetTrigger("s.LP");
        }
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
    private IEnumerator IE_Attack(AttackData data)
    {

        Attacking = true;
        yield return new WaitForSeconds(Utility.FramesToSeconds(data.Startup));

        data.Hitbox.enabled = true;
        if (LogAttacks) Debug.Log("\"" + data.Hitbox.name + "\"");
        yield return new WaitForSeconds(Utility.FramesToSeconds(data.Active));

        data.Hitbox.enabled = true;
        yield return new WaitForSeconds(Utility.FramesToSeconds(data.Recovery));

        Attacking = false;
        _anim.SetTrigger("Stand");
    }
}
[Serializable]
public class AttackData
{
    public Collider Hitbox;
    public float Damage = 1;
    public float KnockUp = 0;
    public float KnockBack = 0;
    public int Startup = 6;
    public int Active = 1;
    public int Recovery = 8;
    public int Hitstun = 20;
    public int Hitstop = 3;
}
