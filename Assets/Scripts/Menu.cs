using UnityEngine;
using System.Collections;


public class Menu : MonoBehaviour
{
    public GameObject slider;
    public float idif = 0;

    void Start()
    {
        onStart();
    }

  public void onButtonStartPress ()
    {
        StartCoroutine(showSlider());
    }

  public void onButtonExitPress()
  {
      Application.CancelQuit();
  }

  private void onStart()
  {
      slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
  }

  IEnumerator showSlider()
  {
      for (int i = 0; i < 20; i++)
      {
          idif += 0.05f;
          slider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, idif);
          yield return new WaitForSeconds(0.025f);
      }
      Application.LoadLevel("Game");
      yield break;
  }
}
