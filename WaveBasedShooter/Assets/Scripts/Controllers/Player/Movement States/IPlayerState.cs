using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void UpdateState();

    void ToIdleState();

    void ToWalkState();

    void ToRollState();
}
