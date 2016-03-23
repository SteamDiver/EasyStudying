using System.Windows.Forms;

public class ErrorController {

	public static void ShowError(int code)
    {
        MessageBox.Show("Ошибка "+code, "Ошибка", MessageBoxButtons.OK);
    }
    public static void ShowError(int code, string text)
    {
        MessageBox.Show("Ошибка " + code+"\n"+text, "Ошибка", MessageBoxButtons.OK);
    }
}
