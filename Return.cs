using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
