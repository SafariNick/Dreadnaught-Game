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
            if (menuStack.Count > 0)
            {
                if (menuStack.Contains(newState))
                {
                    menuStack.Pop(); //remove the current instance of this state if it exists to avoid duplicates in the stack
                    return;
                }

            }
            menuStack.Push(newState);
        }
    }
}