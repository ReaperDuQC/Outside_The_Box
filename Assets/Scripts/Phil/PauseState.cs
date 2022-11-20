using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : BaseState
{
    public override void Enter()
    {
        // freeze movemetn of stuff in minigame and controlle
        // pause clock
    }
    public override void Update()
    { }
    public override void Exit()
    {
        // unfreeze unpause
    }
}
