using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.States
{
    //Base State Manager interface that must be inhereted from any state manager script to give it the standard behaviour.
    //The behaviour of the order/moments of change of the behaviour will be defined on the child manager.
    public interface IStateManager 
    {
        public void SwitchState(IStateBase state);

        public IStateBase GetState();
    }
}