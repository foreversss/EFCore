using EFCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.BLL.IService
{
    public interface IUserService
    {
        List<tb_users> users();
    }
}
