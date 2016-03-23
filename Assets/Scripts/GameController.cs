using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows.Forms;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private string _TaskText;
    private string _TaskTitle;
    private bool _hint = false;
    private string _blocks;
    private string _CorrectOrder;
    private int _lessonnumber = 0;
    private int _statuscounter;
    private bool AllFilled = false;
    /////////// объекты конструктора задания//////////////

    public GameObject Title;
    public GameObject Text;
    public GameObject Blocktmp;
    public Transform BlocksPanel;
    public Transform FieldPanel;
    public Transform BlockSlot;
    public Transform FieldBlockSlot;
    public GameObject Status;
    public GameObject ResultsPanel;

    public string Answer;



    public Canvas DragCanvas;


    void Start()
    {

        try
        {
            ResultsPanel.SetActive(false);
            LoadTask(NavController.Lessons[_lessonnumber]);

            SetStatus(_statuscounter, NavController.Lessons.Length);

        }
        catch
        {
            ErrorController.ShowError(004, "Что-то пошло не так...");
        }

    }




    public void LoadTask(string task)
    {
        StreamReader sr = new StreamReader(task);
        if (sr != null)
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                switch (line)
                {
                    case "[Title]":
                        _TaskTitle = sr.ReadLine();
                        Title.GetComponent<Text>().text = _TaskTitle;
                        break;
                    case "[Text]":
                        _TaskText = sr.ReadLine();
                        Text.GetComponent<Text>().text = _TaskText;
                        break;
                    case "[Blocks]":
                        _blocks = sr.ReadLine();
                        string[] split = _blocks.Split(new char[] { ' ' });//разделение списка блоков по пробелу
                        foreach (string blockname in split)
                        {
                            GenerateBlock(blockname);
                            GenerateFieldSlot();
                        }
                        break;
                    case "[CorrectOrder]":
                        _CorrectOrder = sr.ReadLine();
                        break;
                }
            }
        }

    }



    public void Hint(GameObject Object)
    {
        switch (_hint)
        {
            case true:
                _hint = false;
                Object.SetActive(false);
                break;
            case false:
                _hint = true;
                Object.SetActive(true);
                break;

        }

    }

    private Transform GenerateSlot()
    {
        var Slot = Instantiate(BlockSlot);
        Slot.transform.SetParent(BlocksPanel);
        Slot.transform.localScale = new Vector3(1, 1, 1);
        return Slot;
    }

    private void GenerateBlock(string blockname)
    {
        var Block = Instantiate(Blocktmp);
        Block.transform.SetParent(GenerateSlot());
        Block.transform.localScale = new Vector3(1, 1, 1);
        Block.GetComponentInChildren<Text>().text = blockname;
        Block.GetComponentInChildren<DragHand>().myCanvas = DragCanvas;
    }

    private Transform GenerateFieldSlot()
    {
        var Slot = Instantiate(FieldBlockSlot);
        Slot.transform.SetParent(FieldPanel);
        Slot.transform.localScale = new Vector3(1, 1, 1);
        return Slot;
    }

    private bool Check()
    {
        bool right = false;
        try
        {
            foreach (Transform child in FieldPanel.transform)
            {
                Answer += child.GetChild(0).GetComponentInChildren<Text>().text;

            }
            if (Answer == _CorrectOrder && Answer != null)
            {
                right = true;
                AllFilled = true;
            }
            else
            {
                right = false;
                AllFilled = true;
            }

            Answer = null;


            return right;
        }
        catch
        {
            Answer = null;
            AllFilled = false;
            ErrorController.ShowError(005, "Не был дан ответ");
            return right = false;
        }
    }
    public void Next()
    {
        bool right = Check();
        if (right && _lessonnumber == NavController.Lessons.Length - 1)
        {
            _statuscounter += 1;
            SetStatus(_statuscounter, NavController.Lessons.Length);
            ResultsPanel.SetActive(true);
            Status.transform.SetParent(ResultsPanel.transform);
            Status.transform.position = new Vector3(0, 0, 0);

        }

        if (right && _lessonnumber < NavController.Lessons.Length - 1)
        {

            DestroySlots();


            _lessonnumber += 1;
            _statuscounter += 1;
            LoadTask(NavController.Lessons[_lessonnumber]);
            SetStatus(_statuscounter, NavController.Lessons.Length);

        }
        if (!right && _lessonnumber < NavController.Lessons.Length - 1 && AllFilled) {
            DestroySlots();            
            LoadTask(NavController.Lessons[_lessonnumber+=1]);

        }


    }
    private void DestroySlots()
    {
        foreach (Transform fieldslot in FieldPanel.transform)
        {
            DestroyObject(fieldslot.gameObject);

        }

        foreach (Transform blockslot in BlocksPanel.transform)
        {
            DestroyObject(blockslot.gameObject);

        }
    }
    private string SetStatus(int x, int y)
    {
        return Status.GetComponent<Text>().text = "Верно " + x + " из " + y;
    }

    public void Back()
    {
        
        SceneManager.LoadScene(0);
        //DestroyObject(NavController);
        
    }

}

