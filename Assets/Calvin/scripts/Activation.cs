using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Media;

public class Activation : MonoBehaviour
{
    // most of all the code here is made by calvin
    public static Activation Instance;
    public bool active;
    public AudioClip bcgMusic;
    public RawImage shootTitle;
    public GameObject otherText;
    public bool debug = false;
    public RawImage fadeImage;
    public GameObject waisedsoundeffect;

    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
        }
    }
    void Start()
    {
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
    }

    void Update()
    {
        if (debug)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(active == false)
                {

                 active = true;
                }
                else
                {
                    active = false;
                }
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
                //Destroy(audio, bcgMusic.length);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void ShootMesage(bool active)
    {
        shootTitle.enabled = active;
        otherText.SetActive(!active);

    }
    public void waisted(Vector3 pos)
    {

        StartCoroutine(FadeIn());

        GameObject audio = Instantiate(waisedsoundeffect);
        //AudioSource audio = gameObject.AddComponent<AudioSource>();
        //audio.clip = bcgMusic;
        //audio.spatialBlend = 0f;
        //audio.Play();
        Destroy(audio, bcgMusic.length);
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