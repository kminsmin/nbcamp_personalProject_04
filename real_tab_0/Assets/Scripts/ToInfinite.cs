using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToInfinite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToInfiniteRun()
    {
        SceneManager.LoadScene("InfiniteScene");
    }

    public void ToTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }


    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
