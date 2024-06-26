# U16A2

## Problem 1 - Design Documentation 

The client has requested that we create a Todo list that will allow for:

+ Creation and deletion of tasks 

+ Tracking done state and allowing tasks to be set to complete 

+ Supporting title, description, due date, completed with description and due date being mutable (note also requirement to change completion status above) 

+ Displaying a list of tasks 

+ Toggling whether all tasks or only incomplete tasks are displayed 

This program is primarily intended to be shown as a GUI application. The creation and deletion of tasks should be done using a button or similar element in the GUI, this will make the deletion and creation tasks a lot simpler for the users. This is also how we are going to make the task marked as completed is by using the same type of button as the add tasks and remove tasks. The way we will show that the complete tasks are being displayed with be with a check box that will be able to be toggled on and off. 

### Case Diagram

![alt text](Img/image.png)

### Name Spaces and Libraries

![alt text](Img/image2.png)

Theses namespaces will have classes and methods that will be of use when it comes to creating this todo list. Some the methods and classes will help with UI controls data collection and property changing.

### Data Structure/Data Storage

![alt text](Img/image3.png)

This is how the todo list will be getting all of the information from the inputs that the user has done, this piece of code is getting the information from the user and then storing it in the correct place ready to be return later on to the user. 

## Problem 1 - Development Documentation

When it comes to the application I have developed a simple todo list that will allow for the creation and deletion of tasks, WPF application (Windows Presentation Foundation).

### "MainWindow.xaml" Code

```xml
<Window x:Class="TaskEditor.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TaskEditor"
        mc:Ignorable="d"
        Title="To-Do List" Height="450" Width="800"
        Background="#ECFFFF">
    <Grid Margin="0,10,0,0">

        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2*" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <ListBox x:Name="TaskList" Grid.Column="0" Margin="277,10,10,10" SelectionMode="Single" Grid.ColumnSpan="2" Background="#ADD8E6"/>

        <StackPanel Margin="10,10,276,10">
            <TextBox x:Name="TitleBox" Text="Task Name" Margin="0,0,0,10" Background="#ADD8E6"/>
            <TextBox x:Name="DescriptionBox" Text="Task Description" Margin="0,0,0,10" Height="108" Background="#ADD8E6"/>
            <DatePicker x:Name="DueDatePicker" Margin="0,0,0,10" Background="#ADD8E6"/>
            <Button Content="Add Task to List" Click="AddTask_Click" Margin="0,0,0,10" Background="#ADD8E6"/>
            <Button Content="Delete Task from List" Click="DeleteTask_Click" Margin="0,0,0,10" Background="#ADD8E6"/>
            <Button Content="Mark as Finished" Click="MarkAsComplete_Click" Margin="0,0,0,10" Background="#ADD8E6"/>
            <CheckBox x:Name="ShowCompletedTasksCheckBox" Content="Show Finished Tasks" Checked="ShowCompletedTasks_Checked" Unchecked="ShowCompletedTasks_Checked"/>
        </StackPanel>

       

    </Grid>
</Window>


```

### "MainWindow.xaml.cs" Code

```csharp

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

```

### Test Plan

| Test Number | Expected Result                                                              | Actual Result     |
|-------------|------------------------------------------------------------------------------|-------------------|
| 1.          | When I click the add button it will  add the task to the list.               | Works as Intended |
| 2.          | When I click the delete button it will remove the task from the list.        | Works as intended |
| 3.          | When I select a date it will be added to the task                            | Works as intended |
| 4.          | When I click the marked as completed it will change the status of the task | Works as intended |
| 5.          | When using the show finished check box it will only show the finished tasks  | Works as intended |

![alt text](Img/image8.png)

![alt text](Img/image9.png)

![alt text](Img/image10.png)

## Problem 2 - Design Documentation

The college library needs to add all its books to a new index system. The system will allow for reading CSV files and then generating a unique ID for the books then output them into a new CSV file.

### Pseudocode

BEGIN CSVProcessor

    FUNCTION Main
        SET inputFilePath TO "filepath\original.csv"
        SET outputFilePath TO "filepath\data.csv"

        READ all lines from inputFilePath INTO fileLines
        CREATE outputContent

        FOR EACH line IN fileLines
            SPLIT line by ',' INTO columns
            SET hashValue TO ComputeMD5Hash(columns)
            ADD line + "," + hashValue TO outputContent
        END FOR

        WRITE outputContent TO outputFilePath
        PRINT "CSV file processed successfully!"
    END FUNCTION

    FUNCTION ComputeMD5Hash(values)
        CONCATENATE values INTO concatenatedValues
        CONVERT concatenatedValues TO bytes dataBytes
        COMPUTE MD5 hash of dataBytes INTO hashBytes

        CREATE hashString FROM hashBytes AS hexadecimal string
        RETURN first 8 characters of hashString
    END FUNCTION

END CSVProcessor

### GUI Design

Using Photoshop I have created a design for the GUI that will be used for the library system.

![alt text](Img/image6.png)

## Problem 2 - Development Documentation 

```csharp

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class CSVProcessor
{
    static void Main()
    {
        string inputFilePath = @"..\..\..\Taskdata.csv";
        string outputFilePath = @"..\..\..\TaskdataUPDATED.csv";

        string[] fileLines = File.ReadAllLines(inputFilePath);

        StringBuilder outputContent = new StringBuilder();

        foreach (string line in fileLines)
        {
            string[] columns = line.Split(',');
            string hashValue = ComputeMD5Hash(columns);
            outputContent.AppendLine($"{line},{hashValue}");
        }

        File.WriteAllText(outputFilePath, outputContent.ToString());
        Console.WriteLine("CSV file processed successfully!");
    }

    static string ComputeMD5Hash(string[] values)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            StringBuilder hashBuilder = new StringBuilder();
            string concatenatedValues = string.Join("", values);
            byte[] dataBytes = Encoding.UTF8.GetBytes(concatenatedValues);
            byte[] hashBytes = md5Hash.ComputeHash(dataBytes);

            foreach (byte b in hashBytes)
            {
                hashBuilder.Append(b.ToString("X2"));
            }

            return hashBuilder.ToString().Substring(0, 8);
        }
    }
}

```

### Test Plan

| Test Number | Expected Result                                | Actual Result              |
|-------------|------------------------------------------------|----------------------------|
| 1.          | The code can find the CSV file                 | The code can fine the file |
| 2.          | The code will output the info to a new CSV file | The code works as intended |
| 3.          | The code will generate a unique hash           | The code works as intended |
| 4.          | The hash will be not be different for repeats  | The code works as intended |
| 5.          | The code will output in the command line       | The code works as intended |

![alt text](Img/image11.png)

### Review and Feedback

#### Aiden Scowen

"Your solutions looks like they meet the requirements, both solutions are working as intended and the code is easy to read however i would suggest changing the colours around on the GUI as it looks a little weird." 

#### Ugnius Mieldazys

"Your solutions looks like they meet the requirements, both solutions are working and simple, the GUI looks good and the code is easy to read."

### Inprovements based on feedback 

#### Aiden Scowen

![alt text](Img/image12.png)

### 

## Evaluation

When it comes to the client requirements I have met all of the requirements for the client and have created an easy and simple solutions for the client. According to the feedback I have received the code is easy to read and the GUI looks good. I have also made the changes that were suggested to me by the feedback that I have received, this would be changing the colour theme of my GUI around.

The hardest part of the project was making sure the hash was unique however was also able to not have different hashes for the same data. The next issue was the GUI as it was difficult to make sure that it was working correctly. Then the buttons did not work as intended and had to be fixed.

