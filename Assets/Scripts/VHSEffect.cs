using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VHSEffect : MonoBehaviour {

	public Material myMaterial;
	public bool isPaused = false;

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
		if (!isPaused) {
			myMaterial.SetFloat ("_OffsetNoiseX", Random.Range (0f, 0.6f));
			float offsetNoise = myMaterial.GetFloat ("_OffsetNoiseY");
			myMaterial.SetFloat ("_OffsetNoiseY", offsetNoise + Random.Range (-0.03f, 0.03f));
		} else {
			myMaterial.SetFloat ("_OffsetNoiseX", 0);
		}

		//Vertical shift
		if (!isPaused) {
			float offsetPosY = myMaterial.GetFloat ("_OffsetPosY");
			if (offsetPosY > 0.0f) {
				myMaterial.SetFloat ("_OffsetPosY", offsetPosY - Random.Range (0f, offsetPosY));
			} else if (offsetPosY < 0.0f) {
				myMaterial.SetFloat ("_OffsetPosY", offsetPosY + Random.Range (0f, -offsetPosY));
			} else if (Random.Range (0, 150) == 1) {
				myMaterial.SetFloat ("_OffsetPosY", Random.Range (-0.5f, 0.5f));
			}
		} else {
			myMaterial.SetFloat ("_OffsetPosY", 0);
		}

		//Channel color shift
		if (isPaused) {
			float offsetColor = myMaterial.GetFloat ("_OffsetColor");
			if (offsetColor > 0.003f) {
				myMaterial.SetFloat ("_OffsetColor", Random.Range (0.003f, 0.1f));
			} else if (Random.Range (0, 40) == 1) {
				myMaterial.SetFloat ("_OffsetColor", Random.Range (0.003f, 0.005f));
			}
		} else {
			myMaterial.SetFloat ("_OffsetColor", 0);
		}



		//Distortion
		if (!isPaused) {
			if (Random.Range (0, 15) == 1) {
				myMaterial.SetFloat ("_OffsetDistortion", Random.Range (1f, 480f));
			} else {
				myMaterial.SetFloat ("_OffsetDistortion", Random.Range (1f, 480f));
			}
			Graphics.Blit (source, destination, myMaterial);
		} else {
			myMaterial.SetFloat ("_OffsetDistortion", 0);
		}


	}

}
