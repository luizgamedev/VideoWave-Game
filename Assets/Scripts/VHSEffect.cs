﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VHSEffect : MonoBehaviour {

	public Material myMaterial;

	void Awake()
	{
		myMaterial = new Material (Shader.Find ("Custom/VHSeffect"));
		myMaterial.SetTexture ("_SecondaryTex", Resources.Load ("Textures/TVnoise")as Texture);
		myMaterial.SetFloat ("_OffsetPosY", 0f);
		myMaterial.SetFloat ("_OffsetDistortion", 480f);
		myMaterial.SetFloat ("_Intensity", 0.64f);
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//TV noise
		myMaterial.SetFloat("_OffsetNoiseX",Random.Range(0f,0.6f));
		float offsetNoise = myMaterial.GetFloat ("_OffsetNoiseY");
		myMaterial.SetFloat ("_OffsetNoiseY", offsetNoise + Random.Range (-0.03f, 0.03f));

		//Vertical shift
		float offsetPosY = myMaterial.GetFloat("_OffsetPosY");
		if (offsetPosY > 0.0f) {
			myMaterial.SetFloat ("_OffsetPosY", offsetPosY - Random.Range (0f, offsetPosY));
		} else if (offsetPosY < 0.0f) {
			myMaterial.SetFloat ("_OffsetPosY", offsetPosY + Random.Range (0f, -offsetPosY));
		} else if (Random.Range (0, 150) == 1) {
			myMaterial.SetFloat ("_OffsetPosY", Random.Range (-0.5f, 0.5f));
		}

		//Channel color shift

		//float offsetColor = myMaterial.GetFloat ("_OffsetColor");
		//if (offsetColor > 0.003f) {
		//	myMaterial.SetFloat ("_OffsetColor", Random.Range (0.003f, 0.1f));
		//} else if (Random.Range (0, 40) == 1) {
		//	myMaterial.SetFloat ("_OffsetColor", Random.Range (0.003f, 0.1f));
		//}

		//Distortion
		if (Random.Range (0, 15) == 1) {
			myMaterial.SetFloat ("_OffsetDistortion", Random.Range (1f, 480f));
		} else {
			myMaterial.SetFloat ("_OffsetDistortion", Random.Range (1f, 480f));
		}
		Graphics.Blit (source, destination, myMaterial);


	}
}
