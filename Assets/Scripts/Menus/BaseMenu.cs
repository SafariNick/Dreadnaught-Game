using UnityEngine;

public enum MenuStates
{
    MainMenu, Settings, Credits, Pause
}

public class BaseMenu : MonoBehaviour
{
    [HideInInspector]
    public MenuStates state;
    protected MenuController context;

    public virtual void Init(MenuController currentContext) => context = currentContext;
    public virtual void Enter() { }
    public virtual void Exit() { }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void JumpBack() => context.JumpBack();
    public void JumpTo(MenuStates newState) => context.JumpTo(newState);
}