using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using weather.util;

namespace weather.DataAccessLayer
{
    
        public class cityDetails
        {
            public string city
            {
                get;
                set;
            }
            public int id
            {
                get;
                set;
            }

        }

        public class DataAccessObject
        {

            public Dictionary<string, List<cityDetails>> countryToCityMap = new Dictionary<string, List<cityDetails>>();
            public Dictionary<int, float> cityToTempMap = new Dictionary<int, float>();
            public Dictionary<float, List<int>> tempToCityMap = new Dictionary<float, List<int>>();
            public Dictionary<int, string> cityToCountryMap = new Dictionary<int, string>();


            void updateTempToCityMap(int city)
            {
                // get the currentTemp from citytoTemp map use it to reverse map the current tempToCity Key and delete the current entry from this map
                float currtemp = cityToTempMap[city];
                if (tempToCityMap.ContainsKey(currtemp))
                {
                    List<int> currentCities = tempToCityMap[currtemp];
                    if (currentCities.Contains(city))
                        currentCities.Remove(city);
                }
            }

            void setTempToCityMap(float temp, int city)
            {

                updateTempToCityMap(city);

                if (tempToCityMap.ContainsKey(temp))
                {
                    List<int> currCity = tempToCityMap[temp];
                    currCity.Add(city);
                    tempToCityMap[temp] = currCity;
                }
                else
                {
                    List<int> info = new List<int>();
                    info.Add(city);
                    tempToCityMap.Add(temp, info);
                }
            }

            void addCountryInfo(string country, cityDetails city)
            {

                if (countryToCityMap.ContainsKey(country))
                {
                    List<cityDetails> currentInfo = countryToCityMap[country];
                    currentInfo.Add(city);
                    countryToCityMap[country] = currentInfo;
                }
                else
                {
                    List<cityDetails> ls = new List<cityDetails>();
                    ls.Add(city);
                    countryToCityMap.Add(country, ls);
                }
                setCityToCountryMap(city.id, country);

            }

            void setCityToCountryMap(int city, string country)
            {
                if(!cityToCountryMap.ContainsKey(city))
                    cityToCountryMap.Add(city, country);
            }

            List<cityDetails> getCountryInfo(string country)
            {
                List<cityDetails> temp = null;
                if (countryToCityMap.ContainsKey(country))
                {
                    temp = countryToCityMap[country];

                }
                return temp;
            }

            public List<resultsPrb2> getCityCountForTempRange(float mintemp, float maxtemp)
            {
                List<resultsPrb2> results = new List<resultsPrb2>();
                List<int> cities = new List<int>();
                foreach (float temp in tempToCityMap.Keys)
                {
                    if (temp >= mintemp && temp <= maxtemp)
                    {
                        cities.AddRange(tempToCityMap[temp]);
                    }
                }

                Dictionary<string, int> res = new Dictionary<string, int>();
                foreach (int city in cities)
                {
                    string country = cityToCountryMap[city];
                    if (res.ContainsKey(country))
                    {
                        res[country] = res[country] + 1;
                    }
                    else
                    {
                        res.Add(country, 1);
                    }
                }

                foreach (string country in res.Keys)
                {
                    resultsPrb2 o = new resultsPrb2();
                    o.count = res[country];
                    o.country = country;
                    results.Add(o);
                }

                return results;
            }


            public float getCountryTemperature(string country)
            {
                List<cityDetails> cities = getCountryInfo(country);
                if (cities != null)
                {
                    float avgTemp = 0f;
                    foreach (cityDetails city in cities)
                    {
                        avgTemp += getTemperature(city.id);
                    }
                    return avgTemp / cities.Count();
                }
                else
                {
                    return float.NaN;
                }
            }




            float getTemperature(int place)
            {
                return cityToTempMap[place];
            }
            void setTemperature(int place, float temp)
            {
                if (cityToTempMap.ContainsKey(place) == false)
                {
                    cityToTempMap.Add(place, temp);

                }
                else
                {
                    cityToTempMap[place] = temp;
                }
                //adding the temp info to inverted map!
                setTempToCityMap(temp, place);
            }



            public void readData()
            {
                //string text = File.ReadAllText("~/DataAccessLayer"+"weather_14.json"));
                string template = File.ReadAllText( System.Web.HttpContext.Current.Server.MapPath("~/DataAccessLayer/weather_14.json"));
                string[] lines= template.Split('\n');
               // string[] lines = System.IO.File.ReadAllLines(@"D:\weatherapp\weatherapp\weatherapp\DataAccessLayer\weather_14.json");
                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        dynamic weather = JsonConvert.DeserializeObject(line);
                        cityDetails cityInfo = new cityDetails();
                        cityInfo.city = weather.city.name;
                        cityInfo.id = weather.city.id;

                        string country = weather.city.country;
                        int temp = weather.main.temp;


                        addCountryInfo(country, cityInfo);
                        setTemperature(cityInfo.id, temp);
                    }
                }
            }


        }
    
}

