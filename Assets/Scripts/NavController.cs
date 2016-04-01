using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavController : MonoBehaviour
{
    public string folder = @"Data";
    public string themefolder = @"Data\Tasks";
    public static string[] Lessons;
    public static string Theme;
    public static string Pupil;

    public Canvas StartCanvas;
    public Canvas MenuCanvas;
    public Canvas NewPupilCanvas;
    public Canvas LoadPupilCanvas;



    void Start()
    {


        DontDestroyOnLoad(this);
        //MenuCanvas.gameObject.SetActive(false);
        //NewPupilCanvas.gameObject.SetActive(false);
        //StartCanvas.gameObject.SetActive(true);

    }
    public static Transform playerTransform;

    /// <summary>
    /// Эта лажа не работает
    /// </summary>
    void Awake()
    {
        if (playerTransform != null)
        {
            //Destroy();
            return;
        }

        DontDestroyOnLoad(this);
        playerTransform = transform;
    }


    public void LoadLevel(string Level)
    {
        SceneManager.LoadScene(Level);



    }


    public void CanvasActive(Canvas Canvas)
    {

        Canvas.GetComponent<Animator>();
        Canvas.gameObject.SetActive(true);


    }
    public void CanvasDisabled(Canvas Canvas)
    {
        Canvas.gameObject.SetActive(false);

    }
    public void Exit()
    {
        Application.Quit();
    }
}
