using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour {

    int Electric = 100;
    RectTransform Core;

    private void Start()
    {
        Core = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void HandleElectric (int count)
    {
        int _electric = Electric + count;
        if (_electric >= 0 && _electric <= 100) Electric = _electric;
        else if (_electric < 0) Electric = 0;
        else if (_electric > 100) Electric = 100;
        Core.offsetMax = new Vector2(-(30 + (100 - Electric) * 3), -10);
    }

}
