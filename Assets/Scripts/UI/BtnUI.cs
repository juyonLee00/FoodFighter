using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BtnUI : PopupUI
{
    enum Btns
    {
        PointBtn
    }

    enum Txts
    {
        TitleTxt,
        DescTxt,
    }

    enum GameObjs
    {
        Objs,
    }

    enum Imgs
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Btns));
        Bind<TextMeshProUGUI>(typeof(Txts));
        Bind<GameObject>(typeof(GameObjs));
        Bind<Image>(typeof(Imgs));

        //GetButton((int)Btns.PointBtn).gameObject.Bind(OnButtonClicked);

        GameObject obj = GetImage((int)Imgs.ItemIcon).gameObject;
        BindEvent(obj, (PointerEventData data) => { obj.transform.position = data.position; }, DefineUI.UIEvent.Drag);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        GetText((int)Txts.DescTxt).text = "context";
    }
}
