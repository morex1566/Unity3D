using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InputManager : MonoBehaviourSingleton<InputManager>
{
    private static PlayerInputMappingContext inputMappingContext;
    public static PlayerInputMappingContext InputMappingContext
    {
        get => inputMappingContext;
        set => inputMappingContext = value;
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        GetInstance();
    }

    private void Awake()
    {
        inputMappingContext = new PlayerInputMappingContext();
    }

    private void OnEnable()
    {
        inputMappingContext.Enable();
    }

    private void OnDisable()
    {
        inputMappingContext.Disable();
    }
}
