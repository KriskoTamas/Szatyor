﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(1f, 1f, 1f);

    private Vector3 _initScale;

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        print("init scale: " + _initScale);
        _image.color = new Color(1f, 1f, 1f, 1f);
    }

    public override void ProcessData()
    {
        // update pos
        Vector3 newPos = _data.GetHandScreenPosition();
        newPos.z = 0;
        //newPos.y -= 100;
        transform.position = newPos;
        if (_data.IsPressing)
        {
            _image.color = clickColor;
            _image.transform.localScale = clickScale;
            return;
        }
        if (_data.IsHovering)
        {
            _image.color = hoverColor;
        }
        else
        {
            _image.color = normalColor;
        }

        _image.transform.localScale = _initScale;
    }
}
