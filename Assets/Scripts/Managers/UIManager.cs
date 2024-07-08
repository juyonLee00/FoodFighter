using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int _order = 5;

    //팝업 캔버스의 UI 담음 
    Stack<PopupUI> _popupStack = new Stack<PopupUI>();
    SceneUI sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@RootUI");
            if (root == null)
                root = new GameObject { name = "@RootUI" };

            return root;
        }
    }

    public void SetCanvas(GameObject obj, bool sort= true)
    {
        Canvas canvas = UtilUI.GetOrAddComponent <Canvas>(obj);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        //중복 캔버스 
        canvas.overrideSorting = true;


        if(sort)
        {
            canvas.sortingOrder = _order;
            _order += 1;
        }

        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject obj = Managers.Resource.Instantiate($"UI/Popup/(name)");
        T popup = UtilUI.GetOrAddComponent <T> (obj);

        obj.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(PopupUI popup)
    {
        if (_popupStack.Count == 0)
            return;

        //스택의 가장 위의 부
        if (_popupStack.Peek() != popup)
        {
            //Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();

    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        PopupUI popupUI = _popupStack.Pop();
        Managers.Resource.Destroy(popupUI.gameObject);
        popupUI = null;
        _order -= 1;

    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

}
