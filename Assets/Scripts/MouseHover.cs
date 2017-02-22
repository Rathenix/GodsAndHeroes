using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour {

    public Color ActiveColor;
    public Color InactiveColor;
    public GameObject Pointer;

    public bool Selectable;
    public int DestinationSceneId;

    private AudioSource Audio;
    public AudioClip HoverSound;
    public AudioClip SelectedSound;
    public AudioClip UnselectableSound;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        GetComponent<Renderer>().material.color = InactiveColor;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = ActiveColor;
        Pointer.transform.position = new Vector3(transform.position.x - (float)0.6, transform.position.y - (float)0.3);
        Audio.PlayOneShot(HoverSound);
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = InactiveColor;
    }

    IEnumerator LoadScene()
    {
        if (!Selectable)
        {
            Audio.PlayOneShot(UnselectableSound);
        }
        else
        {
            Audio.PlayOneShot(SelectedSound);
            yield return new WaitForSeconds(SelectedSound.length - 1);
            if (DestinationSceneId != -1)
            {
                SceneManager.LoadScene(DestinationSceneId);
            }
        }
    }

    private void OnMouseUp()
    {
        StartCoroutine("LoadScene");
    }
}
