using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Создаем экземпляр контекста данных
        UniversityEntities dbContext = new UniversityEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик загрузки данных
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Загружаем данные из таблицы Students в DataGrid
                StudentsDataGrid.ItemsSource = dbContext.Students.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        // Обработчик добавления новой записи
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый объект-студент
                Students newStudent = new Students
                {
                    FirstName = "Тест",
                    LastName = "Студент",
                    BirthDate = DateTime.Now.AddYears(-20)
                };

                // Добавляем его в контекст
                dbContext.Students.Add(newStudent);
                // Сохраняем изменения в БД
                dbContext.SaveChanges();

                // Обновляем DataGrid
                LoadButton_Click(sender, e);
                MessageBox.Show("Студент добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        // Важно: освобождаем ресурсы контекста при закрытии окна
        protected override void OnClosed(EventArgs e)
        {
            dbContext.Dispose();
            base.OnClosed(e);
        }
    }
}
