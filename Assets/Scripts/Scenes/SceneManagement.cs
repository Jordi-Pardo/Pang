using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
