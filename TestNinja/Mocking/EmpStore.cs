namespace TestNinja.Mocking
{
    public class EmpStore : IEmpStore
    {
        private EmployeeContext _db;

        public EmpStore()
        {
            _db = new EmployeeContext();
        }
        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null) return;
            _db.Employees.Remove(employee);
            _db.SaveChanges();
                
            
            
        }
        
    }
}