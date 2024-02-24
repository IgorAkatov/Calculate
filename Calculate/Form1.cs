using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Calculate
{
    public partial class My_Program : Form
    {
        bool divide_by_zero_trigger = false;
        bool one_comma_for_one_number = true;
        bool new_example = false;
        byte example_count = 0;
        

        public My_Program()
        {
            InitializeComponent();
           
        }


        // методика расчёта 
        string Calculate_double(string example)
        {
            double sum = 0;
            char op = example[0] == '-' ? '-' : '+';


            string[] s = example.Split(new char[] { '+', '-', '*', '/' });
            s = s.Where(x => x != "").ToArray();
            double[] d = Array.ConvertAll(s, a => double.Parse(a));
            Stack<double> stack = new Stack<double>();
            int count = 0;

            int i = op == '-' ? 1 : 0;
            for (; i < example.Length; i++)
            {
                if (example[i] == '+' || example[i] == '-' || example[i] == '*' || example[i] == '/' || i == example.Length - 1)
                {
                    switch (op)
                    {
                        case '+':
                            stack.Push(d[count]);
                            break;
                        case '-':
                            stack.Push(-d[count]);
                            break;
                        case '*':
                            stack.Push(stack.Pop() * d[count]);
                            break;
                        case '/':
                            if (d[count] == 0 && new_example)
                            {
                                divide_by_zero_trigger = true;
                                DividebyZero();
                                return example;
                            }
                            else
                                stack.Push(stack.Pop() / d[count]);



                            break;

                    }
                    count++;
                    op = example[i];

                }



            }
            while (stack.Count > 0)
            {
                sum += stack.Pop();
            }
            if (!Double.IsInfinity(sum) && !new_example)
                return Math.Round(sum, 3).ToString();
            else if (!new_example)
                return "Нельзя делить на ноль";
            else if(!Double.IsInfinity(sum))
                return Math.Round(sum, 3).ToString();   
            else return example;
        }
        void NO_One_More_Zero_Before_Comma()
        {
            List<char> operators = new List<char> { '+', '-', '/', '*' };
            List<char> test_operator = new List<char>();
            if (label_main_okno.Text.Length >= 2)
            {
                test_operator = operators.Where(x => x == label_main_okno.Text[label_main_okno.Text.Length - 2]).ToList();
            }

            if (label_main_okno.Text != "" && one_comma_for_one_number && label_main_okno.Text.Last() == '0' && test_operator.Any())
            {
                return;
            }
            else if (label_main_okno.Text != "" && label_main_okno.Text.Last() == '0' && label_main_okno.Text.Length == 1)
            {
                return;
            }
            else
                Button0_9("0");

        }
        void DividebyZero()
        {
            if (divide_by_zero_trigger)
            {
                button0.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button_dot_comma.Enabled = false;
                button_plus.Enabled = false;
                button_minus.Enabled = false;
                button_divide.Enabled = false;
                button_multiply.Enabled = false;

            }
            else
            {
                button0.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button_dot_comma.Enabled = true;
                button_plus.Enabled = true;
                button_minus.Enabled = true;
                button_divide.Enabled = true;
                button_multiply.Enabled = true;

            }
        }
        

        //Цифры 0-9
        void Button0_9(string str)
        {
            if (example_count < 18)
            {

                if (new_example)
                {
                    label_main_okno.Text = "";
                    new_example = false;
                }

                label_main_okno.Text += str;
                example_count++;
                label1.Text = label_main_okno.Text == string.Empty ? "" : Calculate_double(label_main_okno.Text);
                
                label_focus.Focus();
            }
        }
        private void Button0_Click(object sender, EventArgs e)
        {
         //   Button0_9("0");
            NO_One_More_Zero_Before_Comma();
          
        }
        private void Button1_Click(object sender, EventArgs e) => Button0_9 ("1");
        private void Button2_Click(object sender, EventArgs e) => Button0_9 ("2");
        private void Button3_Click(object sender, EventArgs e) => Button0_9 ("3");
        private void Button4_Click(object sender, EventArgs e) => Button0_9 ("4");
        private void Button5_Click(object sender, EventArgs e) => Button0_9 ("5");
        private void Button6_Click(object sender, EventArgs e) => Button0_9 ("6");
        private void Button7_Click(object sender, EventArgs e) => Button0_9 ("7");
        private void Button8_Click(object sender, EventArgs e) => Button0_9 ("8");        
        private void Button9_Click(object sender, EventArgs e) => Button0_9 ("9");
        private void Button_dot_comma_Click(object sender, EventArgs e)
        {
            if (one_comma_for_one_number && example_count<18)
            {
                if (label_main_okno.Text == "" || label_main_okno.Text.Last() == '+' || label_main_okno.Text.Last() == '-'
                                               || label_main_okno.Text.Last() == '/' || label_main_okno.Text.Last() == '*')
                {
                    label_main_okno.Text += "0";
                    example_count++;
                }
                label_main_okno.Text += ",";
                example_count++;
                label1.Text = label_main_okno.Text == string.Empty ? "" : Calculate_double(label_main_okno.Text);

                label_focus.Focus();
                one_comma_for_one_number = false;
            }






        }
        

        // операции +-*/
        void Operator (string str)
        {
            one_comma_for_one_number = true;
            new_example = false;

            try
            {
                if (example_count < 18)
                {
                    if (Char.IsNumber(label_main_okno.Text.Last()))
                        label_main_okno.Text += str;
                    else if (label_main_okno.Text.Length != 1)
                    {
                        label_main_okno.Text = label_main_okno.Text.Remove(label_main_okno.Text.Length - 1);
                        example_count--;
                        label_main_okno.Text += str;
                    }
                    example_count++;
                }
            }
            catch  { }
            label_focus.Focus();
        }

        private void Button_plus_Click(object sender, EventArgs e)     => Operator("+");
        private void Button_multiply_Click(object sender, EventArgs e) => Operator("*");
        private void Button_divide_Click(object sender, EventArgs e)   => Operator("/");
        private void Button_minus_Click(object sender, EventArgs e)
        {
            one_comma_for_one_number = true;
            new_example = false;

            try
            {
                if (example_count < 18)
                {
                    if (Char.IsNumber(label_main_okno.Text.Last()))
                        label_main_okno.Text += "-";
                    else
                    {
                        label_main_okno.Text = label_main_okno.Text.Remove(label_main_okno.Text.Length - 1);
                        example_count--;
                        label_main_okno.Text += "-";
                    }
                    example_count++;
                }
            }
            catch  
            {
                label_main_okno.Text = "-";
                example_count++;
            }
            label_focus.Focus();
        }
        

        // дополнительные кнопки 
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            label_main_okno.Text = string.Empty;
            label1.Text   = string.Empty;
            divide_by_zero_trigger = false;
            one_comma_for_one_number = true;
            example_count = 0;
            DividebyZero();
            label_focus.Focus();
        }

        private void ButtonBackspace_Click(object sender, EventArgs e)
        {
           if(label_main_okno.Text!=string.Empty) label_main_okno.Text = label_main_okno.Text.Remove(label_main_okno.Text.Length - 1);
            label1.Text = string.Empty;
            divide_by_zero_trigger = false;
            new_example = false;

            
            
            example_count --;
            if (example_count == 255)
            {
                example_count = 0;
            }

            DividebyZero();
            label1.Text = label_main_okno.Text == string.Empty ? "" : Calculate_double(label_main_okno.Text);

            label_focus.Focus();
        }

        private void Button_result_Click(object sender, EventArgs e)
        {
            one_comma_for_one_number = false;
            new_example = true;
            label_main_okno.Text = label_main_okno.Text == string.Empty? "" : Calculate_double(label_main_okno.Text);
            example_count = (byte)label_main_okno.Text.Length;
            label_focus.Focus();

        }


        //управление ввода с клавиатуры
        private void My_Program_KeyDown(object sender, KeyEventArgs e)
        {

            if (!divide_by_zero_trigger)
            {
                switch (e.KeyValue)
                {
                    case (char)Keys.OemMinus:
                    case (char)Keys.Subtract:
                        Button_minus_Click(button_minus, null);
                        break;

                    case (char)Keys.Oemplus:
                    case (char)Keys.Add:
                        Button_plus_Click(button_plus, null);
                        break;

                    case (char)Keys.Divide:
                        Button_divide_Click(button_divide, null);
                        break;

                    case (char)Keys.Multiply:
                        Button_multiply_Click(button_multiply, null);
                        break;


                    case ((char)Keys.Enter):
                        Button_result_Click(button_result, null);
                        break;

                    case (char)Keys.D0:
                    case (char)Keys.NumPad0:
                        Button0_Click(button0, null);
                        break;

                    case (char)Keys.D1:
                    case (char)Keys.NumPad1:
                        Button1_Click(button1, null);
                        break;

                    case (char)Keys.D2:
                    case (char)Keys.NumPad2:
                        Button2_Click(button2, null);
                        break;

                    case (char)Keys.D3:
                    case (char)Keys.NumPad3:
                        Button3_Click(button3, null);
                        break;

                    case (char)Keys.D4:
                    case (char)Keys.NumPad4:
                        Button4_Click(button4, null);
                        break;

                    case (char)Keys.D5:
                    case (char)Keys.NumPad5:
                        Button5_Click(button5, null);
                        break;
                    case (char)Keys.D6:
                    case (char)Keys.NumPad6:
                        Button6_Click(button6, null);
                        break;

                    case (char)Keys.D7:
                    case (char)Keys.NumPad7:
                        Button7_Click(button7, null);
                        break;

                    case (char)Keys.D8:
                    case (char)Keys.NumPad8:
                        Button8_Click(button8, null);
                        break;

                    case (char)Keys.D9:
                    case (char)Keys.NumPad9:
                        Button9_Click(button9, null);
                        break;

                    case (char)Keys.OemQuestion:
                        Button_dot_comma_Click(button_dot_comma, null);
                        break;
                }
            }

                switch(e.KeyValue)
                {
                case (char)Keys.Back:
                    ButtonBackspace_Click(buttonBackspace, null);
                    break;
                case (char)Keys.Decimal:
                case (char)Keys.Oemcomma:
                case (char)Keys.OemPeriod:
                    Button_dot_comma_Click(button_dot_comma, null);
                    break;


            }
        
        }

    }
}

