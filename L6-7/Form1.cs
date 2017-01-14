using System;
using System.Linq;
using System.Windows.Forms;

namespace L6_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            var analyzer = new LexicAnalyzer(tbFor.Text);

            tbInfo.Text = "";
            tbLexems.Text = "";

            foreach (var info in analyzer.Info)
                tbInfo.Text += info + Environment.NewLine;

            tbLexems.Text = "Ключевые слова: " + Environment.NewLine;
            foreach (var keyword in analyzer.Keywords)
            {
                tbLexems.Text += keyword + Environment.NewLine;
            }

            tbLexems.Text += Environment.NewLine + "Идентификаторы:" + Environment.NewLine;
            foreach (var identificator in analyzer.Identificators.Distinct())
            {
                tbLexems.Text += identificator + Environment.NewLine;
            }

            tbLexems.Text += Environment.NewLine + "Константы: " + Environment.NewLine;
            foreach (var constant in analyzer.Constants)
            {
                tbLexems.Text += constant + Environment.NewLine;
            }

            tbLexems.Text += Environment.NewLine + "Операторы:" + Environment.NewLine;
            foreach (var operation in analyzer.Operations)
            {
                tbLexems.Text += operation + Environment.NewLine;
            }
        }
    }
}
