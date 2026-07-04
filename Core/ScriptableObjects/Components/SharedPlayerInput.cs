using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Components/PlayerInput", fileName = "SharedVal_PlayerInput_Name")]
    public class SharedInput : SharedComponent<PlayerInput>
    {

    }

    [Serializable]
    public class SharedInputReference : SharedValueReference<PlayerInput>
    {

    }
}