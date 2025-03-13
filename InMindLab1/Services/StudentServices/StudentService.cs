
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using InMindLab1.Common;
using InMindLab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMindLab1.Services.StudentServices;

public class StudentService : IStudentService
{
    List<Student> students = new List<Student>();

    public StudentService()
    {
        Student student1 = new Student(1, "student 1", "student1@gmail.com");
        Student student2 = new Student(2, "student 2", "student2@gmail.com");
        Student student3 = new Student(3, "student 3", "student3@gmail.com");
        students.Add(student1);
        students.Add(student2);
        students.Add(student3);
    }
    
    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Result<Student> GetStudentById(int id)
    {
        
        Student student = students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return Result<Student>.Failure($"Student with id {id} Not Found");
        }
        return Result<Student>.Success(student);
    }

    public List<Student> getStudentByName(string name)
    {
        return students.FindAll(s => s.Name == name);
    }

    public string date(string language)
    {
        if (string.IsNullOrEmpty(language))
        {
            //Assume default Language is English
            language = "en-US";
        }

        CultureInfo culture;

        try
        {
            culture = CultureInfo.GetCultureInfo(language);
        }
        catch
        {
            //If invalid language
            return "Invalid Language";
        }
        return DateTime.Today.ToString(culture);
    }

    public void changeName(Student student)
    {
        Student toChange = students.First(s => s.Id == student.Id);
        toChange.Name = student.Name;
        toChange.Email = student.Email;
    }

    public async void UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return;

        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);

            // Ensure directory exists
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images"));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return;
        }
        catch (Exception ex)
        {
            return;
        }
    }

    public void deleteStudent(int id)
    {
        if (id < 0)
        {
            //No Negative Ids
            return;
        }
        Student toDelete = students.First(s => s.Id == id);
        students.Remove(toDelete);
    }
}