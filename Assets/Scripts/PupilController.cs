using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PupilController : MonoBehaviour
{
    public bool IsOk = false;
  

    private string folder = new NavController().folder;
    public void start()
    {
        DontDestroyOnLoad(this);
    }

    public void NewPupil(GameObject PupilName)
    {
        try
        {
            if (PupilName.GetComponent<Text>().text != "")
            {              
               NavController.Pupil = folder + "/Pupils/" + PupilName.GetComponent<Text>().text + ".txt";
               IsOk = true;
            }
            else
            {
                IsOk = false;               
                ErrorController.ShowError("Поле не заполнено");
            }
        }
        catch
        {
            ErrorController.ShowError("Что-то пошло не так...");
        }
    }
    public void LoadPupil(GameObject PupilName)
    {
        try
        {
            StreamReader Pupil = new StreamReader(folder + "/Pupils/" + PupilName.GetComponent<Text>().text + ".txt");
            NavController.Pupil = folder + "/Pupils/" + PupilName.GetComponent<Text>().text + ".txt";
            IsOk = true;
            Debug.Log("Ok");
        }
        catch
        {
            ErrorController.ShowError("Такой ученик не найден");            
            IsOk = false;            
        }
    }
    
}
