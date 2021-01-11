
using A.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL
{
    public class StudentDll : IRepository
    {
        //定义死的数据源

        List<Student> students = new List<Student>()
        {
            new Student(){Id = 1,Name="小明",Grade= Grade.Unassigned,Mailbox="1695443384@qq.com" },
            new Student(){Id = 2,Name="小丽",Grade= Grade.Unassigned,Mailbox="12345645@qq.com" },
            new Student(){Id = 3,Name="小高",Grade= Grade.Unassigned,Mailbox="12306345@qq.com" },
            new Student(){Id = 4,Name="小波",Grade= Grade.Unassigned,Mailbox="162343255435@qq.com" }
        };


        public Student GetById(int Id)
        {
            return students.Where(x => x.Id == Id).FirstOrDefault();
        }

        public Student Add(Student student)
        {
            student.Id = students.Max(x => x.Id) + 1;
            students.Add(student);
            return student;
        }

        public int Delete(int Id)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (Id == students[i].Id)
                {
                    students.Remove(students[i]);
                }
            }          
            return 1;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return students;
        }

        public int Update(Student student)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (student.Id == students[i].Id)
                {
                    students[i] = student;
                }
            }

            return 1;
        }
    }
}
