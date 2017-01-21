using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDot : MonoBehaviour {

	LineDotObjectPool m_lineObjectPool = null;

    public float m_XThreshhold = 100f;

    private Camera m_mainCamera;

	public void SetObjectPool(LineDotObjectPool _objPool)
	{
		m_lineObjectPool = _objPool;
        m_mainCamera = Camera.main;
	}

	public void SetLineActive(bool _active)
	{
		gameObject.SetActive(_active);
	}

    void Update()
    {

    }

}
