using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCharacter : MonoBehaviour
{
    public enum PlayerState
    {
        stageStart,
        stageMiddle,
        stageEnd
    }
    [SerializeField] public PlayerState State;
    private void OnTriggerEnter(Collider other) {
        
    }
}