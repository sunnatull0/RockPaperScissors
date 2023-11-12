using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const string toBlack = "toBlack";

    public void LoadScene()
    {
        animator.SetTrigger(toBlack);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
