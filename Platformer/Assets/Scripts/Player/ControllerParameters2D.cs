using UnityEngine;
using System;
[Serializable]

public class ControllerParameters2D
{
    public enum JumpBehavior
    {
        CanJumpOnGround,
        CanJumpAnywhere,
        CantJump
    } 
    public Vector2 MaxVelocity = new Vector2 (float.MaxValue, float.MaxValue);
    [Range(0,90)]
    public float SlopeLimit = 30;
    public float Gravity = -40f;
    public JumpBehavior JumpRestricitions;

    public float JumpMagnitude = 23;
    public float JumpFrequency = .25f;
} 