using A.Entity;
using BaseLib.DBContext;
using BaseLib.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace A.DLL
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(EFCoreDBContext dbcontext) : base(dbcontext)
        {

        }
    }
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(EFCoreDBContext dbcontext) : base(dbcontext)
        {

        }
    }
}
