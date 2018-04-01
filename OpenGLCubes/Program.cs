using System.Windows.Forms;

namespace OpenGLCubes
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageBox.Show("Przełączanie trybów - klawisze: 1, 2, 3, 4", "Info - Keyboard");
            using (var window = new Window())
            {
                window.Run(60, 60);
            }
        }
    }
}
