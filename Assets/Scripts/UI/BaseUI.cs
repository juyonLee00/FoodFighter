using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;

public abstract class BaseUI : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    //type : 오브젝트 이름에 따라 구별한 타입.
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = UtilUI.FindChild(gameObject, names[i], true);
            else
                objects[i] = UtilUI.FindChild<T>(gameObject, names[i], true);

            if(objects[i] == null)
                    Debug.Log($"Failed to bind({names[i]})");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    public static void BindEvent(GameObject obj, Action<PointerEventData> action, DefineUI.UIEvent type = DefineUI.UIEvent.Click)
    {
        UIEventHandler handler = UtilUI.GetOrAddComponent<UIEventHandler>(obj);

        switch(type)
        {
            case DefineUI.UIEvent.Click:
                handler.OnClickHandler -= action;
                handler.OnClickHandler += action;
                break;
            case DefineUI.UIEvent.Drag:
                handler.OnDragHandler -= action;
                handler.OnDragHandler += action;
                break;
        }
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx);}
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

}
