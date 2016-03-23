using UnityEngine;
using UnityEngine.UI;
using System.IO;





public class MenuController : MonoBehaviour
{

    public Dropdown ThemeDropdown;
    public Dropdown LessonDropdown;
   
    public NavController Nav;
    void Start()
    {
        




        // проверить наличие файла конфигурации, если его нет то создать со стандартными настройками
        try
        {
            StreamReader streamReader = new StreamReader("config.ini");
        }

        catch
        {
            StreamWriter sw = new StreamWriter("config.ini");
            sw.WriteLine("test");
            sw.Close();
        }

        SetThemes();
        SetLesson();




    }

    public void Panel_click(GameObject Tasks)
    {
        Nav.CanvasActive(Nav.MenuCanvas);
        Tasks.SetActive(true);
    }



    public void ButtonActive(GameObject AimButton)
    {
        AimButton.GetComponent<Button>().interactable = true;

    }

    public void NewPupil(GameObject PupilName)
    {

        PupilController Pupil = gameObject.AddComponent<PupilController>();
        Pupil.NewPupil(PupilName);
        if (Pupil.IsOk == true)
        {
            Nav.CanvasDisabled(Nav.NewPupilCanvas);
            Nav.CanvasActive(Nav.MenuCanvas);
        }

    }
    public void LoadPupil(GameObject PupilName)
    {
        PupilController Pupil = gameObject.AddComponent<PupilController>();
        Pupil.LoadPupil(PupilName);
        if (Pupil.IsOk == true)
        {
            Nav.CanvasDisabled(Nav.LoadPupilCanvas);
            Nav.CanvasActive(Nav.MenuCanvas);
        }
    }

    private void SetThemes()
    {        
        DirectoryInfo di = new DirectoryInfo(Nav.themefolder);
        DirectoryInfo[] themes = di.GetDirectories();


        foreach (DirectoryInfo theme in themes)
        {
            Debug.Log(theme.Name);
            ThemeDropdown.options.Add(new Dropdown.OptionData(theme.Name));

        }

    }
    public void SetLesson()
    {
        NavController.Lessons = Directory.GetFiles(Nav.themefolder+@"\"+ThemeDropdown.options[ThemeDropdown.value].text);
        Debug.Log(NavController.Lessons[0]);
        
        
    }
}
