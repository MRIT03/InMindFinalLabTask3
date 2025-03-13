using System.Collections.Generic;
using InMindLab1.Models;
using System.Globalization;
using System.Net.Mime;
using InMindLab1.Models;
using InMindLab1.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace InMindLab1.Services.StudentServices;

public interface IStudentService
{
    [HttpGet("[action]")]
    public List<Student> GetAllStudents();

    [HttpGet("[action]/{id}")]
    public Result<Student> GetStudentById([FromRoute] int id);

    [HttpGet("[action]")]
    public List<Student> getStudentByName([FromQuery] string name);

    [HttpGet("[action]")]
    public string date([FromHeader(Name = "Accepted-Language")] string language);

    [HttpPost("[action]")]
    public void changeName([FromBody] Student student);


    [HttpPost("[action]")]
    public void UploadImage([FromForm] IFormFile file);

    [HttpDelete("[action]/{id}")]
    public void deleteStudent([FromRoute] int id);


}