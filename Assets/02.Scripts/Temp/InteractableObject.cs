using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetScene;

    /*
    void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            LoadTargetScene();
            Debug.Log("룸오브젝트마우스클릭");
        }
    }
    */

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene);
        Debug.Log(gameObject.name + " 클릭! 씬 로드: " + targetScene);
        Debug.Log("interactObj 씬로드 함수");
    }
}
