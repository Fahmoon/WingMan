using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    internal CharacterManipulatorScript manipulator;
    internal CharacterStateHandler stateHandler;
    internal FreefallControls freefallControls;
    internal ParachuteControls parachuteControls;
    internal Vector3 originalPosition;
    internal Quaternion originalRotation;
    internal Vector3 myCameraOriginalPosition;
    public static CharacterManager instance;
    private void Awake()
    {
        instance = this;
        myCameraOriginalPosition = Camera.main.transform.position;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        manipulator = GetComponentInChildren<CharacterManipulatorScript>();
        stateHandler = GetComponentInChildren<CharacterStateHandler>();
        freefallControls = GetComponentInChildren<FreefallControls>();
        parachuteControls = GetComponentInChildren<ParachuteControls>();
    }

}
