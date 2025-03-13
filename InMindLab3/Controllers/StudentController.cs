using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using InMindLab1.Common;
using InMindLab1.Filters;
using InMindLab1.Middleware;
using InMindLab1.Models;
using InMindLab1.Services;
using InMindLab1.Services.StudentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMindLab1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : Controller
{

    // These will be resolved with dependency injection
    private readonly IStudentService _studentService;
    private readonly ObjectMapperService _objectMapperService;

    public StudentController(IStudentService studentService, ObjectMapperService objectMapperService)
    {
        _studentService = studentService;
        _objectMapperService = objectMapperService;
    }

    [HttpGet("[action]")]
    public List<Student> GetAllStudents()
    {
        return _studentService.GetAllStudents();
    }

    [HttpGet("[action]/{id}")]
    public IActionResult GetStudentById([FromRoute] int id)
    {
        
        Result<Student>  result= _studentService.GetStudentById(id);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(new {Error = result.Error});
        
    }

    [HttpGet("[action]")]
    public List<Student> getStudentByName([FromQuery] string name)
    {
        return _studentService.getStudentByName(name);
    }

    [HttpGet("[action]")]
    public string date([FromHeader(Name = "Accepted-Language")] string language)
    {
        return _studentService.date(language);
    }

    [HttpPost("[action]")]
    public void changeName([FromBody] Student student)
    {
        _studentService.changeName(student);
    }

    
    [HttpPost("[action]")]
    [Consumes("multipart/form-data")]
    public void UploadImage([FromForm] IFormFile file)
    {
        _studentService.UploadImage(file);
    }

    [HttpDelete("[action]/{id}")]
    public void deleteStudent([FromRoute] int id)
    {
        _studentService.deleteStudent(id);
    }

    [HttpGet("[action]")]
    public void exception()
    {
        throw new Exception();
    }
    
    
    [HttpPost("mapStudentToPerson")]
    public IActionResult MapStudentToPerson([FromBody] Student student)
    {
        if (student == null)
            return BadRequest("Invalid student data");

        
        Person person = _objectMapperService.Map<Student, Person>(student);

        return Ok(person);
    }


    [HttpGet("[action]")]
    [ServiceFilter(typeof(ILoggerActionFilter))]
    public IActionResult ILoggerTest([FromBody] Student student)
    {
        return Ok(student);
    }
    
    
}