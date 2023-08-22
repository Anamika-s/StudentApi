using StudentApi.Models;

namespace StudentApi
{
    public interface IStudentRepo
    {
        List<Student> GetStudents();
        Student AddStudent(Student student);
        Student EditStudent(Student student, int id);
        int DeleteStudent(int id);
        Student GetStudentById(int id);


    }
}
