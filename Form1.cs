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
        Model model;//���������� ������
        public Form1()
        {
            InitializeComponent();
            model = new Model();//������������� ������
            model.observers += new System.EventHandler(this.UpdateFromModel);//�������� �� ���������� ������
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateFromModel(this, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)//����� ����� �������� numeric1
        {
            model.SetA(Decimal.ToInt32(numericUpDown1.Value));//���������� ��������� �������� � � ����������� 
        }//����� ������

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)//����� ����� �������� numeric2
        {
            model.SetB(Decimal.ToInt32(numericUpDown2.Value));//���������� ��������� �������� B  � ����������� 
        }//����� ������

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)//����� ����� �������� numeric3
        {
            model.SetC(Decimal.ToInt32(numericUpDown3.Value));//���������� ��������� �������� C � ����������� 
        }//����� ������
        private void trackBar1_Scroll(object sender, EventArgs e)//����� ����� �������� track1
        {
            model.SetA(trackBar1.Value);//���������� ��������� �������� A �������� 
        }//����� ������
        private void trackBar2_Scroll(object sender, EventArgs e)//����� ����� �������� track2
        {
            model.SetB(trackBar2.Value);//���������� ��������� �������� B �������� 
        }//����� ������
        private void trackBar3_Scroll(object sender, EventArgs e)//����� ����� �������� track3
        {
            model.SetC(trackBar3.Value);//���������� ��������� �������� C �������� 
        }//����� ������
        private void UpdateFromModel(object sender, EventArgs e)//����� ���������� ������
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
        }//����� ������

        private void textBox1_KeyDown(object sender, KeyEventArgs e)//����� ��������� �������� tb1
        {
            if (e.KeyCode == Keys.Enter) //���� ������ �������� � ��������� �� �������� ��������� ����� 
                model.SetA(Int32.Parse(textBox1.Text));
        }//����� ������
        private void textBox2_KeyDown(object sender, KeyEventArgs e)//����� ����� �������� tb2
        {
            if (e.KeyCode == Keys.Enter)//���� ������ �������� � ��������� �� �������� ��������� �����
                model.SetB(Int32.Parse(textBox2.Text));
        }//����� ������
        private void textBox3_KeyDown(object sender, KeyEventArgs e)//����� ����� ������� tb3
        {
            if (e.KeyCode == Keys.Enter)//���� ������ �������� � ��������� �� �������� ��������� �����
                model.SetC(Int32.Parse(textBox3.Text));
        }//����� ������


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//����� ���������� �������� A � C ��� �������� �����
        {
            Properties.Settings.Default.value_A = trackBar1.Value;//���������� �������� 
            Properties.Settings.Default.value_C = trackBar3.Value;
            Properties.Settings.Default.Save();
        }//����� ������
    }
    class Model//������
    {
        private int A, B, C;//���������� ��������� ������
        public System.EventHandler observers;
        public Model()
        {
            A = Properties.Settings.Default.value_A;
            B = 0;
            C = Properties.Settings.Default.value_C;

        }
        public int GetA()//������ ��� �
        {
            return A;
        }//����� ������
        public int GetB()//������ ��� B
        {
            return B;
        }//����� ������
        public int GetC()//������ ��� �
        {
            return C;
        }//����� ������
        public void SetA(int a)//������ ��� �
        {
            if (a > C)
            {
                observers.Invoke(this, null); //����������� �������� ��� � (������ ��� �) ( ������������ ��������)
                return;
            }
            if (a > B && a <= C)
                B = a;
            A = a;
            observers.Invoke(this, null);
        }//����� ������
        public void SetB(int b)//������ ��� �
        {
            if (b > C || b < A)
            {
                observers.Invoke(this, null);
                return;
            }
            B = b;
            observers.Invoke(this, null);
        }//����� ������
        public void SetC(int c)//������ ��� �
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
        }//����� ������
    }
}
