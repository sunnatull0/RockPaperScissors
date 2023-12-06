using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private RectTransform menuTransform;

    private readonly float defaultPosition = 0f;
    private readonly float leftPosition = -1920f;
    private readonly float rightPosition = 1920f;
    private readonly float tweenDuration = 1f;

    private bool isPressable = true;

    public void OnSettingsPressed()
    {
        MoveTo(leftPosition, tweenDuration);
    }

    private void Update()
    {
        Debug.Log(menuTransform.rect.width);
    }
    public void OnBackButtonPressed()
    {
        MoveTo(defaultPosition, tweenDuration);
    }

    public void OnLeaderBoardButtonPressed()
    {
        MoveTo(rightPosition, tweenDuration);
    }

    private void MoveTo(float position, float duration)
    {
        if (!isPressable)
            return;

        isPressable = false;
        menuTransform.DOAnchorPosX(position, duration).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            isPressable = true;
        });
    }
}
