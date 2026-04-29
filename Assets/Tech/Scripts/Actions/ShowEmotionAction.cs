using System;

public class ShowEmotionAction : IAction
{
    private Action _showEmotion;

    public ShowEmotionAction(Action showEmotion)
    {
        _showEmotion = showEmotion;
    }

    public void Do()
    {
        if (_showEmotion != null)
            _showEmotion();
    }
}