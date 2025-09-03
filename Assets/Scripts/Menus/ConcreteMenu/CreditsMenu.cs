using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public Button backButton;
    public Button settingsButton;
    public override void Init(MenuController currentContext)
    {
        base.Init(currentContext);
        state = MenuStates.Credits;

        if (backButton) backButton.onClick.AddListener(JumpBack);
        if (settingsButton) settingsButton.onClick.AddListener(() => JumpTo(MenuStates.Settings));
    }
}