using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus;

    public MenuStates initState = MenuStates.MainMenu;

    public BaseMenu currentMenu => _currentMenu;
    private BaseMenu _currentMenu;

    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    Stack<MenuStates> menuStack = new Stack<MenuStates>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (allMenus.Length <= 0)
        {
            allMenus = GetComponentsInChildren<BaseMenu>(true);
        }

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;
            menu.Init(this);

            if (menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        JumpTo(initState);
    }

    public void JumpBack()
    {
        //we should probably log an error here because this should never happen
        if (menuStack.Count <= 0) return;

        menuStack.Pop();
        JumpTo(menuStack.Peek(), true);

    }

    public void JumpTo(MenuStates newState, bool fromJumpBack = false)
    {
        if (!menuDictionary.ContainsKey(newState))
        {
            Debug.LogError($"No menu found for state {newState}");
            return;
        }

        //if we are already in the menu we want to go to, do nothing
        if (_currentMenu == menuDictionary[newState]) return;

        if (_currentMenu != null)
        {
            _currentMenu.Exit();
            _currentMenu.gameObject.SetActive(false);
        }

        _currentMenu = menuDictionary[newState];
        _currentMenu.gameObject.SetActive(true);
        _currentMenu.Enter();

        if (!fromJumpBack)
        {
            if (menuStack.Count > 0 && menuStack.Contains(newState))
            {
                List<MenuStates> oldStates = new List<MenuStates>();
                //remove everything above the new state
                while (menuStack.Peek() != newState)
                {
                    oldStates.Add(menuStack.Pop());
                }

                //pop the new state as we need to re-add it to the top of the stack
                menuStack.Pop();

                //we need to re-add the old states back to the stack
                for (int i = oldStates.Count - 1; i >= 0; i--)
                {
                    menuStack.Push(oldStates[i]);
                }

                menuStack.Push(newState);
                //we don't need to push the new state because it is already on the stack
                return;
            }
            menuStack.Push(newState);
        }
    }
}