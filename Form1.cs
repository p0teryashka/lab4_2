using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab4_2
{
    public partial class Form1 : Form
    {
        Model model;//объ€вление модели
        public Form1()
        {
            InitializeComponent();
            model = new Model();//инициализаци€ модели
            model.observers += new System.EventHandler(this.UpdateFromModel);//подписка на обновлени€ модели
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateFromModel(this, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//метод смены значени€ numeric1
        {
            model.SetA(Decimal.ToInt32(numericUpDown1.Value));//запоминает последнее значение ј с стрелочками 
        }//конец метода

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//метод смены значени€ numeric2
        {
            model.SetB(Decimal.ToInt32(numericUpDown2.Value));//запоминает последнее значение B  с стрелочками 
        }//конец метода

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)//метод смены значени€ numeric3
        {
            model.SetC(Decimal.ToInt32(numericUpDown3.Value));//запоминает последнее значение C с стрелочками 
        }//конец метода
        private void trackBar1_Scroll(object sender, EventArgs e)//метод смены значени€ track1
        {
            model.SetA(trackBar1.Value);//запоминает последнее значение A ползунок 
        }//конец метода
        private void trackBar2_Scroll(object sender, EventArgs e)//метод смены значени€ track2
        {
            model.SetB(trackBar2.Value);//запоминает последнее значение B ползунок 
        }//конец метода
        private void trackBar3_Scroll(object sender, EventArgs e)//метод смены значени€ track3
        {
            model.SetC(trackBar3.Value);//запоминает последнее значение C ползунок 
        }//конец метода
        private void UpdateFromModel(object sender, EventArgs e)//метод обновлени€ модели
        {
            textBox1.Text = model.GetA().ToString();
            textBox2.Text = model.GetB().ToString();
            textBox3.Text = model.GetC().ToString();
            numericUpDown1.Value = model.GetA();
            numericUpDown2.Value = model.GetB();
            numericUpDown3.Value = model.GetC();
            trackBar1.Value = model.GetA();
            trackBar2.Value = model.GetB();
            trackBar3.Value = model.GetC();
        }//конец метода

        private void textBox1_KeyDown(object sender, KeyEventArgs e)//метод изменени€ значени€ tb1
        {
            if (e.KeyCode == Keys.Enter) //если ввести значение в текстбокс то значени€ помен€ютс везде 
                model.SetA(Int32.Parse(textBox1.Text));
        }//конец метода
        private void textBox2_KeyDown(object sender, KeyEventArgs e)//метод смены значени€ tb2
        {
            if (e.KeyCode == Keys.Enter)//если ввести значение в текстбокс то значени€ помен€ютс везде
                model.SetB(Int32.Parse(textBox2.Text));
        }//конец метода
        private void textBox3_KeyDown(object sender, KeyEventArgs e)//метод смены значени tb3
        {
            if (e.KeyCode == Keys.Enter)//если ввести значение в текстбокс то значени€ помен€ютс везде
                model.SetC(Int32.Parse(textBox3.Text));
        }//конец метода


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//метод сохранени€ значений A и C при закрытии формы
        {
            Properties.Settings.Default.value_A = trackBar1.Value;//сохранение ползунка 
            Properties.Settings.Default.value_C = trackBar3.Value;
            Properties.Settings.Default.Save();
        }//конец метода
    }
    class Model//модель
    {
        private int A, B, C;//объ€вление элементов модели
        public System.EventHandler observers;
        public Model()
        {
            A = Properties.Settings.Default.value_A;
            B = 0;
            C = Properties.Settings.Default.value_C;

        }
        public int GetA()//геттер дл€ ј
        {
            return A;
        }//конец метода
        public int GetB()//геттер дл€ B
        {
            return B;
        }//конец метода
        public int GetC()//геттер дл€ —
        {
            return C;
        }//конец метода
        public void SetA(int a)//сеттер дл€ ј
        {
            if (a > C)
            {
                observers.Invoke(this, null); //направление значени€ под ј (только под ј) ( неправильное значение)
                return;
            }
            if (a > B && a <= C)
                B = a;
            A = a;
            observers.Invoke(this, null);
        }//конец метода
        public void SetB(int b)//сеттер дл€ ¬
        {
            if (b > C || b < A)
            {
                observers.Invoke(this, null);
                return;
            }
            B = b;
            observers.Invoke(this, null);
        }//конец метода
        public void SetC(int c)//сеттер дл€ —
        {
            if (c < A)
            {
                observers.Invoke(this, null);
                return;
            }
            if (c < B)
                B = c;
            C = c;
            observers.Invoke(this, null);
        }//конец метода
    }
}
