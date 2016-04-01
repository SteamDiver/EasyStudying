using System.Windows.Forms;

public class ErrorController {

	
    public static void ShowError(string text)
    {
        MessageBox.Show("Ошибка! \n"+text, "Ошибка", MessageBoxButtons.OK);
    }
}
