using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Activation : MonoBehaviour
{
	public bool active;
	public AudioClip bcgMusic;
	public RawImage shootTitle;
	public GameObject otherText;

	public RawImage fadeImage;

	void Start()
	{
		Color color = fadeImage.color;
		color.a = 0;
		fadeImage.color = color;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			active = true;
		}

		shootTitle.enabled = active;
		otherText.SetActive(!active);

		if (Input.GetKeyDown(KeyCode.F))
		{
			StartCoroutine(FadeIn());

			AudioSource audio = gameObject.AddComponent<AudioSource>();
			audio.clip = bcgMusic;
			audio.spatialBlend = 0f;
			audio.Play();
			Destroy(audio, bcgMusic.length);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	IEnumerator FadeIn()
	{
		float duration = 1f;
		float time = 0f;

		Color color = fadeImage.color;

		while (time < duration)
		{
			time += Time.deltaTime;
			color.a = Mathf.Lerp(0f, 1f, time / duration);
			fadeImage.color = color;
			yield return null;
		}

		color.a = 1f;
		fadeImage.color = color;
	}
}