using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : BaseUI
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}
