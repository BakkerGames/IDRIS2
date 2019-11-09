// TestScreen.cs - 12/31/2018

using System.Windows.Forms;

namespace Test.IDRIS.Runtime
{
    public partial class TestScreen : Form
    {
        public TestScreen()
        {
            InitializeComponent();
        }

        private void TestScreen_KeyDown(object sender, KeyEventArgs e)
        {
            int k = e.KeyValue;
        }
    }
}
