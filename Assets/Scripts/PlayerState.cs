using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IDisposable
{
    protected PlayerBaseController baseController;



    public PlayerState(PlayerBaseController baseController)
    {
        this.baseController = baseController;
    }

    public static T CreateState<T>(PlayerBaseController baseController) where T : PlayerState
    {
        baseController.State?.Dispose();

        return (T)Activator.CreateInstance(typeof(T), new object[] { baseController });
    }

    public virtual void FixedUpdate() { }

    public virtual void Update() { }

    public virtual void LateUpdate() { }

    public virtual void Dispose() { }
}
