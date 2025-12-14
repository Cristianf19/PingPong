using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    public GameObject[] windows;

    public void enabledWindow(int idWindow)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (i != idWindow)
            {
                windows[i].SetActive(false);
            }
        }
        windows[idWindow].SetActive(true);
    }
}
