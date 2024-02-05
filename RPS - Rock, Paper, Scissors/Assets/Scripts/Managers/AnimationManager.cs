using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager Instance;

    #region Variables.
    // Animators.
    [Header("UI")]
    [SerializeField] private Animator UIBasicAnimator;
    [SerializeField] private Animator UIEffectsAnimator;

    // Parameter names.
    private const string GreenPlay = "GreenPlay";
    private const string Win = "Win";
    private const string Draw = "Draw";
    private const string Score = "Score";
    #endregion


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        EventManager.Instance.OnWin += PlayWinAnimations;
        EventManager.Instance.OnDraw += PlayDrawAnimations;
        EventManager.Instance.OnStateChanged += OnStateChaged;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnWin -= PlayWinAnimations;
        EventManager.Instance.OnDraw -= PlayDrawAnimations;
        EventManager.Instance.OnStateChanged -= OnStateChaged;
    }


    private void PlayWinAnimations(Transform myTr, Transform otherTr)
    {
        UIBasicAnimator.Rebind();
        UIBasicAnimator.SetTrigger(Score);

        UIEffectsAnimator.Rebind();
        UIEffectsAnimator.SetTrigger(Win);
    }

    private void PlayDrawAnimations(Transform myTr, Transform otherTr)
    {
        UIEffectsAnimator.Rebind();
        UIEffectsAnimator.SetTrigger(Draw);
    }

    private void OnStateChaged()
    {
        if (GameManager.Instance.IsGameActive)
            GameManager.Instance.PlayAfterDelay(PlayGreenAnimation);
    }

    private void PlayGreenAnimation() => UIEffectsAnimator.SetTrigger(GreenPlay);

}
