using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDot : MonoBehaviour {

	LineDotObjectPool m_lineObjectPool = null;

	public void SetObjectPool(LineDotObjectPool _objPool)
	{
		m_lineObjectPool = _objPool;
	}

	public void SetLineActive(bool _active)
	{
		gameObject.SetActive(_active);
	}

    void Update()
    {
        if(transform.position.x < CameraBehaviour.Instance.m_cameraLeftReference.transform.position.x)
        {
            m_lineObjectPool.DeactivateObject(gameObject);
        }
    }

}
