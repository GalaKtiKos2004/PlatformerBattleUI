using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    public float MoveInput {  get; private set; }
    public bool JumpInput { get; private set; }
    public bool IsAttack {  get; private set; }

    private void Awake()
    {
        MoveInput = 0;
        JumpInput = false;
        IsAttack = false;
    }

    private void Update()
    {
        MoveInput = Input.GetAxisRaw(Horizontal);
        JumpInput =  Convert.ToBoolean(Input.GetAxisRaw(Jump));
        IsAttack = Input.GetKeyDown(KeyCode.Z);
    }
}
