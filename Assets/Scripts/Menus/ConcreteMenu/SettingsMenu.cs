using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public Button backButton;
    public Button creditsButton;
    public override void Init(MenuController currentContext)
    {
        base.Init(currentContext);
        state = MenuStates.Settings;

        if (backButton) backButton.onClick.AddListener(JumpBack);
        if (creditsButton) creditsButton.onClick.AddListener(() => JumpTo(MenuStates.Credits));
    }
}