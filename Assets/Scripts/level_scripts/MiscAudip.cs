using UnityEngine;

public class MiscAudip : MonoBehaviour
{
    public AudioClip leverAud;
    public AudioClip doorAud;
    public AudioClip lightAud;
    public AudioClip buttonAud;

    public AudioSource src;

    public void PlayLever()
    {
        src.Stop();
        src.PlayOneShot(leverAud);
    }
     public void PlayDoor()
    {
        src.Stop();
        src.PlayOneShot(doorAud);
    }
      public void PlayLight()
    {
        src.Stop();
        src.PlayOneShot(lightAud);
    }
      public void PlayButton()
    {
        src.Stop();
        src.PlayOneShot(buttonAud);
    }
}
