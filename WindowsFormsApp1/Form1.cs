using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        int n =3; 
        List<int> C1 = new List<int>(); 
        List<int> C2 = new List<int>(); 
        List<int> C3 = new List<int>(); 
        int activeRing;
        int compMove = 0;
        List<int> HanoiList = new List<int>();


        public Form1()
        {
            InitializeComponent();
            Restart();

        }

        public void AddRing(List<int> LTo)
        {
            if (LTo.Count == 0)
            {
                LTo.Add(activeRing);
                activeRing = 0;
                lblMsg.Text = "";
            }
            else
            {
                if (LTo[LTo.Count - 1] > activeRing)
                {
                    LTo.Add(activeRing);
                    activeRing = 0;
                    lblMsg.Text = "";
                }
                else
                {
                    MessageBox.Show("нельзя");
                }
            }           
        } 

        public void TakeRing(List<int> L)
        {
            activeRing = L[L.Count-1];
            L.RemoveAt(L.Count-1);
            lblMsg.Text = "Вы сняли кольцо " + activeRing;
        } 

        public void Draw() 
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            for (int i = C1.Count-1; i>=0;i--)
            { 
                label1.Text = label1.Text + C1[i];
            }
            for (int i = C2.Count - 1; i >= 0; i--)
            {
                label2.Text = label2.Text + C2[i];
            }
            for (int i = C3.Count - 1; i >= 0; i--)
            {
                label3.Text = label3.Text + C3[i];
            }
            if (activeRing == 0)
            {
                BtnTextChange("снять");
            }
            else
            {
                BtnTextChange("надеть");
            }

            label1.Refresh(); //Добавил чтобы обнавлялось значение башен
            label2.Refresh();
            label3.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (activeRing == 0)
            {
                TakeRing(C1);
                Draw();
            }
            else
            {
                AddRing(C1);
                Draw();
            }
            CheckWin(C2);
            CheckWin(C3);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (activeRing == 0)
            {
                TakeRing(C2);
                Draw();
            }
            else
            {
                AddRing(C2);
                Draw();
            }
            CheckWin(C2);
            CheckWin(C3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (activeRing == 0)
            {
                TakeRing(C3);
                Draw();
            }
            else
            {
                AddRing(C3);
                Draw();
            }
            CheckWin(C2);
            CheckWin(C3);
        }

        public void CheckWin(List<int> L)
        {
            bool f=false; //true - победа
            if (L.Count == n)
            { 
                f=true;
                for(int i = 0;i>=n-1;i++)
                { 
                    if (L[i+1] > L[i])
                    {
                        f = false;
                        break;
                    }
                }
            }
            if (f == true)
            {
                Win();
            }
        } 

        private void Win()
        {
            MessageBox.Show("Победа! Повторим?");
            Restart();
        } 

        public void Restart()
        {
            compMove = 0;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            C1.Clear();
            C2.Clear();
            C3.Clear();
            HanoiList.Clear();
            for (int i = n; i > 0; i--)
            {
                C1.Add(i);
            }
            Draw();
        } 

        public void BtnTextChange(string c)
        {
            button1.Text = c.ToString();
            button2.Text = c.ToString();
            button3.Text = c.ToString();
        }
        public void Comp(int n, int i, int k)
        {
            if (n == 1)
            {
                HanoiList.Add(i);
                HanoiList.Add(k);
            }
            else
            {
                int tmp = 6 - i - k;
                Comp(n - 1, i, tmp);
                HanoiList.Add(i);
                HanoiList.Add(k);
                Comp(n - 1, tmp, k);
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Thread.Sleep(200); // таймаут но по другому

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
           
            if (compMove == 0)
            {
                Comp(n, 1, 2);
                compMove = 1;
                button4_Click(sender, e); // вызывается тут а не по таймеру
            }
            else
            {
                switch (HanoiList[compMove - 1])
                {
                    case 1:
                        button1_Click(sender, e);
                        break;
                    case 2:
                        button2_Click(sender, e);
                        break;
                    case 3:
                        button3_Click(sender, e);
                        break;
                }
               
                compMove++;
                button4_Click(sender, e);  // вызывается тут а не по таймеру
            }
        }
    }
}
