using A.Entity;
using BaseLib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace A.DLL
{
    public interface IStudentRepository: IBaseRepository<Student>
    {

    }
    public interface IUsersRepository : IBaseRepository<Users>
    {

    }
}
