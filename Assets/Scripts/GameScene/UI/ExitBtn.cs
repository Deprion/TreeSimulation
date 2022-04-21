using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener( delegate 
        {
            GameManager.inst.Terminate();
            SceneManager.LoadScene("MenuScene");
        });
    }
}
