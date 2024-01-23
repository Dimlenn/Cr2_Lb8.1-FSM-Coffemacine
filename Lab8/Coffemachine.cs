using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace Lab8
{
    public class Coffemachine
    {
        /*public async void CappuchinoMaker(Button button1, Button button2, Label label1, Form1 form)
        {
            StartIngreedientsCheck(button1, button2, label1);
            await Task.Delay(2500);

            if (CappuchinoIngreedientsCheck(Water, Grains, Milk, label1))
            {
                label1.Text = "Заваривание каппучино...";
                LoadingBarMaking(label1, form);
                await Task.Delay(6000);
            };
            await Task.Delay(3500);
            EndMaker(button1, button2, label1);
        }
        public async void AmericanoMaker(Button button1, Button button2, Label label1, Form1 form)
        {
            StartIngreedientsCheck(button1, button2, label1);
            await Task.Delay(2500);

            if (AmericanoIngreedientsCheck(Water, Grains, Milk, label1))
            {
                label1.Text = "Заваривание американо...";
                LoadingBarMaking(label1, form);
                await Task.Delay(6000);
            };

            await Task.Delay(3500);
            EndMaker(button1, button2, label1);
        }*/
        public int Water { get; set; } = 100;
        public int Grains { get; set; } = 100;
        public int Milk { get; set; } = 30;
        private int minimal = 100;
        private int minimalMilk = 50;
        /*public void ShowIngreedients()
        {
            waterBox.Text = Convert.ToString(Water);
            grainsBox.Text = Convert.ToString(Grains);
            milkBox.Text = Convert.ToString(Milk);
        }*/

        private void Hello()
        {
            Console.ReadLine();
        }
        public async void AmericanoMaker(Label label1, CoffemacineForm form)
        {
            if (AmericanoIngreedientsCheck(Water, Grains, Milk, label1))
            {
                Water -= 100;
                Grains -= 100;
                Milk -= 50;
                await Task.Delay(5000);
                label1.Text = "Заваривание американо...";
                LoadingBarMaking(label1, form);
            };
        }
        public async void CappuchinoMaker(Label label1, CoffemacineForm form)
        {
            await Task.Delay(5000);
            if (CappuchinoIngreedientsCheck(Water, Grains, Milk, label1))
            {
                Water -= 100;
                Grains -= 100;
                label1.Text = "Заваривание каппучино...";
                LoadingBarMaking(label1, form);
            }
        }
        public void StartIngreedientsCheck(Button button1, Button button2, Label label1)
        {
            button1.Visible = false;
            button2.Visible = false;
            label1.Text = "Проверка ингридиентов...";
        }
        public async void EndMaker(Button button1, Button button2, Label label1)
        {
            await Task.Delay(12000);
            button1.Visible = true;
            button2.Visible = true;
            label1.Text = "Выберите желаемый напиток";
        }
        public bool CappuchinoIngreedientsCheck(int water, int grains, int milk, Label label)
        {
            if (water >= minimal && milk >= minimalMilk && grains >= minimal)
            {
                return true;
            }

            var ingreedients = new List<string>();
            if (water < minimal)
            {
                ingreedients.Add("воды");
            }
            if (grains < minimal)
            {
                ingreedients.Add("зёрен");
            }
            if (milk < minimal)
            {
                ingreedients.Add("молока");
            }

            ShowNeedEngreedients(ingreedients, label);

            return false;
        }
        public bool AmericanoIngreedientsCheck(int water, int grains, int milk, Label label)
        {
            if (water >= minimal && grains >= minimal)
            {
                return true;
            }

            var ingreedients = new List<string>();
            if (water < minimal)
            {
                ingreedients.Add("воды");
            }
            if (grains < minimal)
            {
                ingreedients.Add("зёрен");
            }

            ShowNeedEngreedients(ingreedients, label);

            return false;
        }
        private async void ShowNeedEngreedients(List<string> ingreedients, Label label1)
        {
            string errorText = string.Join(", ", ingreedients);
            errorText = "Не хватает " + errorText;
            label1.Text = errorText;
            await Task.Delay(3500);
        }
        private async void LoadingBarMaking(Label label1, CoffemacineForm form)
        {
            PictureBox pictureBox1 = new PictureBox
            {
                Location = new Point(label1.Left - 1, label1.Top + 40),
                Size = new Size(256, 32),
                Image = Image.FromFile("C:\\Users\\n_mel\\VS Code projects\\Lab8\\LoadingBar\\Running.gif")
            };

            form.Controls.Add(pictureBox1);

            await Task.Delay(5000);

            label1.Text = "              Готово!";
            pictureBox1.Dispose();
        }
    }
}
