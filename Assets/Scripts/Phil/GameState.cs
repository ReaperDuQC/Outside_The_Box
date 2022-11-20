using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : BaseState
{
    public override void Enter()
    {
        // spawn minigame
        // start minigame
    }
    public override void Update()
    {
        // check minigame progression
        // if touhts completed or cliche 100% 
              // change state to chapter end
    }
    public override void Exit()
    {
        // give score to scoremanager
        // destroye minigame
    }
}
