
using core.Interfaces;
using core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;

namespace core.Implementations;
public class DBMImplementations : IDBm
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _config;
    public DBMImplementations(IHttpClientFactory clientFactory, IConfiguration config)
    {
        _clientFactory = clientFactory;
        _config = config;
    }

    public async Task<dynamic> DeleteById(DBManagerObject obj)
    {
        string payload = JsonConvert.SerializeObject(obj);
        Console.WriteLine("payload: " + payload);
        using (var httpResponse = _clientFactory.CreateClient().PostAsync(_config["DBManagerURL"] + "/DeleteById", new StringContent(payload, Encoding.UTF8, "application/json")))
        {
            var stringResult = await httpResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine("stringResult: " + stringResult);
            return stringResult;
        }
    }

    public async Task<dynamic> FindAll()
    {

        using (var httpResponse = _clientFactory.CreateClient().PostAsync(_config["DBManagerURL"] + "/FindAll", new StringContent("", Encoding.UTF8, "application/json")))
        {
            var stringResult = await httpResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine(stringResult);
            return stringResult;
        }
        

    }

    public async Task<dynamic> FindById(DBManagerObject obj)
    {
        string payload = JsonConvert.SerializeObject(obj);
        Console.WriteLine("payload: " + payload);
        using (var httpResponse = _clientFactory.CreateClient().PostAsync(_config["DBManagerURL"] + "/FindById", new StringContent(payload, Encoding.UTF8, "application/json")))
        {
            var stringResult = await httpResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine("stringResult: " + stringResult);
            return stringResult;
        }
    }

    public async Task<dynamic> Insert(DBManagerObject obj)
    {
        string payload = JsonConvert.SerializeObject(obj.data);
        Console.WriteLine("payload: " + payload);
        using (var httpResponse = _clientFactory.CreateClient().PostAsync(_config["DBManagerURL"] + "/Insert", new StringContent(payload, Encoding.UTF8, "application/json")))
        {
            var stringResult = await httpResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine("stringResult: " + stringResult);
            return stringResult;
        }

    }

    public async Task<dynamic> Upsert(dynamic obj)
    {
        //string payload = JsonConvert.SerializeObject(obj);
        //Console.WriteLine("payload: " + payload);
        using (var httpResponse = _clientFactory.CreateClient().PostAsync(_config["DBManagerURL"] + "/Upsert", new StringContent(obj + "", Encoding.UTF8, "application/json")))
        {
            var stringResult = await httpResponse.Result.Content.ReadAsStringAsync();
            Console.WriteLine("stringResult: " + stringResult);
            return stringResult;
        }
    }
}