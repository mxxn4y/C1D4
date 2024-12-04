using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetScene;

    void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene);
        Debug.Log(gameObject.name + " Å¬¸¯! ¾À ·Îµå: " + targetScene);
    }
}
