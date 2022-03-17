using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
//using System.Collections.Specialized;

//namespace Lab_1.Controllers;
namespace Lab_1.Controllers;

[ApiController]
// [Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private const string V = "|";
   // private static readonly string[] Summaries = new[]
   // {
   //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
   // };
     
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Country")]
        
    public List<Item> Get(string country)
    {
        
       
       var list = new List<Item>();
       
       
       
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.Country.Equals(country)
          select dt;

        foreach (Item dt in itemquery)
        {
        
     //     list.Add(row.ToString()+ V + dt.RowNumber.ToString() + V + dt.Country + V + dt.Segment + V + dt.Product + V + dt.UnitSold.ToString());
        list.Add(dt);
        
        
        }
             
       
            return list;
        
    }
 [HttpGet("Product")]
public List<Item> Get1(string product)
    {
        
       //DataContainer dtt = new DataContainer();
       var list = new List<Item>();
       //var dta = DataContainer.DataList;
        
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.Product.Equals(product)
          select dt;

        foreach (Item dt in itemquery)
        {
  //          list.Add(row.ToString()+ V + dt.RowNumber.ToString() + V + dt.Country + V + dt.Segment + V + dt.Product + V + dt.UnitSold.ToString());
        list.Add(dt);
       
        }
            return list;
        
}
 [HttpGet("Segment")]
public List<Item> Get2(string segment)
    {
     //   row=1;
       //DataContainer dtt = new DataContainer();
       var list = new List<Item>();
       //var dta = DataContainer.DataList;
        
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.Segment.Equals(segment)
          select dt;
    
        foreach (Item dt in itemquery)
        {
     //       list.Add(row.ToString()+ V + dt.RowNumber.ToString() + V + dt.Country + V + dt.Segment + V + dt.Product + V + dt.UnitSold.ToString());
     //   row++;
     list.Add(dt);
        }
            return list;
        
}
[HttpGet("Segment + Country")]
public IDictionary<string,string> Get3(string segment, string country)
    {
       
        decimal res = 0;
      
       var list = new List<Item>();
       
        
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where (dt.Segment.Equals(segment) && dt.Country.Equals(country)) 
          select dt;

        foreach (Item dt in itemquery)
        {   
            res = res + dt.UnitSold; 
        }

           IDictionary<string,string> dict = new Dictionary<string,string>();
                dict["Country"]=country;
                dict["Segment"]=segment;
                dict["UnitSold"]=res.ToString();



           
            return dict;
        
}

[HttpGet("Add new record")]
public List<Item> Get4(string segment, string country, string product, decimal unitsold)
    {
      
       int licznik = 0;
       var list = new List<Item>();

       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.Country.Equals(country) && dt.Segment.Equals(segment) && dt.Product.Equals(product) && dt.UnitSold.Equals(unitsold)
          select dt;
      
        licznik = DataContainer.DataList.Count;
        DataContainer.DataList.Add(new Item(licznik+2, segment,country,product,unitsold));
       
        DataContainer.Instance.WriteToExcel(licznik+2, segment,country,product,unitsold);
       
        foreach (Item dt in itemquery)
        {   
        list.Add(dt);
        }
        
            return list;
        
}

[HttpGet("delete record")]
public List<Item> Get5(int del_rec)
    {
       var list = new List<Item>(); 
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.RowNumber.Equals(del_rec)
          select dt;


        foreach (Item dt in itemquery)
        {   
         list.Add(dt);
        }
        
        DataContainer.DataList.RemoveAt(del_rec-2);
        DataContainer.Instance.DeleteFromExcel(del_rec);
        

            return list;
        
        
    }
     [HttpGet("Search")]
public List<Item> Get6(int _row)
    {
     
      
       var list = new List<Item>();
       
        
       IEnumerable<Item> itemquery =
       from dt in DataContainer.DataList
       where dt.RowNumber.Equals(_row)
       select dt;

        foreach (Item dt in itemquery)
        {
    //        list.Add(row.ToString()+ V + (dt.RowNumber).ToString() + V + dt.Country + V + dt.Segment + V + dt.Product + V + dt.UnitSold.ToString());
       list.Add(dt);
        }
            return list;
        
}

}
