using System.Collections.ObjectModel;
using Task2.model;

namespace Task2
{
    public partial class MainPage : ContentPage
    {
        // Observable collection to hold employee records
        public ObservableCollection<Employee> Employees { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Employees = new ObservableCollection<Employee>();
            EmployeeListView.ItemsSource = Employees; // Bind the ListView to Employees collection
        }

        // Add new employee
        private void AddEmployee_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(EmployeeAge.Text, out int age))
            {
                Employees.Add(new Employee
                {
                    ID = Employees.Count + 1,
                    Name = EmployeeName.Text,
                    Age = age,
                    DOB = EmployeeDOB.Date
                });
                ClearFields();
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid age.", "OK");
            }
        }

        // Update selected employee
        private void UpdateEmployee_Clicked(object sender, EventArgs e)
        {
            if (EmployeeListView.SelectedItem is Employee selectedEmployee)
            {
                selectedEmployee.Name = EmployeeName.Text;
                selectedEmployee.Age = int.Parse(EmployeeAge.Text);
                selectedEmployee.DOB = EmployeeDOB.Date;
                EmployeeListView.ItemsSource = null;
                EmployeeListView.ItemsSource = Employees; // Refresh ListView
                ClearFields();
            }
            else
            {
                DisplayAlert("Selection Error", "Please select an employee to update.", "OK");
            }
        }

        // Delete selected employee
        private void DeleteEmployee_Clicked(object sender, EventArgs e)
        {
            if (EmployeeListView.SelectedItem is Employee selectedEmployee)
            {
                Employees.Remove(selectedEmployee);
                ClearFields();
            }
            else
            {
                DisplayAlert("Selection Error", "Please select an employee to delete.", "OK");
            }
        }

        // View employee details
        private void ViewEmployee_Clicked(object sender, EventArgs e)
        {
            if (EmployeeListView.SelectedItem is Employee selectedEmployee)
            {
                EmployeeID.Text = selectedEmployee.ID.ToString();
                EmployeeName.Text = selectedEmployee.Name;
                EmployeeAge.Text = selectedEmployee.Age.ToString();
                EmployeeDOB.Date = selectedEmployee.DOB;
            }
            else
            {
                DisplayAlert("Selection Error", "Please select an employee to view.", "OK");
            }
        }

        // Clear input fields
        private void ClearFields()
        {
            EmployeeID.Text = string.Empty;
            EmployeeName.Text = string.Empty;
            EmployeeAge.Text = string.Empty;
            EmployeeDOB.Date = DateTime.Today;
        }
    }
}
