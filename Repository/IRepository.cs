
using A.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<Student> GetAllStudent();
        Student GetById(int Id);
        Student Add(Student student);
        int Delete(int Id);

        int Update(Student student);


    }
}
