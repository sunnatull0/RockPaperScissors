using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    #region Variables.
    [SerializeField] private Animator animator;
    
    // Parameter names.
    private const string toGame = "toGame";
    private const string toMenu = "toMenu";
    #endregion


    public void ToGame() => animator.SetTrigger(toGame);

    public void ToMenu() => animator.SetTrigger(toMenu);

    public void LoadScene(int index) => SceneManager.LoadScene(index);


}
