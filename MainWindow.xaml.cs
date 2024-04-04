using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab1_task2_Length_сonverter
{
    enum UnitMeasure { mm , cm, dm, m, km, yd, mile }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double number_mm; // Число пользователя переведенное в миллиметры.
        int unitLeft; // Индекс меры измерения числа пользователя
        int unitRight; // Индекс меры конвертации

        public MainWindow()
        {
            InitializeComponent();
            initComboBoxLength();
        }

        /// <summary>
        /// Инициализация двух ComboBox
        /// </summary>
        private void initComboBoxLength() 
        {
            // Заполнение двух ComboBox единицами измерения длины.
            foreach (var value in Enum.GetValues(typeof(UnitMeasure))) 
            {
                ComboBoxItem itemLeft = new ComboBoxItem();
                itemLeft.Content = value;

                ComboBoxItem itemRight = new ComboBoxItem();
                itemRight.Content = value;

                ComboBox_ChoiceLengthLeft.Items.Add(itemLeft);
                ComboBox_ChoiceLengthRight.Items.Add(itemRight);
            }
            ComboBox_ChoiceLengthLeft.SelectedIndex = 0;
            ComboBox_ChoiceLengthRight.SelectedIndex = 0;
        }

        /// <summary>
        /// Обработчик событий двух comboBox выбора еденицы измерения.
        /// </summary>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /// Устанавливает индексы выбранных единиц измерения длины

             ComboBox box = (ComboBox)sender;
            if (box.Name == "ComboBox_ChoiceLengthLeft")
            {
                unitLeft = box.SelectedIndex;
            }
            else
            {
               unitRight = box.SelectedIndex;
            }
            
        }

        /// <summary>
        /// Извлекает пользовательский ввод и конвертирует его в миллиметры. При неудачном конвертировании сообщает об ошибке.
        /// </summary>
        private void extractInputData()
        {
            double user_number;

            // Обработка запятой или точки при вводе дробного числа.
            string userInput = TextBox_InputValue.Text;
            if (userInput.Contains(".")) userInput = userInput.Replace(".", ",");


            bool flag = Double.TryParse(userInput, out user_number);
            if (flag && user_number > 0)
            {
                number_mm = convertTo_mm(user_number);
            }
            else
            {
                string msg_text = "Некорректный ввод!!!\nВозможно вы ввели буквы алфавита или отрицательное число.\n";
                MessageBox.Show(msg_text, "Некорректный ввод!");

            }
        }

        /// <summary>
        /// Приводит ввод пользователя к миллиметрам.
        /// </summary>
        /// <param name="value">Значение длины конвертируемой в миллиметры.</param>
        private double convertTo_mm(double value)
        {
            if (unitLeft == ((int) UnitMeasure.mm)) return value;
            if (unitLeft == (int)UnitMeasure.cm) return value * 10;
            if (unitLeft == (int)UnitMeasure.dm) return value * 100 ;
            if (unitLeft == (int)UnitMeasure.m) return value * 1000;
            if (unitLeft == (int)UnitMeasure.km) return value * 1000000;
            if (unitLeft == (int)UnitMeasure.yd) return value * 914.4;
            else
            {
                // UnitMeasure.mile
                return value * 1609344;
            }
          
        }

        /// <summary>
        /// Конвертирует число в выбранную единицу измерения.
        /// </summary>
        private double convertTo_userChoice(double value)
        {
            if (unitRight == ((int)UnitMeasure.mm)) return value;
            if (unitRight == (int)UnitMeasure.cm) return value / 10;
            if (unitRight == (int)UnitMeasure.dm) return value / 100;
            if (unitRight == (int)UnitMeasure.m) return value / 1000;
            if (unitRight == (int)UnitMeasure.km) return value / 1000000;
            if (unitRight == (int)UnitMeasure.yd) return value / 914.4;
            else
            {
                // UnitMeasure.mile
                return value / 1609344;
            }
        }

        /// <summary>
        /// Обработчик кнопки Конвертировать.
        /// </summary>
        private void Button_Convert_Click(object sender, RoutedEventArgs e)
        {
            extractInputData();
            double number = convertTo_userChoice(number_mm);
            TextBox_OutputValue.Text = number.ToString();
        }
    }
}