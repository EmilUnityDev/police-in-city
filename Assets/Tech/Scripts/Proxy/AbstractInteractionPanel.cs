using UnityEngine;
using System;

public abstract class AbstractInteractionPanel : MonoBehaviour
{
    public abstract void AssignLetGo(Action action);

    public abstract void AssignArrest(Action action);

    public abstract void AssignShock(Action action);

    public abstract void Activate();

    public abstract void Deactivate();
}