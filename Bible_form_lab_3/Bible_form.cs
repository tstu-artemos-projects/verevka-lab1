using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace Bible_form_lab_3
{
    public partial class FormManager : Form
    {
        private bool _isProgrammaticChange = false;//проверка заполнена ли переменная
        public FormManager()
        {
            InitializeComponent();
        }

        private void btnNewBook_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabNewBook;
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabPageSupport;
        }

        private void btnMarket_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabPageMarket;
        }

        private void tabPageMain_Click(object sender, EventArgs e)
        {

        }
        private void tabControlNewBook_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!_isProgrammaticChange)
            {
                e.Cancel = true;
            }
        }

        private void buttonClearNewBook_Click(object sender, EventArgs e)
        {
            textBoxTitleBook.Clear();
            textBoxAutor.Clear();//очищаем текстовые поля

            if (comboBoxType.Items.Count > 0)
                comboBoxType.SelectedIndex = -1; // очищаем комбобоксы

            numericUpDownPages.Value = numericUpDownPages.Minimum;
            numericUpDownPrice.Value = numericUpDownPrice.Minimum;//очищаем поля с номерами            
            labelIDforUsing.Text = "(будет присвоен автоматически)";// сбрасываем label с ID

            textBoxTitleBook.Focus();// для красоты возвращаем фокус на первое поле
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {

            try
            {
                ValidateInput(); // запускаем проверку

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

        }

        private void ShowError(Exception ex)
        {
            string message, title;

            switch (ex)
            {
                case ArgumentException argEx:
                    message = $"Ошибка в поле {argEx.ParamName}: {argEx.Message}";
                    title = "Ошибка валидации";
                    break;

                case FormatException:
                    message = "Неверный формат числовых данных.";
                    title = "Ошибка формата";
                    break;

                default:
                    message = $"Непредвиденная ошибка: {ex.Message}";
                    title = "Критическая ошибка";
                    break;
            }

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ValidateInput()
        {
            // Проверка названия для поиска
            if (string.IsNullOrWhiteSpace(textBoxSearchTitle.Text))
                throw new ArgumentException("Поле 'Поиск по названию' не может быть пустым", "SearchTitle");

            // Проверка ID (должно быть числом)
            if (!int.TryParse(textBoxSearchID.Text, out int id) || id <= 0)
                throw new ArgumentException("ID должен быть положительным целым числом", "SearchID");

            // Проверка названия книги
            if (string.IsNullOrWhiteSpace(textBoxTitleBook.Text))
                throw new ArgumentException("Название книги обязательно для заполнения", "TitleBook");

            // Проверка автора
            if (string.IsNullOrWhiteSpace(textBoxAutor.Text))
                throw new ArgumentException("Укажите автора книги", "Author");

            // Проверка количества страниц (из NumericUpDown)
            if (numericUpDownPages.Value <= 0)
                throw new ArgumentException("Количество страниц должно быть больше 0", "Pages");

            // Проверка цены
            if (numericUpDownPages.Value < 0) // Предположим, цена в другом numeric, или используем этот для примера
                throw new ArgumentException("Цена не может быть отрицательной", "Price");
        }

        private void buttonReturn1_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabPageMain;
        }

        private void buttonReturn2_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabPageMain;
        }

        private void buttonReturn3_Click(object sender, EventArgs e)
        {
            tabControlNewBook.SelectedTab = tabPageMain;
        }

        private void FormManager_Load(object sender, EventArgs e)
        {

        }

        private void buttonSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInput(); // запускаем проверку

            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Если в списке выбрана хотя бы одна книга
            if (listViewBooks.SelectedItems.Count > 0)
            {
                // Скрываем панель поиска
                groupBoxSearch.Visible = false;

                // Показываем панель с информацией и кнопкой продажи
                groupBoxDetails.Visible = true;

                // Достаем данные выбранной книги (предположим, они в Tag элемента)
                // Если используете просто текст, можно брать из SubItems
                var title = listViewBooks.SelectedItems[0].Text;
                var author = "Автор книги"; // Тут подставьте логику получения автора
                var price = 500; // Тут цена из вашего объекта

                labelInfo.Text = $"Книга: {title}\nАвтор: {author}\nЦена: {price} руб.";

                // Перемещаем панель деталей на место поиска (на всякий случай)
                groupBoxDetails.Location = groupBoxSearch.Location;
                groupBoxDetails.Size = groupBoxSearch.Size;
            }
            else
            {
                // Если выбор снят — возвращаем поиск
                groupBoxSearch.Visible = true;
                groupBoxDetails.Visible = false;
            }
        }

        private void btnBackToSearch_Click(object sender, EventArgs e)
        {
            listViewBooks.SelectedIndices.Clear(); // снимаем выделение, это само вызовет возврат поиска
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (listViewBooks.SelectedItems.Count > 0)
            {
                var selectedItem = listViewBooks.SelectedItems[0];
                
                listViewBooks.Items.Remove(selectedItem);// удаляем книгу из списка
                
                groupBoxDetails.Visible = false;// возвращаем панель поиска, так как выбор пропал
                groupBoxSearch.Visible = true;

                MessageBox.Show("Книга успешно продана!", "Магазин");
            }
        }
    }
}
