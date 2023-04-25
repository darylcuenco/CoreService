using Microsoft.AspNetCore.Mvc;

namespace core.Controllers;
using core.Interfaces;
using core.Models;
using Newtonsoft.Json;
using System;

[ApiController]
[Route("[controller]")]
public class CoreController : ControllerBase
{


    private readonly ILogger<CoreController> _logger;
    private readonly IDBm _dbm;

    public CoreController(ILogger<CoreController> logger, IDBm dbm)
    {
        _logger = logger;
        _dbm = dbm;
    }

    [HttpGet("health")]
    public string Get()
    {
        return "healthy";
    }

    [HttpGet("FindAll")]
    public ResponseModelArray FindAll()
    {
        dynamic apiResp = JsonConvert.DeserializeObject(_dbm.FindAll().Result);
        ResponseModelArray resp = new ResponseModelArray();
        if (apiResp.isSuccess != null && (bool)apiResp.isSuccess)
        {
            resp.result = JsonConvert.DeserializeObject<List<DBManagerObject>>(apiResp.result + "");
            resp.isSuccess = apiResp.isSuccess;

        }
        else
        {
            resp.result = apiResp.result + "";
            resp.isSuccess = false;
        }

        return resp;
    }

    [HttpPost("Insert")]
    public ResponseModel Insert(dynamic request)
    {
        Console.WriteLine("request: " + request);
        DBManagerObject obj = new DBManagerObject();
        obj.data = JsonConvert.DeserializeObject<Data>(request + "");
        dynamic apiResp = JsonConvert.DeserializeObject(_dbm.Insert(obj).Result);
        ResponseModel resp = new ResponseModel();

        Console.WriteLine("apiResp.result: " + apiResp.result);
        if (apiResp.isSuccess != null && (bool)apiResp.isSuccess)
        {
            resp.result = JsonConvert.DeserializeObject<DBManagerObject>(apiResp.result + "");
            resp.isSuccess = apiResp.isSuccess;
        }
        else
        {
            resp.message = apiResp.result + "";
            resp.isSuccess = false;
        }
        return resp;
    }


    [HttpPost("Upsert")]
    public ResponseModel Upsert(dynamic request)
    {
        dynamic apiResp = JsonConvert.DeserializeObject(_dbm.Upsert(request).Result);
        ResponseModel resp = new ResponseModel();

        Console.WriteLine("apiResp.result: " + apiResp.result);
        if (apiResp.isSuccess != null && (bool)apiResp.isSuccess)
        {
            resp.result = JsonConvert.DeserializeObject<DBManagerObject>(apiResp.result + "");
            resp.isSuccess = apiResp.isSuccess;
        }
        else
        {
            resp.message = apiResp.result + "";
            resp.isSuccess = false;
        }
        
        return resp;
    }

    [HttpPost("FindById")]
    public ResponseModel FindById(dynamic request)
    {
        DBManagerObject obj = new DBManagerObject();
        var jsonObj = JsonConvert.DeserializeObject(request + "");
        obj.id = jsonObj.id;
        dynamic apiResp = JsonConvert.DeserializeObject(_dbm.FindById(obj).Result);
        ResponseModel resp = new ResponseModel();

        Console.WriteLine("apiResp.result: " + apiResp.result);
        if (apiResp.isSuccess != null && (bool)apiResp.isSuccess)
        {
            resp.result = JsonConvert.DeserializeObject<DBManagerObject>(apiResp.result + "");
            resp.isSuccess = apiResp.isSuccess;
        }
        else
        {

            resp.message = apiResp.message;
            resp.isSuccess = false;
        }
        
        return resp;
    }

    [HttpPost("DeleteById")]
    public ResponseModel DeleteById(dynamic request)
    {
        DBManagerObject obj = new DBManagerObject();
        var jsonObj = JsonConvert.DeserializeObject(request + "");
        obj.id = jsonObj.id;
        dynamic apiResp = JsonConvert.DeserializeObject(_dbm.DeleteById(obj).Result);
        ResponseModel resp = new ResponseModel();

        Console.WriteLine("apiResp.result: " + apiResp.result);
        resp.message = apiResp.result + "";
        if (apiResp.isSuccess != null && (bool)apiResp.isSuccess)
        {
            resp.isSuccess = apiResp.isSuccess;
        }
        else
        {
            resp.isSuccess = false;

        }
       
        
        return resp;
    }

}
