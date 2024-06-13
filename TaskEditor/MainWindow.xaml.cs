using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaskEditor
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>();
            TaskList.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new TaskItem
            {
                Title = TitleBox.Text,
                Description = DescriptionBox.Text,
                DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
                IsCompleted = false
            };
            Tasks.Add(newTask);
            ResetInputFields();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask)
            {
                Tasks.Remove(selectedTask);
            }
        }

        private void MarkAsComplete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask)
            {
                selectedTask.IsCompleted = true;
                TaskList.Items.Refresh();
            }
        }

        private void ShowCompletedTasks_Checked(object sender, RoutedEventArgs e)
        {
            TaskList.ItemsSource = ShowCompletedTasksCheckBox.IsChecked == true
                ? Tasks
                : Tasks.Where(t => !t.IsCompleted);
        }

        private void ResetInputFields()
        {
            TitleBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            DueDatePicker.SelectedDate = null;
        }
    }

    public class TaskItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Description} - {(IsCompleted ? "Finished" : "Pending")} - Due: {DueDate.ToShortDateString()}";
        }
    }
}
