using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [Header("UI")]
    [SerializeField] private Animator UIBasicAnimator;
    [SerializeField] private Animator UIEffectsAnimator;
    private const string GreenPlay = "GreenPlay";
    private const string Win = "Win";
    private const string Draw = "Draw";
    private const string Score = "Score";


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.Instance.OnWin += (Transform myTr, Transform otherTr) =>
        {
            UIBasicAnimator.Rebind();
            UIBasicAnimator.SetTrigger(Score);

            UIEffectsAnimator.Rebind();
            UIEffectsAnimator.SetTrigger(Win);
        };
        EventManager.Instance.OnDraw += (Transform myTr, Transform otherTr) =>
        {
            UIEffectsAnimator.Rebind();
            UIEffectsAnimator.SetTrigger(Draw);
        };
        EventManager.Instance.OnStateChanged += OnStateChaged;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnWin -= (Transform myTr, Transform otherTr) =>
        {
            UIBasicAnimator.Rebind();
            UIBasicAnimator.SetTrigger(Score);

            UIEffectsAnimator.Rebind();
            UIEffectsAnimator.SetTrigger(Win);
        };
        EventManager.Instance.OnDraw -= (Transform myTr, Transform otherTr) =>
        {
            UIEffectsAnimator.Rebind();
            UIEffectsAnimator.SetTrigger(Draw);
        };
        EventManager.Instance.OnStateChanged -= OnStateChaged;
    }


    private void OnStateChaged()
    {
        if (GameManager.Instance.IsGameActive)
            GameManager.Instance.PlayAfterDelay(PlayGreenAnimation);
    }

    private void PlayGreenAnimation() => UIEffectsAnimator.SetTrigger(GreenPlay);

}
