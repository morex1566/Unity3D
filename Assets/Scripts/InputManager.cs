using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InputManager : MonoBehaviourSingleton<InputManager>
{
    private static InputMappingContext inputMappingContext;
    public static InputMappingContext InputMappingContext
    {
        get => inputMappingContext;
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        GetInstance();
    }

    private void Awake()
    {
        inputMappingContext = new InputMappingContext();
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
