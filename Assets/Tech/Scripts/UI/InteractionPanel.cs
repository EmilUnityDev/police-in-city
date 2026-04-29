using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractionPanel : AbstractInteractionPanel
{
    [SerializeField] private Button _letGoButton, _arrestButton, _shockButton;

    private void OnDestroy()
    {
        _letGoButton.onClick.RemoveAllListeners();
        _arrestButton.onClick.RemoveAllListeners();
        _shockButton.onClick.RemoveAllListeners();
    }

    public override void AssignLetGo(Action action)
    {
        AssignAction(_letGoButton, action);
    }

    public override void AssignArrest(Action action)
    {
        AssignAction(_arrestButton, action);
    }

    public override void AssignShock(Action action)
    {
        AssignAction(_shockButton, action);
    }

    private void AssignAction(Button button, Action action)
    {
        button.onClick.AddListener(() => action());
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }
}