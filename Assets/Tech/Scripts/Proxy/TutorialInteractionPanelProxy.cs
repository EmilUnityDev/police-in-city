using UnityEngine;
using System;

public class TutorialInteractionPanelProxy : AbstractInteractionPanel
{
    [SerializeField] private InteractionPanel _realInteractionPanel;
    [SerializeField] private GameObject _fingerButtonCanvas;

    public override void AssignLetGo(Action action)
    {
        _realInteractionPanel.AssignLetGo(action);
    }

    public override void AssignArrest(Action action)
    {
        _realInteractionPanel.AssignArrest(action);
    }

    public override void AssignShock(Action action)
    {
        _realInteractionPanel.AssignShock(action);
    }

    public override void Activate()
    {
        _realInteractionPanel.Activate();
        _fingerButtonCanvas.SetActive(true);
    }

    public override void Deactivate()
    {
        _realInteractionPanel.Deactivate();
        _fingerButtonCanvas.SetActive(false);
    }
}