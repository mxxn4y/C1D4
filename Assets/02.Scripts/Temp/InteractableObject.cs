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
            Debug.Log("�������Ʈ���콺Ŭ��");
        }
    }
    */

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene);
        Debug.Log(gameObject.name + " Ŭ��! �� �ε�: " + targetScene);
        Debug.Log("interactObj ���ε� �Լ�");
    }
}
